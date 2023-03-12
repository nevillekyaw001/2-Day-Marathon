using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ====== ENEMY MOVEMENT ========
    public float speed;
    public float timeToChange;
    public bool horizontal;

    //public GameObject smokeParticleEffect;
    //public ParticleSystem fixedParticleEffect;

    //public AudioClip hitSound;
    //public AudioClip fixedSound;

    Rigidbody2D rigidbody2d;
    float remainingTimeToChange;
    Vector2 direction = Vector2.right;
    bool repaired = false;

    // ===== ANIMATION ========
    Animator animator;

    // ================= SOUNDS =======================
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        remainingTimeToChange = timeToChange;

        direction = horizontal ? Vector2.right : Vector2.down;

        //animator = GetComponent<Animator>();

        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (repaired)
        {
            return;
        }
  

        remainingTimeToChange -= Time.deltaTime;

        if (remainingTimeToChange <= 0)
        {
            remainingTimeToChange += timeToChange;
            direction *= -1;
        }

        //animator.SetFloat("ForwardX", direction.x);
        //animator.SetFloat("ForwardY", direction.y);
    }

    void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (repaired)
            return;

        PlayerController controller = other.collider.GetComponent<PlayerController>();

        if (controller != null)
        {
            //controller.ChangeHealth(-1);
            return;
        }

    }

    public void Fix()
    {
        animator.SetTrigger("Fixed");
        repaired = true;

        //smokeParticleEffect.SetActive(false);

        //Instantiate(fixedParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        //we don't want that enemy to react to the player or bullet anymore, remove its reigidbody from the simulation
        rigidbody2d.simulated = false;

        /*audioSource.Stop();
        audioSource.PlayOneShot(hitSound);
        audioSource.PlayOneShot(fixedSound);*/
    }
}
