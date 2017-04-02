using UnityEngine.UI;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour {

    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text speedText;
    [SerializeField]
    private float healthMultiplier = 1.1f;
    [SerializeField]
    private float movementSpeedMultiplier = 1.05f;
    [SerializeField]
    private int upgradeCost = 50;
    private PlayerStats stats;

    private void OnEnable() {
        stats = PlayerStats.instance;
        UpdateValues();
    }

    void UpdateValues() {
        healthText.text = "HEALTH: " + stats.maxHealth.ToString();
        speedText.text = "SPEED: " + stats.movementSpeed.ToString();
    }

    public void UpgradeHealth() {
        if (_GameMaster.money < upgradeCost) {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);
        _GameMaster.money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }

    public void UpgradeSpeed() {
        if (_GameMaster.money < upgradeCost) {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.movementSpeed = Mathf.Round((stats.movementSpeed * movementSpeedMultiplier));
        _GameMaster.money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }
}
