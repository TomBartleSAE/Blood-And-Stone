using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : ManagerBase<MessageManager>
{
    [SerializeField] private TextMeshProUGUI messageText;

    private float timer;
    
    public void ShowMessage(string message, float duration)
    {
        timer = duration;
        messageText.text = message;
        messageText.enabled = true;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                messageText.enabled = false;
            }
        }
    }
}
