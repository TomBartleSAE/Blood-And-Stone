using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxSelection : MonoBehaviour
{
    public RectTransform selectionBox;
    public Camera cam;
    public GraphicRaycaster graphicRaycaster;
    public TowerPlacement towerPlacement;

    public event Action<bool> GhoulSelectedEvent; 

    public LayerMask ghoulLayer;

    public List<GameObject> units = new List<GameObject>();

    private Vector2 startPos;

    private bool HUDClick;
    private bool clickHold;

    private void Start()
    {
        InputManager.Instance.OnLeftClickEvent += PerformClick;
        InputManager.Instance.OnLeftReleaseEvent += ReleaseClick;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnLeftClickEvent -= PerformClick;
        InputManager.Instance.OnLeftReleaseEvent -= ReleaseClick;
    }

    void Update()
    {
        //Prevents clicking/dragging onto HUD elements
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = InputManager.Instance.GetMousePosition();
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(data, results);

        //can't select units while building selected
        if (towerPlacement.selectedBuilding != null)
        {
            HUDClick = true;
        }

        if (results.Count > 0)
        {
            HUDClick = true;
            return;
        }

        //if click is being held down, will draw a box from click start point to current position
        if (clickHold)
        {
            //May need to add a timer to determine whether was single click or drag
            
            if (HUDClick == false)
            {
                if (!selectionBox.gameObject.activeInHierarchy)
                {
                    selectionBox.gameObject.SetActive(true);    
                }

                //mouse positions to set size
                float width = data.position.x - startPos.x;
                float height = data.position.y - startPos.y;

                //setting size of box
                selectionBox.sizeDelta = new Vector2(Math.Abs(width), Math.Abs(height));
                selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
            }
        }
    }

    //single click/click start
    void PerformClick(ClickEventArgs args)
    {
        clickHold = true;
        
        if (units.Count != 0)
        {
            units.Clear();
        }

        startPos = InputManager.Instance.GetMousePosition();

        RaycastHit hit;
            
        if (Physics.Raycast(cam.ScreenPointToRay(InputManager.Instance.GetMousePosition()), out hit, Mathf.Infinity, ghoulLayer))
        {
            if (hit.transform.GetComponent<GhoulModel>())
            {
                units.Add(hit.transform.gameObject);
            }
        }
        HUDClick = false;
    }

    //releasing click
    void ReleaseClick(ClickEventArgs args)
    {
        clickHold = false;
        
        //box removed
        selectionBox.gameObject.SetActive(false);

        //gets box size/position
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        //gets position of ghouls, if within the range of the box will be selected and change to selected colour; else will return to ghoul colour
        foreach (var ghoul in DayNPCManager.Instance.Ghouls)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(ghoul.transform.position);

            //if inside the box, will get selected
            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                if (!units.Contains(ghoul))
                {
                    units.Add(ghoul);
                }
            }

            if (units.Contains(ghoul))
            {
                ghoul.GetComponent<ClickMovement>().isSelected = true;
                ghoul.GetComponent<SelectionIndicator>().EnableIndicator();
            }
            
            //if outside the box, will not get selected/get deselected
            else
            {
                ghoul.GetComponent<ClickMovement>().isSelected = false;
                ghoul.GetComponent<SelectionIndicator>().DisableIndicator();
            }
        }

        if (units.Count > 0)
        {
            GhoulSelectedEvent?.Invoke(true);
        }
        else
        {
            GhoulSelectedEvent?.Invoke(false);
        }
    }
}
