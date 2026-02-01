using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public List<DialogueData> dialogues = new List<DialogueData>();

    public float timeAnimationChar = 1;
    int currentDialogueIndex = 0;
    InputSystem_Actions inputActions;

    private bool continueDialogue = false;
    private Coroutine clickCoroutine;
    private Coroutine timeCoroutine;

    public delegate void OnStartDialogue();
    public static OnStartDialogue onStartDialogue;

    public delegate void OnFinishDialogue();
    public static OnFinishDialogue onFinishDialogue;
    void Awake()
    {
        if (DialogueManager.instance != null) Destroy(gameObject);
        else DialogueManager.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        if (inputActions == null) inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    [ContextMenu("Start dialogue")]
    public void StartDialogue()
    {
        StartCoroutine(StartDialogueAnimation(dialogues[currentDialogueIndex]));
    }

    public void StartDialogue(string[] dialogues)
    {
        StartCoroutine(StartDialogueAnimation(dialogues));
    }

    IEnumerator StartDialogueAnimation(DialogueData dialogueData)
    {
        onStartDialogue?.Invoke();
        UIManager.instance.OpenDialoguePanel();
        foreach (string dialogue in dialogueData.dialogues)
        {
            yield return new WaitForSeconds(.3f);
            string tempTxt = "";
            foreach (char text in dialogue)
            {
                tempTxt += text;
                UIManager.instance.SetDialogueText(tempTxt);
                if (!inputActions.Player.Attack.IsPressed())
                {
                    
                    //Debug.Log("Not skipping");
                    yield return new WaitForSeconds(timeAnimationChar);
                }
                else
                {
                    //Debug.Log("Skipping");
                    
                }
            }
            clickCoroutine = StartCoroutine(WaitForPlayerDecisionDialogueInput());
            timeCoroutine = StartCoroutine(WaitForPlayerDecisionDialogueTimer());
            yield return new WaitUntil(() => continueDialogue);
            continueDialogue = false;
        }
        ResetDialogue();
        UIManager.instance.CloseDialoguePanel();
        onFinishDialogue?.Invoke();
    }

    IEnumerator WaitForPlayerDecisionDialogueInput()
    {
        Debug.Log("Waiting for click");
        yield return new WaitUntil(() => inputActions.Player.Attack.triggered);
        StopCoroutine(timeCoroutine);
        Debug.Log("Click engaged, removing timer");
        continueDialogue = true;
    }
    IEnumerator WaitForPlayerDecisionDialogueTimer()
    {
        Debug.Log("Starting timer");
        yield return new WaitForSeconds(5f);
        StopCoroutine(clickCoroutine);
        Debug.Log("Timer ended, removing click detection");
        continueDialogue = true;
    }


    IEnumerator StartDialogueAnimation(string[] dialogues)
    {
        onStartDialogue?.Invoke();
        UIManager.instance.OpenDialoguePanel();
        foreach (string dialogue in dialogues)
        {
            yield return new WaitForSeconds(.3f);
            string tempTxt = "";
            foreach (char text in dialogue)
            {
                tempTxt += text;
                UIManager.instance.SetDialogueText(tempTxt);
                if (!inputActions.Player.Attack.IsPressed())
                {
                    // Debug.Log("Not skipping");
                    yield return new WaitForSeconds(timeAnimationChar);
                }
                else
                {
                    // Debug.Log("Skipping");
                }
            }

            yield return new WaitUntil(() => inputActions.Player.Attack.triggered);
        }
        ResetDialogue();
        UIManager.instance.CloseDialoguePanel();
        onFinishDialogue?.Invoke();
    }

    void ResetDialogue()
    {
        UIManager.instance.SetDialogueText("");
    }
}
