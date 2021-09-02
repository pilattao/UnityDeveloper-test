using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IEnemy // Object B
{
    [SerializeField]
    private Sprite _spriteRight; // Sprite image for object B moving directions
    [SerializeField]
    private Sprite _spriteLeft; // Sprite image for object B moving directions
    [SerializeField]
    private GameObject _movementSpeedText; // Options menu parameter text

    private VelocityManager _velocityManager;

    public Vector2 EnemySpeed { get; private set; } // Speed of Object B
    public Vector2 EnemyPosition { get; private set; } // Position of Object B

    private float _speedModifier; // Modifier of enemy direction

    void Awake()
    {
        _velocityManager = GetComponent<VelocityManager>();
    }

    // Initialization of our main object B variables
    void Start()
    {
        _speedModifier = 1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0);

        StartCoroutine("Patrol");
    }

    // Movement of Object B update
    void Update()
    {
        EnemyPosition = GetComponent<Transform>().position;
        EnemySpeed = GetComponent<Rigidbody2D>().velocity;
    }

    // Changing object B movement direciton and sprite
    public IEnumerator Patrol()
    {
        for (; ; )
        {
            _speedModifier = -_speedModifier;
            _velocityManager.ChangeSpeed(GetComponent<Rigidbody2D>().velocity * -1, GetComponent<Rigidbody2D>());
            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().sprite = _spriteRight;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _spriteLeft;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    // Options menu controller methods
    public void SetMovementSpeed(float speed)
    {
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            _velocityManager.ChangeSpeed(new Vector2(speed, 0), GetComponent<Rigidbody2D>());
        }
        else
        {
            _velocityManager.ChangeSpeed(new Vector2(-speed, 0), GetComponent<Rigidbody2D>());
        }
        _movementSpeedText.GetComponent<Text>().text = speed.ToString();
    }

    public void OnHit(Collider2D hitInfo)
    {
        Debug.Log("Ouch!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit(collision);
    }
}