using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : TutorialElementBase
{
    [System.Serializable]
    public class MovementInfo
    {
        public PathfindingAgent agent;
        public Transform destination;
    }

    public MovementInfo[] movements;
    public float movementTime;
    
    public override void Activate()
    {
        base.Activate();

        foreach (MovementInfo movement in movements)
        {
            movement.agent.FindPath(movement.agent.transform.position, movement.destination.position);
        }

        StartCoroutine(TutorialManager.Instance.ProgressAfterTime(movementTime));
    }
}
