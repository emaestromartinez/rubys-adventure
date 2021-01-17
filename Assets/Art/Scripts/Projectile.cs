using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2d;

    private float destroyTimer = 3.0f;
    private GameObject ruby;
    private Vector2 rubyPosition;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ruby = GameObject.FindGameObjectWithTag("Player");
        rubyPosition = ruby.GetComponent<Rigidbody2D>().position;
    }

    private void Update()
    {
        // Uncomment for destroy on timer;
        // destroyTimer -= Time.deltaTime;
        // if (destroyTimer < 0)
        // {
        //     Destroy(gameObject);
        // }

        if (Vector3.Distance(rigidbody2d.position, ruby.GetComponent<Rigidbody2D>().position) > 5.0f)
        {
            Destroy(gameObject);
        }


    }

    public void Launch(Vector2 direction, float force)
    {

        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.FixRobot();
        }

        Destroy(gameObject);
    }
}
