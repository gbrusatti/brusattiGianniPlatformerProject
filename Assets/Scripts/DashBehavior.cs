using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehavior : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameController gc;

    public float dashDistance = 15f; //Distance of dash

    private float doubleTapTime; //Double tapping a key to use the dash

    private bool isDashing; //While dashing ___
    private bool onGround; //Shows whether or not the player is on ground

    KeyCode lastKeyCode; //Only uses dash if you press the same key not two different keys

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
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
    IEnumerator Dash(float direction)//used for the "dash" (IEnumerator makes the dash actually dash)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platforms")
        {

        }
    }
}
