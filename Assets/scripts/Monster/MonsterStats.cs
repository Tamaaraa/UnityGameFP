using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public MonsterScriptableObject monsterData;

    [HideInInspector]
    public float currentSpeed;

    [HideInInspector]
    public float currentHealth;

    [HideInInspector]
    public float currentDamage;

    [HideInInspector]
    public float currentAttackSpeed;

    void Awake()
    {
        currentSpeed = monsterData.Speed;
        currentHealth = monsterData.Health;
        currentDamage = monsterData.Damage;
        currentAttackSpeed = monsterData.AttackRate;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
