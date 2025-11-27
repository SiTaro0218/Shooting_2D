using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public float lifeTime = 2.0f;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed;

        Destroy(gameObject, lifeTime);
    }
}
