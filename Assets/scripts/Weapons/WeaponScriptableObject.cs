using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject weaponPrefab;
    public GameObject WeaponPrefab
    {
        get => weaponPrefab;
        private set => weaponPrefab = value;
    }

    [SerializeField]
    float damage;
    public float Damage
    {
        get => damage;
        private set => damage = value;
    }

    [SerializeField]
    float speed;
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }

    [SerializeField]
    float attackRate;
    public float AttackRate
    {
        get => attackRate;
        private set => attackRate = value;
    }

    [SerializeField]
    float pierce;
    public float Pierce
    {
        get => pierce;
        private set => pierce = value;
    }
}
