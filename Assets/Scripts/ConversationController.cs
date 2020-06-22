using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

public class ConversationController : MonoBehaviour
{
    [SerializeField] ConversationDialogueInfo conversationInfo = default;
    [SerializeField] CharacterInfo playerInfo = default;
    [SerializeField] Image dialogueImage = default;
    [SerializeField] Text dialogueTextfield = default;
    [SerializeField] Image subDialogueImage = default;
    [SerializeField] Button continueButton = default;
    [SerializeField] Button selectionButton = default;

    Queue<string> conversations;

    void Start()
    {
        conversations = new Queue<string>();
        conversationInfo.conversationStart = false;
        conversationInfo.selectionStart = false;
        conversationInfo.dialoguOpened = false;
    }

    void Update()
    {
        ConversationStart();
        SelectionStart();
    }

    void ConversationStart()
    {

        if (conversationInfo.conversationStart == false)
            return;

        if (conversationInfo.dialoguOpened == true)
            return;

        conversations.Clear();
        foreach (ConversationLine conversation in conversationInfo.conversationData.conversationLines)
        {
            conversations.Enqueue(conversation.text);
        }

        conversationInfo.dialoguOpened = true;
        DialogueOpen();
        ForwardConversation();
    }

    public void ForwardConversation()
    {
        if (conversationInfo.dialoguOpened == false)
            return;

        if (conversationInfo.selectionStart == true)
            return;

        if (conversations.Count == 0)
        {
            if (0 < conversationInfo.conversationData.converSationSelectios.Length)
            {
                conversationInfo.selectionStart = true;
                return;
            }

            DialogueClose();
             return;
        }
        StartCoroutine(LetterDisplay(conversations.Dequeue()));
    }

    IEnumerator LetterDisplay(string conversation)
    {
        dialogueTextfield.text = null;

        foreach (char c in conversation.ToCharArray())
        {
            yield return new WaitForSeconds(0.02f);
            dialogueTextfield.text = StrOpe.i + dialogueTextfield.text + c;
        }
    }

    void SelectionStart()
    {
        if (conversationInfo.selectionStart == false)
            return;

        conversationInfo.selectionStart = false;

        foreach (ConverSationSelection selection in conversationInfo.conversationData.converSationSelectios)
        {
            Button selectButton = Instantiate(selectionButton);
            selectButton.transform.SetParent(subDialogueImage.transform);
            selectButton.transform.localScale = Vector3.one;
            Text textfield = selectButton.GetComponentInChildren<Text>();
            textfield.text = selection.text;
            selectButton.onClick.AddListener(() => OnClickSelection(selection.conversationData));
        }
        SubDialogueOpen();
    }

    void OnClickSelection(ConversationData conversationData)
    {
        conversationInfo.conversationData = conversationData;
        conversationInfo.dialoguOpened = false;
        SubDialogueClose();
    }

    void SubDialogueOpen()
    {
        continueButton.interactable = false;
        subDialogueImage.gameObject.SetActive(true);
    }

    void SubDialogueClose()
    {
        continueButton.interactable = true;
        conversationInfo.selectionStart = false;
        subDialogueImage.gameObject.SetActive(false);
        foreach (Transform child in subDialogueImage.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void DialogueOpen()
    {
        dialogueImage.gameObject.SetActive(true);
    }

    void DialogueClose()
    {
        conversationInfo.conversationStart = false;
        conversationInfo.dialoguOpened = false;
        playerInfo.moveInfo.freeze = false;
        dialogueImage.gameObject.SetActive(false);
    }
}
