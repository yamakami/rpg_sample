using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 lastMove;

    protected Rigidbody2D rb2;

    Animator animator;

    protected virtual void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        MoveCalculation();
    }

    protected virtual void Update()
    {
        Animation();
        LatestMove();
    }

    protected virtual void LatestMove()
    {
        if (Mathf.Abs(move.x) > 0.5f)
        {
            lastMove = new Vector2(move.x, 0f);
        }

        if (Mathf.Abs(move.y) > 0.5f)
        {
            lastMove = new Vector2(0f, move.y);
        }
    }

    protected virtual void MoveCalculation()
    {
        rb2.MovePosition(rb2.position + new Vector2(move.x, move.y).normalized * moveSpeed * Time.fixedDeltaTime);
    }

    protected virtual void Animation()
    {
        animator.SetFloat("move_x", move.x);
        animator.SetFloat("move_y", move.y);

        animator.SetFloat("moving", move.sqrMagnitude);
        animator.SetFloat("last_move_x", lastMove.x);
        animator.SetFloat("last_move_y", lastMove.y);
    }
}
