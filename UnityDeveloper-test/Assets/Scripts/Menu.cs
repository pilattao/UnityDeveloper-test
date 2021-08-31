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

    public void SetMovementSpeed(float speed)
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        enemy.enemySpeed = speed;
        MovementSpeedText.GetComponent<Text>().text = speed.ToString();
    }

    public void SetCannonAngle(float angle)
    {
        Cannon cannon = FindObjectOfType<Cannon>();
        cannon.cannonAngle = angle;
        CannonAngleText.GetComponent<Text>().text = angle.ToString();
    }

    public void SetSprayModifier(float spraymod)
    {
        Cannon cannon = FindObjectOfType<Cannon>();
        cannon.projectileSpray = spraymod;
        SprayModifierText.GetComponent<Text>().text = spraymod.ToString();
    }

    public void SetTowerRange(float range)
    {
        Cannon cannon = FindObjectOfType<Cannon>();
        cannon.shootingRange = range;
        TowerRangeText.GetComponent<Text>().text = range.ToString();
    }

    public void SetCannonCooldown(float cooldown)
    {
        Cannon cannon = FindObjectOfType<Cannon>();
        cannon.cannonCooldown = cooldown;
        CannonCooldownText.GetComponent<Text>().text = cooldown.ToString();
    }
}
