using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    float timer;
    protected Player player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        timer = weaponData.AttackRate;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        timer = weaponData.AttackRate;
    }
}
