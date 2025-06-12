using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider hpSlider;

    private PlayerStats player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        hpSlider.maxValue = player.currentMaxHealth;
        hpSlider.value = player.currentHealth;
    }
}
