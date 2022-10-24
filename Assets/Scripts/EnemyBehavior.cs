using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float rightDir;
    public float leftDir;

    private float direction;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = transform.position;
        if (transform.position.x > rightDir)
        {
            direction = -1;
            //sr.flipX = false; reference to the sprite renderer component
        }
        // Once the enemy goes to the max left direction, will turn around
        else if(transform.position.x < leftDir)
        {
            direction = 1;
            //sr.flipX = true; reference to the sprite renderer component
        }
        movement = direction * speed * Time.deltaTime * Vector2.right;
        transform.Translate(movement);
    }
}
