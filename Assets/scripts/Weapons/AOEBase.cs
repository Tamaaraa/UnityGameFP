using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AOEBase : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected float currentDamage;
    protected float currentAttackRate;

    private Dictionary<MonsterStats, float> damageTimers = new();

    protected virtual void Start()
    {
        currentDamage = weaponData.Damage;
        currentAttackRate = weaponData.AttackRate;
    }

    private void Update()
    {
        var enemies = new List<MonsterStats>(damageTimers.Keys);
        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                damageTimers.Remove(enemy);
                continue;
            }

            damageTimers[enemy] -= Time.deltaTime;
            if (damageTimers[enemy] <= 0f)
            {
                Debug.Log($"Hit {enemy.name}");
                enemy.TakeDamage(currentDamage);
                damageTimers[enemy] = currentAttackRate;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monster"))
        {
            MonsterStats monster = col.GetComponent<MonsterStats>();
            if (!damageTimers.ContainsKey(monster))
            {
                monster.TakeDamage(currentDamage);
                damageTimers[monster] = currentAttackRate;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Monster"))
        {
            MonsterStats monster = col.GetComponent<MonsterStats>();
            damageTimers.Remove(monster);
        }
    }
}
