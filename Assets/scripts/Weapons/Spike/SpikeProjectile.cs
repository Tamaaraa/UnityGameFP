using UnityEngine;

public class SpikeProjectile : ProjectileBase
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.position += currentSpeed * Time.deltaTime * direction;
    }
}
