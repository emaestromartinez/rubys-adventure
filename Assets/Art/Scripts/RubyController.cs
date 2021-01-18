using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int maxHealth = 5;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private ParticleSystem onceSmokeEffect;

    public float timeInvincible = 2.0f;
    float horizontal;
    float vertical;
    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    private bool launchKeyPressed = false;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                launchKeyPressed = true;

            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        position.y = position.y + moveSpeed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (launchKeyPressed)
        {
            Launch();
            launchKeyPressed = false;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            Instantiate(onceSmokeEffect, rigidbody2d.position, Quaternion.identity);
            Instantiate(onceSmokeEffect, rigidbody2d.position, Quaternion.identity);
            Instantiate(onceSmokeEffect, rigidbody2d.position, Quaternion.identity);
            Instantiate(onceSmokeEffect, rigidbody2d.position, Quaternion.identity);
            Instantiate(onceSmokeEffect, rigidbody2d.position, Quaternion.identity);
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public GameObject projectilePrefab;
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

}
