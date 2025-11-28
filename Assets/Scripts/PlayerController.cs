using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    float cameraScrollSpeed = 2.0f;
    public Vector2 margin = new Vector2(0.5f, 0.5f); // 画面端からの余白割合

    Rigidbody2D rb;
    Vector2 moveDirection;
    Camera mainCamera;

    [Header("Shot Settings")]
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform firePos;       // 弾が出る位置

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        // メインカメラからCameraControllerを取得し，スクロール速度をもらう
        mainCamera = Camera.main;
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

        // GetKeyDown: 押した瞬間だけ反応（連打が必要）
        // GetKey: 押しっぱなしで反応（連射になる）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        Vector2 finalVelocity = moveDirection * moveSpeed;
        finalVelocity.y += cameraScrollSpeed; // カメラのスクロール速度を加算
        rb.linearVelocity = finalVelocity;
    }

    void LateUpdate()
    {
        ClampPosition();
    }

    void Fire()
    {
        // firePosが設定されていなければ、自機の位置を使う
        Vector3 spawnPosition = (firePos != null) ? firePos.position : transform.position;

        // 弾を生成する (原本, 場所, 回転)
        Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    }

    void ClampPosition()
    {
        if (mainCamera == null) return;
        // プレイヤーの現在位置
        Vector3 pos = transform.position;
        // 画面の端のワールド座標を取得
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.25f, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(0.75f, 1, 0));
        // プレイヤーの位置をクランプ
        float clampedX = Mathf.Clamp(pos.x, bottomLeft.x + margin.x, topRight.x - margin.x);
        float clampedY = Mathf.Clamp(pos.y, bottomLeft.y + margin.y, topRight.y - margin.y);
        transform.position = new Vector3(clampedX, clampedY, pos.z);
    }
}