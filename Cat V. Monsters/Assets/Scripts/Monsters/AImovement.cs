using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private bool movingRight = true;

    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            rb2D.AddForce(Vector2.up * jumpForce);

        if (collision.tag == "Wall")
        {
            if (movingRight)
            {
                transform.Rotate(0f, -180f, 0f);
                movingRight = false;
            }
            else
            {
                transform.Rotate(0f, -180f, 0f);
                movingRight = true;
            }
        }
    }
}
