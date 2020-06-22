using UnityEngine;

public class MovePlayer : MoveCharacter
{
    [SerializeField] public CharacterInfo playerInfo = null;
    [SerializeField] public ConversationDialogueInfo conversationDialogueInfo = null;
    [SerializeField] public LocationInfo currentLocation;

    protected override void Start()
    {
        playerInfo.playerInput.moveVector = Vector2.zero;
        playerInfo.npcTouching.touching = false;
        moveSpeed = playerInfo.moveSpeed;

        if(playerInfo.backFromBattle == false)
            playerInfo.moveInfo.freeze = false;

        base.Start();
    }

    protected override void FixedUpdate()
    {
        if (playerInfo.moveInfo.freeze == true)
            return;

        base.FixedUpdate();
    }

    protected override void Update()
    {
        if (playerInfo.moveInfo.freeze == false)
        {
            move = playerInfo.playerInput.moveVector;
            playerInfo.moveInfo.lastMove = lastMove;
        }

        base.Update();
    }
}
