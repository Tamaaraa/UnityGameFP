using UnityEngine;

public class KnifeProjectile : ProjectileBase
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
