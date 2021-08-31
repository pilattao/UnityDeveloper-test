using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // Object B
{
    public Sprite sprite_right; // Sprite images for object B moving directions
    public Sprite sprite_left;

    SpriteRenderer spriteRenderer;
    float enemyVelocity; // Speed of object B
    Rigidbody2D enemyRB;
    bool MoveRight; // Bool variable for movement direction toggle
    bool sprite_isRight; // Bool variables for sprite direction toggle
    bool sprite_isLeft;

    // Using awake to get this variables set before start
    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyVelocity = 1.5f;
        enemyRB.velocity = new Vector2(-enemyVelocity, 0.0f);
    }

    // Start is called before the first frame update, initialization of our main object B variables
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("Patrol");
    }

    // Update is called once per frame, movement of object B and sprite changes
    void Update()
    {
        if (MoveRight)
        {
            sprite_isLeft = false;
            //transform.Translate(2 * Time.deltaTime * enemyVelocity, 0, 0);
            enemyRB.velocity = new Vector2(enemyVelocity, 0.0f);
            if(!sprite_isRight)
            {
                spriteRenderer.sprite = sprite_right;
                sprite_isRight = true;
            }
        }
        else
        {
            sprite_isRight = false;
            //transform.Translate(-2 * Time.deltaTime * enemyVelocity, 0, 0);
            enemyRB.velocity = new Vector2(-enemyVelocity, 0.0f);
            if (!sprite_isLeft)
            {
                spriteRenderer.sprite = sprite_left;
                sprite_isLeft = true;
            }
        }
    }

    //Changing object B movement direciton
    IEnumerator Patrol()
    {
        for(; ; )
        {
            MoveRight = !MoveRight;
            yield return new WaitForSeconds(3f);
        }
    }
}