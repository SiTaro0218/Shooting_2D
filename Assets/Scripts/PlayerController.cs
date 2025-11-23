using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float boundaryPadding = 0.5f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        mainCamera = Camera.main;
        currentHealth = maxHealth;
        
        // Calculate screen bounds
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        
        // Get object size - try SpriteRenderer first, fall back to Collider
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.extents.x;
            objectHeight = spriteRenderer.bounds.extents.y;
        }
        else
        {
            // Fallback to collider bounds if no sprite renderer
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                objectWidth = collider.bounds.extents.x;
                objectHeight = collider.bounds.extents.y;
            }
            else
            {
                // Default size if no renderer or collider
                objectWidth = 0.25f;
                objectHeight = 0.25f;
            }
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Clamp position to screen bounds
        newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x + objectWidth + boundaryPadding, screenBounds.x - objectWidth - boundaryPadding);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y + objectHeight + boundaryPadding, screenBounds.y - objectHeight - boundaryPadding);

        transform.position = newPosition;
    }

    private void HandleShooting()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
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
            gameManager.GameOver();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
            
            if (collision.CompareTag("EnemyBullet"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
