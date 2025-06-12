using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public TMP_Text levelText;
    public Slider xpSlider;

    private PlayerStats player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        levelText.text = "Level: " + player.level.ToString();
        xpSlider.value = player.experience / player.experienceGoal;
        xpSlider.maxValue = 1f;
    }
}
