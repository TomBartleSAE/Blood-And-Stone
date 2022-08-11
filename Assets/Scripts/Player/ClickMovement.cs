using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickMovement : MonoBehaviour
{
    public PathfindingAgent agent;

    public Camera cam;

    public LayerMask enemyLayer;
    public LayerMask walkableLayers;

    public Transform target;

    private float timer;
    public float repathTime = 1f;

    public bool isSelected;
    public bool isLeftClickMove;

    public bool clickMovementActive;

    public GraphicRaycaster graphicRaycaster;

    public event Action<bool> HasTargetEvent;
    public event Action StartedMoveEvent;

    private void Start()
    {
	    if (isLeftClickMove)
	    {
		    InputManager.Instance.OnLeftClickEvent += PerformClick;
	    }
	    InputManager.Instance.OnRightClickEvent += PerformClick;
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void OnDestroy()
    {
	    if (isLeftClickMove)
	    {
		    InputManager.Instance.OnLeftClickEvent -= PerformClick;
	    }
	    InputManager.Instance.OnRightClickEvent -= PerformClick;
    }

    public void Update()
    {
        if (target != null)
        {
            timer -= Time.deltaTime;
            
            if (timer < 0)
            {
                MoveToPoint(target.position);
                timer = repathTime;
            }
        }
    }

    private void PerformClick(ClickEventArgs args)
    {
        if (isSelected)
        {
            Ray ray = cam.ScreenPointToRay(args.mousePosition);
            RaycastHit hit;

            // Prevents movement when clicking on assigned UI canvas
            if (graphicRaycaster != null)
            {
                PointerEventData data = new PointerEventData(EventSystem.current);
                data.position = args.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                graphicRaycaster.Raycast(data, results);

                if (results.Count > 0)
                {
                    return;
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                target = hit.transform;
                HasTargetEvent?.Invoke(true);
                return;
            }
            else
            {
                // HACK
                target = null;
                HasTargetEvent?.Invoke(false);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayers))
            {
                Node hitNode = agent.grid.GetNodeFromPosition(hit.point);

                if (!hitNode.isBlocked)
                {
                    MoveToPoint(hitNode.coordinates);
                    StartedMoveEvent?.Invoke();
                }
            }
        }
    }

    public void MoveToPoint(Vector3 destination)
    {
        if (isSelected)
        {
            if (clickMovementActive)
            {
                agent.FindPath(transform.position, destination);
            }
        }
    }
}
