using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public SkinnedMeshRenderer[] renderers;
    private Camera cam;
    public bool ignoreTriggers;
    public LayerMask myLayer;

    private void Start()
    {
        cam = Camera.main;
    }

    public void SetGlow(bool value)
    {
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.material.SetInt("_Enabled", Convert.ToInt32(value));
        }
    }

    public void Update()
    {
        QueryTriggerInteraction trigger = QueryTriggerInteraction.Collide;

        if (ignoreTriggers)
        {
            trigger = QueryTriggerInteraction.Ignore;
        }

        LayerMask layer = transform.root.gameObject.layer;
        Ray ray = cam.ScreenPointToRay(InputManager.Instance.GetMousePosition());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayer, trigger))
        {
            if (hit.transform == transform.root)
            {
                SetGlow(true);
            }
        }
        else
        {
            SetGlow(false);
        }
    }
}
