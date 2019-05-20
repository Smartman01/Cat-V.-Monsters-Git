using UnityEngine;

public class AImovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float distance;

    Transform player;
    PlayerMovement playMoveScript;

    private bool movingRight = true;
    bool patroling = true;

    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playMoveScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patroling)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (playMoveScript.h > 0 && !movingRight)
                Flip();
            else if (playMoveScript.h < 0 && movingRight)
                Flip();
        }

        //Debug.Log(Vector2.Distance(transform.position, player.position));

        patroling = (Vector2.Distance(transform.position, player.position) > distance) ? true : false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            rb2D.AddForce(Vector2.up * jumpForce);

        if (collision.tag == "Wall")
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.Rotate(0f, -180f, 0f);
    }
}
