using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPhaseState : StateBase
{
    public override void Enter()
    {
        base.Enter();
        FindObjectOfType<AudioManager>().Play("MainMenuMusic", AudioManager.ArrayName.music);
    }
}
