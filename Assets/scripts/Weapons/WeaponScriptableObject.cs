using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject weaponPrefab;
    public GameObject WeaponPrefab
    {
        get => weaponPrefab;
        set => weaponPrefab = value;
    }

    [SerializeField]
    float damage;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    [SerializeField]
    float speed;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    [SerializeField]
    float attackRate;
    public float AttackRate
    {
        get => attackRate;
        set => attackRate = value;
    }

    [SerializeField]
    float pierce;
    public float Pierce
    {
        get => pierce;
        set => pierce = value;
    }
}
