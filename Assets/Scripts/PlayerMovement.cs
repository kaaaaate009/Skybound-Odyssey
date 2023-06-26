using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rgBdy;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D colid;


    private float drX = 0;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 18f;

    private enum PlayerMovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    private void Start()
    {
        rgBdy = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colid = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        drX = Input.GetAxisRaw("Horizontal");
        rgBdy.velocity = new Vector2(drX * moveSpeed, rgBdy.velocity.y);

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            jumpSound.Play();
            rgBdy.velocity = new Vector2(rgBdy.velocity.x, jumpForce);
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        PlayerMovementState state;

        if (drX > 0f)
        {
            state = PlayerMovementState.running;
            sprite.flipX = false;
        }
        else if (drX < 0f)
        {
            state = PlayerMovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = PlayerMovementState.idle;
        }

        if (rgBdy.velocity.y > .1f)
        {
            state = PlayerMovementState.jumping;
        }
        else if (rgBdy.velocity.y < -.1f)
        {
            state = PlayerMovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(colid.bounds.center, colid.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
