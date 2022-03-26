using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Newtonsoft.Json.Converters;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToTargetState : AntAIState
{
    public GhoulModel ghoulModel;
    public Rigidbody rb;

    public GameObject pathGrid;
    public PathfindingAgent pathfinding;

    public PathfindingGrid grid;
    //probably need the pathfinding component
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
        rb = GetComponentInParent<Rigidbody>();
        grid = pathGrid.GetComponent<PathfindingGrid>();
        pathfinding = GetComponentInParent<PathfindingAgent>();
        pathfinding.enabled = true;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        //Pathfinding GetPath() function
        //Need to repeat every x seconds if not included in pathfinding script
        pathfinding.FindPath(this.transform.position, ghoulModel.target.transform.position);
            
        Debug.Log("Entering Move to Target State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Debug.Log("Executing Move to Target State");
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Move to Target State");
        pathfinding.enabled = false;
    }
}
