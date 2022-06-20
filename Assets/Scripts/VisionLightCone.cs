using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VisionLightCone : MonoBehaviour
{
    public Vision vision;
    
    void Update()
    {
        // TODO: Move this to an event later when angle and distance changes
        float radius = Mathf.Tan(vision.angle * Mathf.PI / 180f) * vision.distance;
        transform.localScale = new Vector3(radius, radius, vision.distance);
    }
}
