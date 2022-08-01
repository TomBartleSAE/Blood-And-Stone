using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipObject : MonoBehaviour
{
    public Camera cam;
    public GraphicRaycaster graphicRaycaster;

    public LayerMask objectLayer;
    public GameObject tooltip;

    private void Start()
    {
        InputManager.Instance.OnLeftClickEvent += PerformLeftClick;
        ShowTooltip(false);

        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnLeftClickEvent -= PerformLeftClick;
    }

    private void PerformLeftClick(ClickEventArgs args)
    {
        Ray ray = cam.ScreenPointToRay(args.mousePosition);
        RaycastHit hit;

        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = args.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(data, results);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.root == transform)
            {
                ShowTooltip(true);
            }
        }
        else if (results.Count == 0)
        {
            ShowTooltip(false);
        }
    }
    
    public void ShowTooltip(bool active)
    {
        tooltip.SetActive(active);
    }
}
