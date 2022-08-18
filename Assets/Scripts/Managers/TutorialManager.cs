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

    public bool tutorialPreviouslyCompleted;

    public enum MouseButton
    {
        LeftClick,
        RightClick,
        Either
    }

    public MouseButton mouseButton;

    public void Start()
    {
        textBox.SetActive(false);
        
        InputManager.Instance.OnLeftClickEvent += PerformLeftClick;
        InputManager.Instance.OnRightClickEvent += PerformRightClick;

        elements = new TutorialElementBase[transform.childCount];

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = transform.GetChild(i).GetComponent<TutorialElementBase>();
        }
        
        elements[index].Activate();
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnLeftClickEvent -= PerformLeftClick;
        InputManager.Instance.OnRightClickEvent -= PerformRightClick;
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
        
        textBox.SetActive(false);
        
        if (activeClickArea != null)
        {
            if (mouseButton == MouseButton.LeftClick || mouseButton == MouseButton.Either)
            {
                Vector2 mousePosition = InputManager.Instance.GetMousePosition();

                if(RectTransformUtility.RectangleContainsScreenPoint(activeClickArea, mousePosition))
                {
                    activeClickArea = null;
                    GameObject visual = elements[index].GetComponent<TutorialAction>().clickVisual;
                    if (visual != null)
                    {
                        visual.SetActive(false);
                    }
                    Progress();
                }
            }
        }
        else if (elements[index].GetComponent<TutorialDialogue>())
        {
            Progress();
        }
    }

    
    private void PerformRightClick(ClickEventArgs args)
    {
        // HACK copy-pasted from above
        if (activeClickArea != null)
        {
            if (mouseButton == MouseButton.RightClick || mouseButton == MouseButton.Either)
            {
                Vector2 mousePosition = InputManager.Instance.GetMousePosition();

                if(RectTransformUtility.RectangleContainsScreenPoint(activeClickArea, mousePosition))
                {
                    activeClickArea = null;
                    GameObject visual = elements[index].GetComponent<TutorialAction>().clickVisual;
                    if (visual != null)
                    {
                        visual.SetActive(false);
                    }
                    Progress();
                }
            }
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
        
        for (int i = 0; i <= text.Length; i++)
        {
            tutorialText.text = text.Substring(0, i);
            yield return new WaitForSecondsRealtime(typingDelay);
        }

        isTextTyping = false;
    }

    public IEnumerator ProgressAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Progress();
    }

    public void GiveBlood(int amount)
    {
        PlayerManager.Instance.ChangeBlood(PlayerManager.Instance.currentBlood + amount);
    }
    
    public void SetGhouls(int amount)
    {
        PlayerManager.Instance.CurrentGhouls = amount;
    }

    public void LoadNewGameData()
    {
        PlayerManager.Instance.LoadSaveData(SaveManager.Instance.LoadGame(SaveManager.Instance.newGameDataPath));
    }

    public void TutorialCompletedBoolChange(bool value)
    {
        SettingsManager.Instance.tutorialCompleted = value;
    }
}
