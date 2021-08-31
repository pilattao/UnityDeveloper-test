using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MovementSpeedText;
    public GameObject CannonAngleText;
    public GameObject SprayModifierText;
    public GameObject TowerRangeText;
    public GameObject CannonCooldownText;
    Enemy enemy;
    Cannon cannon;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        cannon = FindObjectOfType<Cannon>();

        enemy.enemySpeed = 3;
        MovementSpeedText.GetComponent<Text>().text = enemy.enemySpeed.ToString();

        cannon.cannonAngle = 45;
        CannonAngleText.GetComponent<Text>().text = cannon.cannonAngle.ToString();

        cannon.projectileSpray = 1;
        SprayModifierText.GetComponent<Text>().text = cannon.projectileSpray.ToString();

        cannon.shootingRange = 13;
        TowerRangeText.GetComponent<Text>().text = cannon.shootingRange.ToString();

        cannon.cannonCooldown = 0;
        CannonCooldownText.GetComponent<Text>().text = cannon.cannonCooldown.ToString();
    }

    public void SetMovementSpeed(float speed)
    {
        enemy.enemySpeed = speed;
        MovementSpeedText.GetComponent<Text>().text = speed.ToString();
    }

    public void SetCannonAngle(float angle)
    {
        cannon.cannonAngle = angle;
        CannonAngleText.GetComponent<Text>().text = angle.ToString();
    }

    public void SetSprayModifier(float spraymod)
    {
        cannon.projectileSpray = spraymod;
        SprayModifierText.GetComponent<Text>().text = spraymod.ToString();
    }

    public void SetTowerRange(float range)
    {
        cannon.shootingRange = range;
        TowerRangeText.GetComponent<Text>().text = range.ToString();
    }

    public void SetCannonCooldown(float cooldown)
    {
        cannon.cannonCooldown = cooldown;
        CannonCooldownText.GetComponent<Text>().text = cooldown.ToString();
    }
}
