using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    float cameraScrollSpeed = 2.0f;

    Rigidbody2D rb;
    Vector2 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        // メインカメラからCameraControllerを取得し，スクロール速度をもらう
        GameObject mainCamera = Camera.main.gameObject;
        if (mainCamera != null)
        {
            var cameraController = mainCamera.GetComponent<CameraController>();
            if (cameraController != null)
            {
                cameraScrollSpeed = cameraController.scrollSpeed;
            }
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized; // 正規化して斜め移動時の速度低下を防ぐ
    }

    void FixedUpdate()
    {
        Vector2 finalVelocity = moveDirection * moveSpeed;
        finalVelocity.y += cameraScrollSpeed; // カメラのスクロール速度を加算
        rb.linearVelocity = finalVelocity;
    }
}