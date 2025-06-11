using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject weapon;
    public GameObject Weapon
    {
        get => weapon;
        private set => weapon = value;
    }

    [SerializeField]
    float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
        private set => maxHealth = value;
    }

    [SerializeField]
    float recovery;
    public float Recovery
    {
        get => recovery;
        private set => recovery = value;
    }

    [SerializeField]
    float speed;
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }

    [SerializeField]
    float damageMulti;
    public float DamageMulti
    {
        get => damageMulti;
        private set => damageMulti = value;
    }

    [SerializeField]
    float pickupRange;
    public float PickupRange
    {
        get => pickupRange;
        private set => pickupRange = value;
    }
}
