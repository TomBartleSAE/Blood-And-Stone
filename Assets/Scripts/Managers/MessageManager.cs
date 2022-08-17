using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessageManager : ManagerBase<MessageManager>
{
    [SerializeField] private TextMeshProUGUI messageText;

    private float timer;
    public float lerpValue = -0.1f;
    
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
        
        if (messageText.enabled)
        {
	        messageText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Lerp(-0.5f, 0f, lerpValue += 0.5f * Time.deltaTime));
        }
    }
}
