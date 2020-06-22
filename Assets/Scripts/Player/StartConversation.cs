using UnityEngine;

public class StartConversation : AssignPlayerProperty
{
    void Update()
    {
        ConversationStart();
    }

    void ConversationStart()
    {
        if (conversationDialogueInfo.conversationStart == true)
            return;

        if (playerInfo.npcTouching.touching == false)
            return;

        if (playerInfo.playerInput.fire == false)
            return;

        conversationDialogueInfo.conversationStart = true;
        conversationDialogueInfo.conversationData = conversationDialogueInfo.conversationOwner.conversationData;

        playerInfo.moveInfo.freeze = true;
        NpcFacingTo(conversationDialogueInfo.conversationOwner, playerInfo);
    }

    void NpcFacingTo(MoveNpc npc, CharacterInfo playerInfo)
    {
        npc.move = Vector2.zero;
        npc.lastMove = playerInfo.moveInfo.lastMove * -1;
    }
}
