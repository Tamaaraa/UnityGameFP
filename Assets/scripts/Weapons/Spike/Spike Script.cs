using UnityEngine;

public class SpikeScript : WeaponBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        // Calculate directions
        Vector2[] directions =
        {
            Vector2.up, // North
            Vector2.right, // East
            Vector2.down, // South
            Vector2.left, // West
            (Vector2.up + Vector2.right).normalized, // Northeast
            (Vector2.up + Vector2.left).normalized, // Northwest
            (Vector2.down + Vector2.right).normalized, // Southeast
            (Vector2.down + Vector2.left).normalized // Southwest
        };

        // Instantiate projectiles in each direction
        foreach (Vector2 direction in directions)
        {
            GameObject spike = Instantiate(weaponData.WeaponPrefab);
            spike.transform.position = transform.position;
            spike.GetComponent<SpikeProjectile>().Direction(direction);
        }
    }
}
