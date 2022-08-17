using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessageManager : ManagerBase<MessageManager>
{
    [SerializeField] private TextMeshProUGUI messageText;

    public float timer;
    public float lerpValue = 0.1f;

    public float dilate;

    public bool textFade = false;
    
    public void ShowMessage(string message, float duration)
    {
        lerpValue = 0;
        timer = duration;
        messageText.text = message;
        messageText.enabled = true;
        textFade = true;
    }

    private void Update()
    {
        if (timer > 0)
        {
	        timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //messageText.enabled = false;
                textFade = false;
            }
        }
        
        if (textFade && messageText.enabled)
        {
            messageText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Lerp(-0.5f, -0.1f, lerpValue += 0.5f * Time.deltaTime));
        }

        if (!textFade && messageText.enabled)
        {
            float dilateFloat = messageText.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate);
            dilate = ( dilateFloat -= 0.5f * Time.deltaTime);
            messageText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilate);
            if (dilate <= -0.5f)
            {
                messageText.enabled = false;
            }
        }
    }
}   
