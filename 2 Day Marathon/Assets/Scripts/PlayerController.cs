using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //---Movement---
    public float speed = 4;
    Rigidbody2D rigidbody2d;
    Vector2 currentInput;

    //---PROJECTILE---
    public GameObject projectilePrefab;

    //---Status---


    //---ANIMATION---
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d= GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput();
        LaunchProjectile();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void LaunchProjectile()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            SmallLaser projectile = projectileObject.GetComponent<SmallLaser>();
            projectile.Launch(lookDirection, 300);

            //animator.SetTrigger("Launch");
            //audioSource.PlayOneShot(shootingSound);
        }

    }

    private void PlayerMovement()
    {
        Vector2 position = rigidbody2d.position;

        position = position + currentInput * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    private void PlayerMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        currentInput = move;
    }
}
