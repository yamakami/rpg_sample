using UnityEngine;

public class MoveNpc : MoveCharacter
{ 
    public ConversationData conversationData = null;

    [SerializeField] CharacterInfo playerInfo = null;
    [SerializeField] float randomInterval = 3f;
    [SerializeField] int randomRange = 10;
    [SerializeField] GameObject restrictedCenter = default;

    float currentTime = 0f;
    CircleCollider2D areaCollider;

    const int MOVE_UP = 1;
    const int MOVE_DOWN = 2;
    const int MOVE_LEFT = 3;
    const int MOVE_RIGHT = 4;

    protected override void Start()
    {
        base.Start();
        areaCollider = restrictedCenter.GetComponent<CircleCollider2D>();
    }

    protected override void Update()
    {
        currentTime += Time.deltaTime;

        int moveType = Random.Range(0, randomRange);
        if (currentTime > randomInterval && playerInfo.npcTouching.touching == false)
        {
            switch (moveType)
            {
                case MOVE_UP:
                    move = Vector2.up; break;
                case MOVE_DOWN:
                    move = Vector2.down; break;
                case MOVE_LEFT:
                    move = Vector2.left; break;
                case MOVE_RIGHT:
                    move = Vector2.right; break;
                default:
                    move = Vector2.zero; break;
            }
            currentTime = 0f;
        }

        base.Update();
    }

    protected override void FixedUpdate()
    {
        FreezeInContact();
        MoveAvailableArea();
        base.FixedUpdate();
    }

    void MoveAvailableArea()
    {
        float distance = (transform.position - restrictedCenter.transform.position).sqrMagnitude;
        if (areaCollider.radius <= distance)
            move *= -1f;
    }

    void FreezeInContact()
    {
        if (playerInfo.npcTouching.touching == true)
        {
            rb2.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
     }
}
