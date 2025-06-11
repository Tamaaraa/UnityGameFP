using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Vector2Int tilePos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<MapScroll>().Add(gameObject, tilePos);
    }

    // Update is called once per frame
    void Update() { }
}
