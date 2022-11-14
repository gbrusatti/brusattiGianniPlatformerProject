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
    private SpriteRenderer sr;

    public float Speed = 2.5f;

    private bool onGround; //Shows whether or not the player is on ground
    
    private float jumpSpeed; //Speed of Jump
    private float minJump; //Min speed of jump
    private float maxJumpSpeed; //Max speed of jump

    

    void Start()
    {
       
        startPos = this.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        sr = GetComponent<SpriteRenderer>();
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

        transform.position = playerPos;
        

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
        if (xMove >= 0.1)
        {
            sr.flipX = false;
        }
        else if (xMove <= 0.1)
        {
            sr.flipX = true;
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameController gc = FindObjectOfType<GameController>();
        if (collision.gameObject.tag == "Platforms")
        {
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
}
