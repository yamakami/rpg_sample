using UnityEngine;

public class PlayerInput : AssignPlayerProperty
{
    void Update()
    {
        playerInfo.playerInput.moveVector = new Vector2(
                                                        Input.GetAxisRaw("Horizontal"),
                                                        Input.GetAxisRaw("Vertical"));

        playerInfo.playerInput.fire = Input.GetKeyDown(KeyCode.Space);
    }
}