using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLaser : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //destroy the projectile when it reach a distance of 1000.0f from the origin
        if (transform.position.magnitude > 10000.0f)
        {
            //Destroy(gameObject);
            return;
        }
            
    }

    //called by the player controller after it instantiate a new projectile to launch it.
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();

        //if the object we touched wasn't an enemy, just destroy the projectile.
        if (e != null)
        {
            e.Fix();
        }

        Destroy(gameObject);
    }


}
