using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float speed;

    public Transform gunPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //gunPoint = gameObject.transform.Find("GunPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        Vector2 pos = transform.position;
        bool flipSprite;

        pos.x = pos.x + speed * horiz;

        flipSprite = spriteRenderer.flipX ? (horiz > 0f) : (horiz < 0f);

        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            //gunPoint.transform.Rotate(0f, 180f, 0f);
            //gunPoint.transform.position = new Vector2(0.12f, -0.005f);
        }

        animator.SetInteger("Speed", (int)horiz);
        
        transform.position = pos;
    }*/

    [HideInInspector]
    public bool facingRight = true;
    //[HideInInspector]
    public bool jump = false;
    public float Speed;
    public float jumpForce;
    public Transform groundCheck;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButton("Jump") && grounded)
            jump = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 pos = transform.position;

        anim.SetInteger("Speed", (int)h);

        pos.x = pos.x + Speed * h;

        transform.position = pos;

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (!facingRight)
            anim.SetInteger("Speed", (int)h);

        if (jump)
        {
            grounded = false;
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, -180f, 0f);
    }
}
