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
        onStartDialogue?.Invoke();
    }

    public void StartDialogue(string[] dialogues)
    {
        StartCoroutine(StartDialogueAnimation(dialogues));
        onStartDialogue?.Invoke();
    }

    IEnumerator StartDialogueAnimation(DialogueData dialogueData)
    {
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
                    Debug.Log("Not skipping");
                    yield return new WaitForSeconds(timeAnimationChar);
                }
                else
                {
                    Debug.Log("Skipping");
                }
            }

            yield return new WaitUntil(() => inputActions.Player.Attack.triggered);
        }
        onFinishDialogue?.Invoke();
        ResetDialogue();
        UIManager.instance.CloseDialoguePanel();
    }

    IEnumerator StartDialogueAnimation(string[] dialogues)
    {
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
        onFinishDialogue?.Invoke();
        ResetDialogue();
        UIManager.instance.CloseDialoguePanel();
    }

    void ResetDialogue()
    {
        UIManager.instance.SetDialogueText("");
    }
}
