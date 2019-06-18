using UnityEngine;
using System.Collections;

public class AImovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float distance;

    bool enemySpawner = true;
    public float waitTime = 5.0f;

    public Transform player;
    PlayerMovement playMoveScript;

    private bool movingRight = true;
    //bool patroling = true;

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
        if (enemySpawner)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playMoveScript = player.GetComponent<PlayerMovement>();

            StartCoroutine(enemyWait());

            enemySpawner = false;
        }

        if (Vector2.Distance(transform.position, player.position) < 2.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (playMoveScript.h > 0 && !movingRight)
                Flip();
            else if (playMoveScript.h < 0 && movingRight)
                Flip();
        }
        //}

        //Debug.Log(Vector2.Distance(transform.position, player.position));

        //patroling = (Vector2.Distance(transform.position, player.position) > distance) ? true : false;
    }

    void OnTriggeStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            rb2D.AddForce(Vector2.up * jumpForce);

        if (collision.tag == "Wall")
        {
            Flip();
        }
    }

    IEnumerator enemyWait()
    {
        yield return new WaitForSeconds(waitTime);
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.Rotate(0f, -180f, 0f);
    }
}
