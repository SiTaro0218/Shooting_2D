using UnityEngine;

public class CameraController : MonoBehaviour
{
    // スクロール速度 (Inspectorから調整できるようにpublicにする)
    public float scrollSpeed = 2.0f;

    // ゲームが進行中かどうかのフラグ
    private bool isScrolling = true;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 初期状態として、カメラはスクロールを開始する
        isScrolling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScrolling)
        {
            // 現在のカメラの位置を取得
            Vector3 currentPosition = transform.position;

            // Y軸方向に scrollSpeed * 1秒間の経過時間 分だけ移動
            // Time.deltaTimeを使うことで、フレームレートに関わらず一定速度で移動できる
            currentPosition.y += scrollSpeed * Time.deltaTime;

            // カメラの位置を更新
            transform.position = currentPosition;
        }
    }

    // スクロールを停止・再開するためのメソッド（GameManagerから呼び出す想定）
    public void SetScrolling(bool scrolling)
    {
        isScrolling = scrolling;
    }
}