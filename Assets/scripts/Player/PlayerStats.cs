using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerScriptableObject characterData;

    [HideInInspector]
    public float currentMaxHealth;

    [HideInInspector]
    public float currentHealth;

    [HideInInspector]
    public float currentRecovery;

    [HideInInspector]
    public float currentSpeed;

    [HideInInspector]
    public float currentDamageMulti;

    [HideInInspector]
    public float currentPickUpRange;

    public List<GameObject> weapons;

    void Awake()
    {
        characterData = CharacterSelection.GetData();
        CharacterSelection.instance.DestroyInstance();

        currentMaxHealth = characterData.MaxHealth;
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentSpeed = characterData.Speed;
        currentDamageMulti = characterData.DamageMulti;
        currentPickUpRange = characterData.PickupRange;

        AddWeapon(characterData.Weapon);
    }

    void Update()
    {
        Recover();
    }

    public int experience = 0;
    public int level = 1;
    public float experienceGoal = 100;

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUp();
    }

    void LevelUp()
    {
        while (experience >= experienceGoal)
        {
            experienceGoal += 100 * (float)Math.Pow(1.5f, level / 7);
            currentMaxHealth *= 1.2f;
            currentRecovery *= 1.1f;
            currentSpeed += 0.1f;
            currentDamageMulti *= 1.1f;
            level += 1;
        }
    }

    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void Recover()
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            if (currentHealth > currentMaxHealth)
            {
                currentHealth = currentMaxHealth;
            }
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        newWeapon.transform.SetParent(transform);
        weapons.Add(newWeapon);
    }
}
