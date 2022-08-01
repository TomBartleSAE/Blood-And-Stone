using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBlood : MonoBehaviour
{
    // Super hacky way to change blood in tutorial
    public void Give(int amount)
    {
        PlayerManager.Instance.ChangeBlood(PlayerManager.Instance.currentBlood + amount);
    }
}
