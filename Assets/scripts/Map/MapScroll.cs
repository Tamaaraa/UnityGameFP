using UnityEngine;

public class MapScroll : MonoBehaviour
{
    public Transform player;
    public Vector2Int currentPos;
    private Vector2Int playerPos;
    public float tileSize = 20f;
    GameObject[,] tiles;

    public int HTiles;
    public int VTiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[HTiles, VTiles];
    }

    // Update is called once per frame
    void Update()
    {
        playerPos.x = (int)(player.position.x / tileSize);
        playerPos.y = (int)(player.position.y / tileSize);

        if (player.position.x < 0)
        {
            playerPos.x -= 1;
        }
        if (player.position.y < 0)
        {
            playerPos.y -= 1;
        }

        if (currentPos != playerPos)
        {
            currentPos = playerPos;
            UpdateGrid();
        }
    }

    private void UpdateGrid()
    {
        foreach (GameObject tile in tiles)
        {
            if ((int)(tile.transform.position.x / tileSize) - playerPos.x == -2)
            {
                tile.transform.position += Vector3Int.right * 60;
            }
            else if ((int)(tile.transform.position.x / tileSize) - playerPos.x == 2)
            {
                tile.transform.position += Vector3Int.left * 60;
            }
            else if ((int)(tile.transform.position.y / tileSize) - playerPos.y == -2)
            {
                tile.transform.position += Vector3Int.up * 60;
            }
            else if ((int)(tile.transform.position.y / tileSize) - playerPos.y == 2)
            {
                tile.transform.position += Vector3Int.down * 60;
            }
        }
    }

    internal void Add(GameObject tile, Vector2Int tilePos)
    {
        tiles[tilePos.x, tilePos.y] = tile;
    }
}
