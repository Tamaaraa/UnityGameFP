using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpPanel;
    public Button[] levelUpButtons;

    public void ShowLevelUpScreen(UpgradeOption[] upgradeOptions)
    {
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);

        for (int i = 0; i < levelUpButtons.Length; i++)
        {
            if (i < upgradeOptions.Length)
            {
                int index = i;
                levelUpButtons[i].gameObject.SetActive(true);
                levelUpButtons[i].GetComponentInChildren<TMP_Text>().text = upgradeOptions[i].title;
                levelUpButtons[i].onClick.RemoveAllListeners();
                levelUpButtons[i].onClick.AddListener(() => SelectUpgrade(upgradeOptions[index]));
            }
            else
            {
                levelUpButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void SelectUpgrade(UpgradeOption upgradeOption)
    {
        Debug.Log("Upgrade selected: " + upgradeOption.title);
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        upgradeOption.Apply?.Invoke();
    }
}
