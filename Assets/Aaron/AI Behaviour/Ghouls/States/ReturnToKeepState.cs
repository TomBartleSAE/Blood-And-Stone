using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class ReturnToKeepState : AntAIState
{
    public GhoulModel ghoulModel;
    
    public Transform keepLocation;
    //need Pathfinding ref here

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        ghoulModel = GetComponentInParent<GhoulModel>();
        //Ref to pathfinding

    }
    public override void Enter()
    {
        base.Enter();
        
        //Pathfinding GetPath() in here
        //Don't need to repeat as keep doesn't move
        
        Debug.Log("Entering Return to Keep State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Return to Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        
        Debug.Log("Exiting Return to Keep State");
    }
}
