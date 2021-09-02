using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove // Movable
{
    void ChangeSpeed(Vector2 speed, Rigidbody2D rigidBody); // Changing velocity of a rigid body
}

public interface IHit // Hittable
{
    void OnHit(Collider2D hitInfo); // Collision trigger
}

public interface IRange // In Range
{
    bool ProximityCheck(Vector2 FirstTarget, Vector2 secondTarget, float Range); // Proximity of an object check
}

public interface ICooldown // Not on cooldown
{
    bool CooldownCheck(float currentTime, float cooldown); // Time cooldown check
}

public interface IDestroy : IHit // Destroyable
{
    void DestroyTrigger(GameObject obj); // Collision trigger with destroying object
}

public interface ITower : IRange // Object A Tower entity
{
    void Fire(float projectileSpeed, Rigidbody2D rigidBody, Transform firePoint); // Instantiation of projectile prefab
    Vector2 VelocityCalc(Vector2 destination, float angle, float enemySpeed, float projectileMass); // Function for finding optimal velocity of projectile
    IEnumerator Firing(); // Projectile launch cycle
}

public interface IEnemy : IHit // Object B Enemy entity
{
    IEnumerator Patrol(); // Changing object B movement direciton and sprite
}