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
        foreach (string dialogue in dialogueData.dialogues)
        {
            string tempTxt = "";
            foreach (char text in dialogue)
            {
                tempTxt += text;
                UIManager.instance.SetDialogueText(tempTxt);
                yield return new WaitForSeconds(timeAnimationChar);
            }

            yield return new WaitUntil(() => inputActions.Player.Attack.triggered);
        }
        yield return null;
    }
}
