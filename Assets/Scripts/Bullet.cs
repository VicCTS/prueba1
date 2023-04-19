using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rBody2D;

    public float bulletSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rBody2D = GetComponent<Rigidbody2D>();

        rBody2D.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            Enemy enemyScript = collider.gameObject.GetComponent<Enemy>();
            enemyScript.Die();
        }

        if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Coin" || collider.gameObject.tag == "Bullet")
        {
            return;
        }

        Destroy(this.gameObject);
    }
}
