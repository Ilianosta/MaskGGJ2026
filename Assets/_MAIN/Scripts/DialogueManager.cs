using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public List<DialogueData> dialogues = new List<DialogueData>();

    public float timeAnimationChar = 1;
    int currentDialogueIndex = 0;

    InputSystem_Actions inputActions;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    [ContextMenu("Start dialogue")]
    public void StartDialogue()
    {
        StartCoroutine(StartDialogueAnimation(dialogues[currentDialogueIndex]));
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
        UIManager.instance.CloseDialoguePanel();
        yield return null;
    }
}
