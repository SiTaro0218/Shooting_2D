using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
    // Layer 1: Ground
    public Tilemap backgroundTilemap;
    public TileBase groundTile;

    // Layer 2: Details
    public Tilemap detailTileMap;
    public TileBase waterTile;
    public TileBase mountainTile;

    public int mapWidth = 10;
    public int mapHeight = 30;
    public float Probability = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = -mapWidth / 2; x < mapWidth / 2; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                // 1. 背景タイルを配置
                backgroundTilemap.SetTile(pos, groundTile);

                // 2. 詳細タイルをランダムに配置
                if (Random.Range(0f, 1f) < Probability) // 確率で水タイルを配置
                {
                    detailTileMap.SetTile(pos, waterTile);
                }
                else if (Random.Range(0f, 1f) < Probability) // 確率で山タイルを配置
                {
                    detailTileMap.SetTile(pos, mountainTile);
                }
            }
        }
    }
}
