using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    [SerializeField] private float moveSpeed = 5;
    private bool vertical;

    [SerializeField] private ParticleSystem smokeEffect;


    public float changeTime = 3.0f;
    private float timer;
    private int direction = 1;
    private bool broken = true;
    Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);

            position.y = position.y + Time.deltaTime * moveSpeed * direction;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);

            position.x = position.x + Time.deltaTime * moveSpeed * direction;
        }

        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    public void FixRobot()
    {
        broken = false;
        rigidbody2D.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
    }


}
