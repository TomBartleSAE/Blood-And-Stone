using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class IdleState : AntAIState
{
    public Wander wander;
    public GameObject owner;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        wander = GetComponentInParent<Wander>();
        owner = aGameObject;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        wander.speed = 8;
    }

     public override void Execute(float aDeltaTime, float aTimeScale)
     {
         base.Execute(aDeltaTime, aTimeScale);
     }

    public override void Exit()
    {
        base.Exit();
    }
}
