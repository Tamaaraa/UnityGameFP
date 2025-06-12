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
    public float currentPickUpRange;

    private WeaponLibrary weaponLibrary;
    public List<GameObject> weapons;

    void Awake()
    {
        characterData = CharacterSelection.GetData();
        CharacterSelection.instance.DestroyInstance();

        currentMaxHealth = characterData.MaxHealth;
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentSpeed = characterData.Speed;
        currentPickUpRange = characterData.PickupRange;
        weaponLibrary = FindObjectOfType<WeaponLibrary>();

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
            int excessExp = experience - (int)experienceGoal;
            experienceGoal = 100 * (float)Math.Pow(1.5f, level / 7);
            experience = 0 + excessExp;
            float hpDifference = currentMaxHealth * 1.2f - currentMaxHealth;
            currentHealth += hpDifference;
            currentMaxHealth *= 1.2f;
            currentRecovery *= 1.1f;
            level += 1;

            LevelUpUI levelUpUI = FindObjectOfType<LevelUpUI>();

            UpgradeOption[] options = new UpgradeOption[]
            {
                new("gain 500 exp", () => IncreaseExperience(500)),
                new("Ten-fold recovery", () => currentRecovery *= 10),
                new("Add forcefield weapon", () => AddWeapon(weaponLibrary.forceFieldPrefab))
            };

            levelUpUI.ShowLevelUpScreen(options);
        }
    }

    internal void TakeDamage(float damage)
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
