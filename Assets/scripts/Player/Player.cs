using UnityEngine;

public class Player : MonoBehaviour
{
    // moving etc.
    public Vector2 move;
    public Vector2 lastMove;
    public Animator animator;

    // Stats
    public Rigidbody2D rb;
    private PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        lastMove = new Vector2(1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (move.x != 0)
        {
            lastMove = new Vector2(move.x, 0f);
        }
        if (move.y != 0)
        {
            lastMove = new Vector2(0f, move.y);
        }
        if (move.x != 0 && move.y != 0)
        {
            lastMove = move;
        }

        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
    }

    void FixedUpdate()
    {
        rb.velocity = move * player.currentSpeed;
    }
}
