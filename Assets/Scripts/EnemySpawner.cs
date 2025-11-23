using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;
    public float spawnRate = 2f;
    public float spawnRangeX = 8f;
    public float spawnY = 6f;
    
    [Header("Wave Settings")]
    public bool enableWaves = false;
    public int enemiesPerWave = 10;
    public float timeBetweenWaves = 5f;
    
    private float nextSpawnTime = 0f;
    private int enemiesSpawnedInWave = 0;
    private bool isSpawning = true;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        nextSpawnTime = Time.time + spawnRate;
    }

    private void Update()
    {
        if (!isSpawning || gameManager == null || gameManager.IsGameOver())
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
            
            if (enableWaves)
            {
                enemiesSpawnedInWave++;
                
                if (enemiesSpawnedInWave >= enemiesPerWave)
                {
                    StartCoroutine(WaveCooldown());
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            return;

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private System.Collections.IEnumerator WaveCooldown()
    {
        isSpawning = false;
        enemiesSpawnedInWave = 0;
        
        yield return new WaitForSeconds(timeBetweenWaves);
        
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
