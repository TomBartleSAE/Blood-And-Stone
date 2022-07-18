using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : ManagerBase<TutorialManager>
{
    public GameObject textBox;
    public TextMeshProUGUI tutorialText;
    public Image characterPortrait;

    public RectTransform activeClickArea;

    private TutorialElementBase[] elements;
    public int index = 0;

    private bool isTextTyping = false;
    private Coroutine textTyping;
    private string currentText;

    public void Start()
    {
        InputManager.Instance.OnLeftClickEvent += PerformLeftClick;

        elements = new TutorialElementBase[transform.childCount];

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = transform.GetChild(i).GetComponent<TutorialElementBase>();
        }
        
        elements[index].Activate();
    }

    private void PerformLeftClick(ClickEventArgs obj)
    {
        if (isTextTyping)
        {
            StopCoroutine(textTyping);
            tutorialText.text = currentText;
            isTextTyping = false;
            return;
        }
        
        if (activeClickArea != null)
        {
            Vector2 mousePosition = InputManager.Instance.GetMousePosition();

            if(RectTransformUtility.RectangleContainsScreenPoint(activeClickArea, mousePosition))
            {
                activeClickArea = null;
                elements[index].GetComponent<TutorialAction>().clickVisual?.SetActive(false); // Yucky
                Progress();
            }
        }
        else
        {
            Progress();
        }
    }

    public void Progress()
    {
        index++;
        elements[index].Activate();
    }

    public void ShowText(string text, Sprite portrait, float typingDelay)
    {
        textBox.SetActive(true);
        textTyping = StartCoroutine(TypeText(text, typingDelay));

        if (portrait != null)
        {
            characterPortrait.gameObject.SetActive(true);
            characterPortrait.sprite = portrait;
        }
        else
        {
            characterPortrait.gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeText(string text, float typingDelay)
    {
        // Shows text instantly if delay is set to 0
        if (typingDelay <= 0)
        {
            tutorialText.text = text;
            yield break;
        }
        
        isTextTyping = true;
        currentText = text;
        
        for (int i = 0; i < text.Length + 1; i++)
        {
            tutorialText.text = text.Substring(0, i);
            yield return new WaitForSeconds(typingDelay);
        }

        isTextTyping = false;
    }
}
