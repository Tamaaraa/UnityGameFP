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
    public List<GameObject> weapons;
    private readonly HashSet<string> unlockedWeapons = new();
    private UpgradeLibrary upgradeLibrary;

    void Awake()
    {
        characterData = CharacterSelection.GetData();
        CharacterSelection.instance.DestroyInstance();
        upgradeLibrary = FindObjectOfType<UpgradeLibrary>();

        currentMaxHealth = characterData.MaxHealth;
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentSpeed = characterData.Speed;
        currentPickUpRange = characterData.PickupRange;

        AddWeapon(characterData.Weapon);
        unlockedWeapons.Add(characterData.Weapon.name);
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
            level += 1;

            LevelUpUI levelUpUI = FindObjectOfType<LevelUpUI>();

            var options = upgradeLibrary.GetRandomUpgrades();

            levelUpUI.ShowLevelUpScreen(options.ToArray());
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
        newWeapon.name = newWeapon.name.Replace("(Clone)", "").Trim();

        weapons.Add(newWeapon);

        upgradeLibrary.RemoveWeaponUnlockUpgrade(newWeapon.name);
        upgradeLibrary.AddWeaponUpgrades(newWeapon.name);

        foreach (var wep in weapons)
            Debug.Log(wep.name);
    }
}
