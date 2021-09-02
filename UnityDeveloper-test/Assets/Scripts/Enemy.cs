using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour // Object B
{
    [SerializeField]
    private Sprite _spriteRight; // Sprite images for object B moving directions
    [SerializeField]
    private Sprite _spriteLeft; // Sprite images for object B moving directions
    [SerializeField]
    private GameObject _movementSpeedText;

    public float EnemySpeed { get; private set; } // Speed of object B

    private bool _moveRight; // Bool variable for movement direction toggle
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _enemyRB;
    private bool _spriteIsRight; // Bool variables for sprite direction toggle
    private bool _spriteIsLeft;

    // Using awake to get this variables set before start
    void Awake()
    {
        EnemySpeed = 1.5f;
    }

    // Start is called before the first frame update, initialization of our main object B variables
    void Start()
    {
        _enemyRB = GetComponent<Rigidbody2D>();
        _enemyRB.velocity = new Vector2(-EnemySpeed, 0.0f);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine("Patrol");
    }

    // Update is called once per frame, movement of object B and sprite changes
    void Update()
    {
        if (_moveRight)
        {
            _spriteIsLeft = false;
            _enemyRB.velocity = new Vector2(EnemySpeed, 0.0f);
            if(!_spriteIsRight)
            {
                _spriteRenderer.sprite = _spriteRight;
                _spriteIsRight = true;
            }
        }
        else
        {
            _spriteIsRight = false;
            _enemyRB.velocity = new Vector2(-EnemySpeed, 0.0f);
            if (!_spriteIsLeft)
            {
                _spriteRenderer.sprite = _spriteLeft;
                _spriteIsLeft = true;
            }
        }
    }

    //Changing object B movement direciton
    IEnumerator Patrol()
    {
        for(; ; )
        {
            _moveRight = !_moveRight;
            yield return new WaitForSeconds(3f);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        EnemySpeed = speed;
        _movementSpeedText.GetComponent<Text>().text = speed.ToString();
    }
}