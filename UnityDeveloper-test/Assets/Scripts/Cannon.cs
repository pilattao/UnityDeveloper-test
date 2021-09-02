using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour, ITower// Object A
{
    [SerializeField]
    private GameObject _projectile; // Projectile prefab
    [SerializeField]
    private GameObject _cannonAngleText; // Options menu parameter text
    [SerializeField]
    private GameObject _sprayModifierText; // Options menu parameter text
    [SerializeField]
    private GameObject _towerRangeText; // Options menu parameter text
    [SerializeField]
    private GameObject _cannonCooldownText; // Options menu parameter text

    private VelocityManager _velocityManager;

    private GameObject _enemy; // Object B
    private GameObject _firePoint; // Object A
    private Vector2 _enemyPosition; // Current position of enemy (object B)
    private Vector2 _enemySpeed; // Speed of object B
    private float _shootingRange; // Object A detection range
    private float _projectileWeight; // Mass of projectile
    private float _cannonAngle; // Angle for projectile launch
    private float _projectileSpeed; // Velocity for projectile launch
    private float _cannonNextShot; // Time for next shot
    private float _cannonCooldown; // Cooldown object A cannon
    private float _projectileSpray; // Projectile spray modifier

    private void Awake()
    {
        _velocityManager = GetComponent<VelocityManager>();
    }

    // Initialization of our main object A variables
    private void Start()
    {
        _cannonAngle = 45;
        _projectileSpray = 1;
        _shootingRange = 13;
        _cannonCooldown = 0.0f;

        _cannonAngleText.GetComponent<Text>().text = _cannonAngle.ToString();
        _sprayModifierText.GetComponent<Text>().text = _projectileSpray.ToString();
        _towerRangeText.GetComponent<Text>().text = _shootingRange.ToString();
        _cannonCooldownText.GetComponent<Text>().text = _cannonCooldown.ToString();

        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _firePoint = gameObject;
        _projectileWeight = _projectile.GetComponent<Rigidbody2D>().mass;
        _cannonNextShot = Time.time;

        StartCoroutine("Firing");
    }

    // Projectile launch cycle
    public IEnumerator Firing()
    {
        for (; ; )
        {
            _enemyPosition = _enemy.GetComponent<Enemy>().EnemyPosition;
            if (ProximityCheck(_enemy.transform.position, _firePoint.transform.position, _shootingRange))
            {
                if (CooldownCheck(Time.time, _cannonNextShot))
                {
                    _enemyPosition = _enemy.GetComponent<Enemy>().EnemyPosition;
                    _enemySpeed = _enemy.GetComponent<Enemy>().EnemySpeed;
                    _firePoint.transform.rotation = Quaternion.Euler(0, 0, _cannonAngle);
                    float projectileSpeedX = VelocityCalc(_enemyPosition, _cannonAngle, _enemySpeed.x, _projectileWeight).x;
                    float projectileSpeedY = VelocityCalc(_enemyPosition, _cannonAngle, _enemySpeed.y, _projectileWeight).y;
                    _projectileSpeed = Mathf.Sqrt((projectileSpeedX * projectileSpeedX) + (projectileSpeedY * projectileSpeedY));
                    Fire(_projectileSpeed, _projectile.GetComponent<Rigidbody2D>(), _firePoint.transform);
                    _cannonNextShot = Time.time + _cannonCooldown;
                }
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    // Instatiating projectile prefab
    public void Fire(float _projectileSpeed, Rigidbody2D rigidBody, Transform firePoint)
    {
        Rigidbody2D p = Instantiate(rigidBody, firePoint.transform.position, firePoint.transform.rotation);
        _velocityManager.ChangeSpeed(transform.right * _projectileSpeed, p);
    }

    // Proximity of an object check
    public bool ProximityCheck(Vector2 firstTarget, Vector2 secondTarget, float Range)
    {
        if (Vector2.Distance(_enemy.transform.position, _firePoint.transform.position) <= Range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Time cooldown check
    public bool CooldownCheck(float currentTime, float cooldown)
    {
        if (currentTime > cooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Function for finding optimal velocity of projectile
    public Vector2 VelocityCalc(Vector2 destination, float angle, float enemySpeed, float projectileMass)
    {
        Vector2 dir = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
        float height = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += height / Mathf.Tan(a);
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        float time = (velocity * 2 * Mathf.Sin(a * 2f)) / (Physics2D.gravity.y + projectileMass);
        if (enemySpeed > 0)
        {
            dir = new Vector2((destination.x - enemySpeed * Mathf.Tan(a * 1.5f) * time) * Random.Range(1, _projectileSpray) - transform.position.x, destination.y - transform.position.y);
        }
        else
        {
            dir = new Vector2((destination.x - enemySpeed * Mathf.Tan(a) * time) * Random.Range(1, _projectileSpray) - transform.position.x, destination.y - transform.position.y);
        }
        height = dir.y;
        dir.y = 0;
        dist = dir.magnitude;
        a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += height / Mathf.Tan(a);
        velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized;
    }

    // Options menu controller methods
    public void SetCannonAngle(float angle)
    {
        _cannonAngle = angle;
        _cannonAngleText.GetComponent<Text>().text = angle.ToString();
    }

    public void SetSprayModifier(float spraymod)
    {
        _projectileSpray = spraymod;
        _sprayModifierText.GetComponent<Text>().text = spraymod.ToString();
    }

    public void SetTowerRange(float range)
    {
        _shootingRange = range;
        _towerRangeText.GetComponent<Text>().text = range.ToString();
    }

    public void SetCannonCooldown(float cooldown)
    {
        _cannonCooldown = cooldown;
        _cannonCooldownText.GetComponent<Text>().text = cooldown.ToString();
    }
}
