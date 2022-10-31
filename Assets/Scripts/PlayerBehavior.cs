using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    Vector2 startPos;

    private Vector2 jumpForce = new Vector2 (0, 300);

    private Rigidbody2D rb2d;
    private GameController gc;

    public float Speed = 2.5f;
    public float dashDistance = 15f; //Distance of dash
    
    private bool doubleJump = true;
    private bool jumpCooldown = false;
    private bool isDashing; //While dashing ___
    private bool onGround; //Shows whether or not the player is on ground
    private float doubleTapTime; //Double tapping a key to use the dash
    private float jumpSpeed; //Speed of Jump
    private float minJump; //Min speed of jump
    private float maxJumpSpeed; //Max speed of jump

    KeyCode lastKeyCode; //Only uses dash if you press the same key not two different keys

    void Start()
    {
       
        startPos = this.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        onGround = true;
        jumpSpeed = 0f;
        minJump = 2f;
        maxJumpSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    { 
        float xMove = Input.GetAxis("Horizontal");

        Vector3 playerPos = transform.position;
        

        playerPos.x += xMove * Speed * Time.deltaTime;

        float yMove = Input.GetAxis("Vertical");

        playerPos.y += yMove * Speed * Time.deltaTime;

        transform.position = playerPos;

        bool shouldJump = (Input.GetKeyUp(KeyCode.Space));

        if (shouldJump && !jumpCooldown)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(jumpForce);

            jumpCooldown = true;
        }
        else if (doubleJump == true && shouldJump)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(jumpForce);

            doubleJump = false;
        }

        if (onGround)
        {
            if(Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.S)))
            {
                if(jumpSpeed < maxJumpSpeed)
                {
                    jumpSpeed += Time.deltaTime*10f;
                }
                else
                {
                    jumpSpeed = maxJumpSpeed;
                }
                print(jumpSpeed);
            }

            else
            {
                if(jumpSpeed > 0f)
                {
                    jumpSpeed = jumpSpeed + minJump;
                    rb2d.velocity = new Vector2(0f, jumpSpeed);
                    jumpSpeed = 0f;
                    onGround = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) //Left Dash
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                //Dash
                StartCoroutine(Dash(-0.5f));
            }
            else
            {
               doubleTapTime = Time.time + 0.3f;
            }

            lastKeyCode = KeyCode.A;
        }
        if (Input.GetKeyDown(KeyCode.D)) //Right Dash
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                //Dash
                StartCoroutine(Dash(0.5f));
            }
            else
            {
                doubleTapTime = Time.time + 0.3f;
            }

            lastKeyCode = KeyCode.D;
        }
    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {

        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameController gc = FindObjectOfType<GameController>();
        if (collision.gameObject.tag == "Platforms")
        {
            jumpCooldown = false;
            doubleJump = true;
            onGround = true;
        }
        if(collision.gameObject.tag == "Spikes")
        {
            gc.UpdateLives();
            transform.position = startPos;

        }
        if (collision.gameObject.tag == "RedSlime")
        {
            gc.UpdateLives();
            transform.position = startPos;
        }
        if (collision.gameObject.tag == "LoadScene")
        {
            SceneManager.LoadScene(2);            
        }
        if (collision.gameObject.tag == "LoadScene2")
        {
            SceneManager.LoadScene(3);
        }
        if (collision.gameObject.tag == "EndGame")
            gc.WinGame();
    }
    IEnumerator Dash (float direction)//used for the "dash" (IEnumerator makes the dash actually dash)
    {
        isDashing = true;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
        rb2d.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb2d.gravityScale = gravity;
    }
}
