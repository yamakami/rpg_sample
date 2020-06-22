using UnityEngine;

public class ContactTrigger : AssignPlayerProperty
{
    public float rayDistance = 0.8f;
    public string targetLayer;
    public string targetTag;

    void Update()
    {
        ChangeIsEnteredStatus();
    }
    
    void ChangeIsEnteredStatus()
    {
        bool up    = CheckCharacterContact(transform.up, rayDistance);
        bool upLeft = CheckCharacterContact(transform.up + -transform.right, rayDistance / 1.5f);
        bool upRight = CheckCharacterContact(transform.up + transform.right, rayDistance / 1.5f);
        bool down  = CheckCharacterContact(-transform.up, rayDistance);
        bool downLeft = CheckCharacterContact(-transform.up + -transform.right, rayDistance / 1.5f);
        bool downRight = CheckCharacterContact(-transform.up + transform.right, rayDistance / 1.5f);
        bool left  = CheckCharacterContact(-transform.right, rayDistance);
        bool right = CheckCharacterContact(transform.right, rayDistance);

        if (up == false && down == false && left == false && right == false
        && upLeft == false && upRight == false && downLeft == false && downRight == false)
        {
            playerInfo.npcTouching.touching = false;
            return;
        }
        playerInfo.npcTouching.touching = true;
    }

    bool CheckCharacterContact(Vector2 direction, float distansce)
    {
        LayerMask layerMask = LayerMask.NameToLayer(targetLayer);
        Vector2 target      = (Vector2)transform.position + (direction * distansce);
        RaycastHit2D hit    = Physics2D.Raycast(target, transform.position, layerMask);

        Debug.DrawLine(transform.position, target, Color.blue);
        if (hit.collider.CompareTag(targetTag) == true)
        {
            conversationDialogueInfo.conversationOwner = hit.collider.GetComponent<MoveNpc>();
            return true;
        }
        return false;
    }
}