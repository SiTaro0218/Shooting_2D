using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveDirection = -1f; // -1 for downward movement

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;
    public bool canShoot = true;

    [Header("Score")]
    public int scoreValue = 10;

    private void Start()
    {
        currentHealth = maxHealth;
        nextFireTime = Time.time + Random.Range(0f, fireRate);
    }

    private void Update()
    {
        Move();
        
        if (canShoot && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // Destroy if moved off screen (bottom)
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.up * moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddScore(scoreValue);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
            Die();
        }
    }
}
