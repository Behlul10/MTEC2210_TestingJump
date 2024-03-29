using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed = 20;
    float xMove;
    private Rigidbody2D rb;
    bool jumpFlag;
    public float jumpPower = 10;

    private int jumpCount = 0;
    private int maxJumpCount = 2;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount < maxJumpCount) // if isGrounded = T or jumped < 2 times
            {
                jumpFlag = true;
                jumpCount++;
                isGrounded = false; // Will be reset to true when the player touches the ground again

                /*if (jumpCount >= maxJumpCount)
                {
                    isGrounded = false; // Will be reset to true when the player touches the ground again
                }*/
            }
        }

        /*
         * First Check if your grounded or check if you are 
         * 
         */

        //transform.Translate(Time.deltaTime * speed * xMove, 0, 0);
        //rb.AddForce(Vector2.right * xMove * speed * Time.deltaTime);          // bad way of doing it, also common way
    }

    private void FixedUpdate()
    {
        //rb.AddForce(Vector2.right * xMove * speed * Time.deltaTime);
        //rb.velocity = (Vector2.right * xMove * speed * Time.deltaTime);
        rb.velocity = new Vector2(xMove * speed * Time.deltaTime, rb.velocity.y);

        if (jumpFlag)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpFlag = false; 
        }
    }
    /*
    //destroyes the coin when triggered and not collision
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count
        }
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    /*
    //trigger to detect if the player is touching the ground
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    */
}
