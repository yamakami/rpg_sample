using UnityEngine;

[CreateAssetMenu(fileName = "New Conversations", menuName = "ScriptableObject/Conversations", order = 2)]
public class ConversationData : ScriptableObject
{
    public ConversationLine[] conversationLines;
    public ConverSationSelection[] converSationSelectios;
}

[System.Serializable]
public struct ConversationLine
{
    [TextArea(2, 5)]
    public string text;
}

[System.Serializable]
public struct ConverSationSelection
{
    [TextArea(2, 5)]
    public string text;
    public ConversationData conversationData;
}