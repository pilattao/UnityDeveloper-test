using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float speed;
    public bool MoveRight;
    public Sprite sprite_right;
    public bool sprite_isRight;
    public Sprite sprite_left;
    public bool sprite_isLeft;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Patrol");
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveRight)
        {
            sprite_isLeft = false;
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            if(!sprite_isRight)
            {
                spriteRenderer.sprite = sprite_right;
                sprite_isRight = true;
            }
        }
        else
        {
            sprite_isRight = false;
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            if (!sprite_isLeft)
            {
                spriteRenderer.sprite = sprite_left;
                sprite_isLeft = true;
            }
        }
    }

    IEnumerator Patrol()
    {
        for(; ; )
        {
            MoveRight = !MoveRight;
            yield return new WaitForSeconds(3f);
        }
    }
}