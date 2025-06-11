using UnityEngine;

[CreateAssetMenu(fileName = "MonsterScriptableObject", menuName = "ScriptableObjects/Monster")]
public class MonsterScriptableObject : ScriptableObject
{
    [SerializeField]
    float speed;
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }

    [SerializeField]
    float health;
    public float Health
    {
        get => health;
        private set => health = value;
    }

    [SerializeField]
    float damage;
    public float Damage
    {
        get => damage;
        private set => damage = value;
    }

    [SerializeField]
    float attackRate;
    public float AttackRate
    {
        get => attackRate;
        private set => attackRate = value;
    }
}
