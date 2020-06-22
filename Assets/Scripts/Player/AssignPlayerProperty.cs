using UnityEngine;

public class AssignPlayerProperty : MonoBehaviour
{
    protected CharacterInfo playerInfo = null;
    protected ConversationDialogueInfo conversationDialogueInfo = null;

    void Start()
    {
        MovePlayer movePlayer = gameObject.GetComponent<MovePlayer>();

        playerInfo = movePlayer.playerInfo;
        conversationDialogueInfo = movePlayer.conversationDialogueInfo;
    }
}
