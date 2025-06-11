using UnityEngine;

public class PlayerPickups : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D collectionRange;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        collectionRange = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        collectionRange.radius = player.currentPickUpRange;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IPickupInterface collectible))
        {
            collectible.Collect();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IPickupInterface collectible))
        {
            col.transform.position = Vector2.MoveTowards(
                col.transform.position,
                player.transform.position,
                35 * Time.deltaTime
            );
        }
    }
}
