using UnityEngine;

public class KnifeScript : WeaponBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject knife = Instantiate(weaponData.WeaponPrefab);
        knife.transform.position = transform.position;
        knife.GetComponent<KnifeProjectile>().Direction(player.lastMove);
    }
}
