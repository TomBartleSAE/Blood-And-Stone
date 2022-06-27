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

    public LayerMask ghoulLayer;

    public List<GameObject> units = new List<GameObject>();

    public Vector2 startPos;

    public bool HUDClick;

    private void Start()
    {
        //cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //mouse up
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
        
        //Prevents clicking/dragging onto HUD elements
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(data, results);

        if (results.Count > 0)
        {
            HUDClick = true;
            return;
        }

        //mouse down
        if (Input.GetMouseButtonDown(0))
        {
            if (units.Count != 0)
            {
                units.Clear();
            }
            
            //TODO raycast for individual select
            
            startPos = Input.mousePosition;

            RaycastHit hit;
            
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ghoulLayer))
            {
                if (hit.transform.GetComponent<GhoulModel>())
                {
                    units.Add(hit.transform.gameObject);
                    hit.transform.GetComponent<GhoulModel>().isSelected = true;
                    hit.transform.GetComponent<SelectionIndicator>().EnableIndicator();
                }
            }
            HUDClick = false;
        }
        
        //mouse held down
        if (Input.GetMouseButton(0))
        {
            /*if (units.Count != 0)
            {
                units.Clear();
            }*/

            if (HUDClick == false)
            {
                UpdateSelectionBox(Input.mousePosition);
            }
        }
    }

    //creating a selection box
    void UpdateSelectionBox(Vector2 currentMousePos)
    {

        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        //mouse positions to set size
        float width = currentMousePos.x - startPos.x;
        float height = currentMousePos.y - startPos.y;

        //setting size of box
        selectionBox.sizeDelta = new Vector2(Math.Abs(width), Math.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    //clearing the box from the screen
    void ReleaseSelectionBox()
    {
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
                ghoul.GetComponent<GhoulModel>().isSelected = true;
                ghoul.GetComponent<SelectionIndicator>().EnableIndicator();
            }
            
            //if outside the box, will not get selected/get deselected
            else
            {
                ghoul.GetComponent<GhoulModel>().isSelected = false;
                ghoul.GetComponent<SelectionIndicator>().DisableIndicator();
            }
        }
    }
}
