using System.Collections.Generic;
using UnityEngine;

public class UpgradeLibrary : MonoBehaviour
{
    public static UpgradeLibrary Instance;
    public List<UpgradeOption> upgradeOptions = new();
    private readonly HashSet<string> addedWeaponUpgrades = new();

    private PlayerStats player;
    private WeaponLibrary weaponLibrary;

    public const string UnlockKnifeTitle = "Unlock Knife weapon";
    public const string UnlockForceFieldTitle = "Unlock Field of Death weapon";
    public const string UnlockSpikeTitle = "Unlock Spike weapon";

    private void Awake()
    {
        weaponLibrary = FindObjectOfType<WeaponLibrary>();
        player = FindObjectOfType<PlayerStats>();

        InitializeUpgrades();
    }

    void InitializeUpgrades()
    {
        var playerStats = FindObjectOfType<PlayerStats>();

        // base upgrades
        upgradeOptions = new List<UpgradeOption>
        {
            new("Increase Max Health by 30", () => playerStats.currentMaxHealth += 30),
            new("Increase Speed by 10%", () => playerStats.currentSpeed *= 1.1f),
            new("Recover HP 30% Faster per level", () => playerStats.currentRecovery *= 0.06f),
            new(
                "Increase pickup range by 25% per level",
                () => playerStats.currentPickUpRange += .25f
            ),
        };

        // Add weapons
        upgradeOptions.AddRange(
            new List<UpgradeOption>
            {
                new(
                    UnlockForceFieldTitle,
                    () => playerStats.AddWeapon(weaponLibrary.forceFieldPrefab)
                ),
                new(UnlockSpikeTitle, () => playerStats.AddWeapon(weaponLibrary.spikePrefab)),
                new(UnlockKnifeTitle, () => playerStats.AddWeapon(weaponLibrary.knifePrefab))
            }
        );
    }

    public List<UpgradeOption> GetRandomUpgrades()
    {
        List<UpgradeOption> upgradePool = new(upgradeOptions);
        List<UpgradeOption> selectedUpgrades = new();

        for (int i = 0; i < 3 && upgradePool.Count > 0; i++)
        {
            int index = Random.Range(0, upgradePool.Count);
            selectedUpgrades.Add(upgradePool[index]);
            upgradePool.RemoveAt(index);
        }

        return selectedUpgrades;
    }

    public void AddWeaponUpgrades(string weaponName)
    {
        if (addedWeaponUpgrades.Contains(weaponName))
            return;

        switch (weaponName)
        {
            case "Dagger":
                upgradeOptions.AddRange(
                    new List<UpgradeOption>
                    {
                        new(
                            "Upgrade Knife Damage by 30% per level",
                            () =>
                            {
                                var knife = player.weapons.Find(wep => wep.name.Contains("Dagger"));
                                knife.GetComponent<WeaponBase>().weaponData.Damage += 3f;
                            }
                        ),
                        new(
                            "Upgrade Knife Pierce by 1",
                            () =>
                            {
                                var knife = player.weapons.Find(wep => wep.name.Contains("Dagger"));
                                knife.GetComponent<WeaponBase>().weaponData.Pierce += 1;
                            }
                        ),
                        new(
                            "Upgrade Knife Attack Speed by 10%",
                            () =>
                            {
                                var knife = player.weapons.Find(wep => wep.name.Contains("Dagger"));
                                knife.GetComponent<WeaponBase>().weaponData.AttackRate *= .9f;
                            }
                        )
                    }
                );
                break;

            case "ForceField":
                upgradeOptions.AddRange(
                    new List<UpgradeOption>
                    {
                        new(
                            "Expand Field of Death Radius by 20% per level",
                            () =>
                            {
                                var field = player.weapons.Find(wep =>
                                    wep.name.Contains("ForceField")
                                );
                                field.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                            }
                        ),
                        new(
                            "Increase Field of Death Damage by 30% per level",
                            () =>
                            {
                                var field = player.weapons.Find(wep =>
                                    wep.name.Contains("ForceField")
                                );
                                field.GetComponent<AOEBase>().weaponData.Damage += .6f;
                            }
                        ),
                    }
                );
                break;

            case "Spike":
                upgradeOptions.AddRange(
                    new List<UpgradeOption>
                    {
                        new(
                            "Increase Spike Attack Rate By 10%",
                            () =>
                            {
                                var spike = player.weapons.Find(wep => wep.name.Contains("Spike"));
                                spike.GetComponent<WeaponBase>().weaponData.AttackRate *= 0.9f;
                            }
                        ),
                        new(
                            "Increase Spike Pierce By 1",
                            () =>
                            {
                                var spike = player.weapons.Find(wep => wep.name.Contains("Spike"));
                                spike.GetComponent<WeaponBase>().weaponData.Pierce += 1;
                            }
                        ),
                        new(
                            "Increase Spike Damage by 30% per level",
                            () =>
                            {
                                var spike = player.weapons.Find(wep => wep.name.Contains("Spike"));
                                spike.GetComponent<WeaponBase>().weaponData.Damage += 1.8f;
                            }
                        ),
                    }
                );
                break;
        }

        addedWeaponUpgrades.Add(weaponName);
    }

    public void RemoveWeaponUnlockUpgrade(string weaponName)
    {
        switch (weaponName)
        {
            case "Dagger":
                upgradeOptions.RemoveAll(upgrade => upgrade.title == UnlockKnifeTitle);
                break;
            case "ForceField":
                upgradeOptions.RemoveAll(upgrade => upgrade.title == UnlockForceFieldTitle);
                break;
            case "Spike":
                upgradeOptions.RemoveAll(upgrade => upgrade.title == UnlockSpikeTitle);
                break;
        }
    }
}
