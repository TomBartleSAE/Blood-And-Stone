using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Anthill.AI;
using UnityEngine;

public class FleeingState : AntAIState
{
    public VillagerModel villager;
    public GameObject owner;
    public AudioManager.ArrayName audioArrayName;
    public string audioFleeingClipName;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
        villager = aGameObject.GetComponentInParent<VillagerModel>();
    }
    
    public override void Enter()
    {
        base.Enter();

        //TODO path to flee along
        villager.GetComponent<FollowPath>().moveSpeed = villager.fleeSpeed;
        int index = Random.Range(1, 3); //this is just picking a random number for the different scream clips
        AudioManager.Instance.Play(audioFleeingClipName + index, audioArrayName); // here's the code for the audio event
        StartCoroutine(Flee());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aDeltaTime);
    }

    public override void Exit()
    {
        villager.GetComponent<FollowPath>().moveSpeed = villager.moveSpeed;
        base.Exit();
    }
    
    IEnumerator Flee()
    {
        yield return new WaitForSeconds(villager.fleeTime);

        villager.isScared = false;
    }
}