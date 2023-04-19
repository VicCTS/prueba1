using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    float horizontal = 1;
    Animator anim;
    BoxCollider2D boxCollider;
    Rigidbody2D rBody;
    SFXManager sfxManager;
    SoundManager soundManager;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();

        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
    }

    public void Die()
    {
        anim.SetBool("IsDead", true);
        boxCollider.enabled = false;
        Destroy(this.gameObject, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Mario muerto");
            Destroy(collision.gameObject);
            soundManager.StopBGM();
            sfxManager.MarioDeath();
            gameManager.GameOver();
        }

        if(collision.gameObject.tag == "ColisionGoomba")
        {
            if(horizontal == 1)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "ColisionGoomba")
        {
            if(horizontal == 1)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 1;
            }
        }
    }
}
