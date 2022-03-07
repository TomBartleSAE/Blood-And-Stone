using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class VillagerModel : MonoBehaviour
{
    public AntAIAgent antAIAgent;

    static event Action PersonEatenEvent;

    public bool isScared;
    public bool isStunned;
    public bool isEaten;
    
    // Start is called before the first frame update
    void Start()
    {
        antAIAgent.SetGoal("Survive");

        PersonEatenEvent += Reaction;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEaten)
        {
            PersonEatenEvent?.Invoke();
        }
    }

    public void Reaction()
    {
        //Raycast to check if in sight
        //if(Raycast == true)
        //{
        //  isScared = true;
        //}
    }
}
