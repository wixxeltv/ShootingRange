using System;
using System.Collections;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Bullets bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;
    [SerializeField] public float fireRate;
    public GameObject shootEffect;
    public Rigidbody objectRigidBody = null;
    public AudioSource shotSound;
    protected bool canFire = true;

    private void Start()
    {
        canFire = true;
    }

    protected IEnumerator fire()
    {
        canFire = false;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    void Shoot()
    {
        if (shootEffect != null)
        {
            shotSound.PlayOneShot(shotSound.clip);
            Instantiate(shootEffect, shootPoint.position, Quaternion.identity);
        }
        Bullets bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Vector3 initialForce = shootPoint.forward * bulletSpeed;
        if (objectRigidBody != null)
        {
            initialForce += objectRigidBody.linearVelocity;
        }

        bullet.rb.AddForce(initialForce, ForceMode.Impulse);
    }

    
}