using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Vector2 jumpForce = new Vector2 (0, 300);
    private Rigidbody2D rb2d;
    public float Speed = 2.5f;
    private bool beenHit = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

        bool shouldJump = (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0));

        if (shouldJump && !beenHit)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(jumpForce);
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
    }
}
