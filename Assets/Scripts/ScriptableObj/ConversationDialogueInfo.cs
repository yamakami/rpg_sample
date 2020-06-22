using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConversationDialogueInfo", menuName = "ScriptableObject/ConversationDialogueInfo", order = 1)]
public class ConversationDialogueInfo : ScriptableObject
{
    public MoveNpc conversationOwner;
    public bool conversationStart = false;
    public bool dialoguOpened = false;
    public bool selectionStart    = false;
    public ConversationData conversationData;
}
