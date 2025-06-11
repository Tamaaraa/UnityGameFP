using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float duration;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentAttackRate;
    protected float currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentAttackRate = weaponData.AttackRate;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, duration);
    }

    public void Direction(Vector3 dir)
    {
        direction = dir;

        float x = direction.x;
        float y = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (x < 0 && y == 0)
        { // left
            scale.x *= -1;
            rotation.z = 45f;
        }
        else if (x == 0 && y > 0)
        { // up
            scale.x *= -1;
        }
        else if (x == 0 && y < 0)
        { // down
            scale.y *= -1;
        }
        else if (x < 0 && y > 0)
        { // up left
            rotation.z = 180f;
            scale.y *= -1;
        }
        else if (x > 0 && y > 0)
        { // up right
            rotation.z = 0f;
        }
        else if (x > 0 && y < 0)
        { // down right
            rotation.z = -90f;
        }
        else if (x < 0 && y < 0)
        { // down left
            rotation.z = -90f;
            scale.y *= -1;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monster"))
        {
            MonsterStats monster = col.GetComponent<MonsterStats>();
            monster.TakeDamage(currentDamage);

            if (--currentPierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
