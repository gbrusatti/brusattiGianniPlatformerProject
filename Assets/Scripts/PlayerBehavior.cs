using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Animator knop;

    Vector2 startPos;

    private Vector2 jumpForce = new Vector2 (0, 300);

    private Rigidbody2D rb2d;
    private GameController gc;
    private SpriteRenderer sr;
    private Slider slider;

    public Animator myAnimator;

    public float Speed = 2.5f;

    private bool onGround; //Shows whether or not the player is on ground
    public bool SUp;
    public bool SDown;
    
    private float jumpSpeed; //Speed of Jump
    private float minJump; //Min speed of jump
    private float maxJumpSpeed; //Max speed of jump

    public AudioClip SplatSoundEffect;
    public AudioClip JumpSoundEffect;

    

    void Start()
    {
       
        startPos = this.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        sr = GetComponent<SpriteRenderer>();
        slider = GameObject.FindObjectOfType<Slider>();

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
                slider.value = jumpSpeed / maxJumpSpeed;
                myAnimator.SetTrigger("Jump");
                
            }

            else
            {
                if(jumpSpeed > 0f)
                {
                    jumpSpeed = jumpSpeed + minJump;
                    rb2d.velocity = new Vector2(0f, jumpSpeed);
                    jumpSpeed = 0f;
                    onGround = false;
                    slider.value = 0f;
                 
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
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            AudioSource.PlayClipAtPoint(JumpSoundEffect, Camera.main.transform.position, 1f);
            knop.SetBool("SDown", false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            knop.SetBool("SDown", true);
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameController gc = FindObjectOfType<GameController>();
        if (collision.gameObject.tag == "Platforms")
        {
            onGround = true;
                AudioSource.PlayClipAtPoint(SplatSoundEffect, Camera.main.transform.position, 1f);
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
        if (collision.gameObject.tag == "LoadScene3")
        {
            SceneManager.LoadScene(4);
        }
        if (collision.gameObject.tag == "EndGame")
            gc.WinGame();
    }
}
