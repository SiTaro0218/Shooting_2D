#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/// <summary>
/// Helper script to quickly create basic game prefabs for the shooting game.
/// This is an Editor script that adds a menu item to Unity.
/// Usage: In Unity Editor, go to Tools > Create Shooting Game Prefabs
/// </summary>
public class PrefabCreator : MonoBehaviour
{
    [MenuItem("Tools/Create Shooting Game Prefabs")]
    public static void CreateAllPrefabs()
    {
        if (EditorUtility.DisplayDialog("Create Prefabs", 
            "This will create basic prefabs for the shooting game. Make sure the Prefabs folder exists in Assets.", 
            "Create", "Cancel"))
        {
            CreatePlayerPrefab();
            CreatePlayerBulletPrefab();
            CreateEnemyPrefab();
            CreateEnemyBulletPrefab();
            
            EditorUtility.DisplayDialog("Success", 
                "Basic prefabs created! Check Assets/Prefabs folder.\n\n" +
                "Next steps:\n" +
                "1. Assign bullet prefabs to Player and Enemy\n" +
                "2. Drag Player prefab to scene\n" +
                "3. Assign Enemy prefab to EnemySpawner\n" +
                "4. Configure collision layers", 
                "OK");
        }
    }

    private static void CreatePlayerPrefab()
    {
        // Create player GameObject with 2D sprite
        GameObject player = new GameObject("Player");
        player.tag = "Player";
        player.layer = LayerMask.NameToLayer("Player") != -1 ? LayerMask.NameToLayer("Player") : 0;
        
        // Add SpriteRenderer for 2D
        var renderer = player.AddComponent<SpriteRenderer>();
        renderer.sprite = CreateSquareSprite();
        renderer.color = new Color(0.2f, 0.5f, 1f); // Blue
        
        // Scale
        player.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        
        // Add BoxCollider2D
        var collider = player.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        
        // Add Rigidbody2D
        var rb = player.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        
        // Add PlayerController
        var controller = player.AddComponent<PlayerController>();
        controller.moveSpeed = 5f;
        controller.fireRate = 0.2f;
        controller.maxHealth = 3;
        
        // Create FirePoint
        GameObject firePoint = new GameObject("FirePoint");
        firePoint.transform.SetParent(player.transform);
        firePoint.transform.localPosition = new Vector3(0, 0.5f, 0);
        
        // Assign FirePoint to controller
        var so = new SerializedObject(controller);
        so.FindProperty("firePoint").objectReferenceValue = firePoint.transform;
        so.ApplyModifiedProperties();
        
        // Save as prefab
        SavePrefab(player, "Assets/Prefabs/Player.prefab");
        DestroyImmediate(player);
    }

    private static void CreatePlayerBulletPrefab()
    {
        GameObject bullet = new GameObject("PlayerBullet");
        bullet.tag = "PlayerBullet";
        
        var renderer = bullet.AddComponent<SpriteRenderer>();
        renderer.sprite = CreateSquareSprite();
        renderer.color = Color.yellow;
        
        bullet.transform.localScale = new Vector3(0.2f, 0.4f, 1f);
        
        var collider = bullet.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        
        var rb = bullet.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        
        var bulletScript = bullet.AddComponent<Bullet>();
        bulletScript.speed = 10f;
        bulletScript.lifetime = 3f;
        bulletScript.damage = 1;
        bulletScript.isPlayerBullet = true;
        
        SavePrefab(bullet, "Assets/Prefabs/PlayerBullet.prefab");
        DestroyImmediate(bullet);
    }

    private static void CreateEnemyPrefab()
    {
        GameObject enemy = new GameObject("Enemy");
        enemy.tag = "Enemy";
        enemy.layer = LayerMask.NameToLayer("Enemy") != -1 ? LayerMask.NameToLayer("Enemy") : 0;
        
        var renderer = enemy.AddComponent<SpriteRenderer>();
        renderer.sprite = CreateSquareSprite();
        renderer.color = Color.red;
        
        enemy.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        
        var collider = enemy.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        
        var rb = enemy.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        
        var enemyScript = enemy.AddComponent<Enemy>();
        enemyScript.moveSpeed = 2f;
        enemyScript.moveDirection = -1f;
        enemyScript.maxHealth = 3;
        enemyScript.fireRate = 2f;
        enemyScript.canShoot = true;
        enemyScript.scoreValue = 10;
        
        GameObject firePoint = new GameObject("FirePoint");
        firePoint.transform.SetParent(enemy.transform);
        firePoint.transform.localPosition = new Vector3(0, -0.5f, 0);
        
        var so = new SerializedObject(enemyScript);
        so.FindProperty("firePoint").objectReferenceValue = firePoint.transform;
        so.ApplyModifiedProperties();
        
        SavePrefab(enemy, "Assets/Prefabs/Enemy.prefab");
        DestroyImmediate(enemy);
    }

    private static void CreateEnemyBulletPrefab()
    {
        GameObject bullet = new GameObject("EnemyBullet");
        bullet.tag = "EnemyBullet";
        
        var renderer = bullet.AddComponent<SpriteRenderer>();
        renderer.sprite = CreateSquareSprite();
        renderer.color = new Color(1f, 0.5f, 0f); // Orange
        
        bullet.transform.localScale = new Vector3(0.2f, 0.4f, 1f);
        bullet.transform.rotation = Quaternion.Euler(0, 0, 180);
        
        var collider = bullet.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        
        var rb = bullet.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        
        var bulletScript = bullet.AddComponent<Bullet>();
        bulletScript.speed = 10f;
        bulletScript.lifetime = 3f;
        bulletScript.damage = 1;
        bulletScript.isPlayerBullet = false;
        
        SavePrefab(bullet, "Assets/Prefabs/EnemyBullet.prefab");
        DestroyImmediate(bullet);
    }

    private static void SavePrefab(GameObject obj, string path)
    {
        string folderPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        
        PrefabUtility.SaveAsPrefabAsset(obj, path);
        Debug.Log($"Created prefab: {path}");
    }

    private static Sprite CreateSquareSprite()
    {
        // Create a simple 1x1 white square texture
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        
        // Create sprite from texture
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, 1, 1),
            new Vector2(0.5f, 0.5f),
            1f
        );
        
        return sprite;
    }
}
#endif
