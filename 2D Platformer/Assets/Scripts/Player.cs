using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets._2D;

public class Player : MonoBehaviour {
    public int fallBoundary = -9;
    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";
    private AudioManager audioManager;
    [SerializeField]
    private StatusIndicator statusIndicator;
    private PlayerStats stats;

    private void Start() {
        stats = PlayerStats.instance;
        stats.curHealth = stats.maxHealth;
        if (statusIndicator == null) {
            Debug.LogError("statusIndicator is null");
        } else {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
        _GameMaster.theGM.onToggleUpgradeMenu += OnUpgradeMenuToggle;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("no audioManager in Player script");
        }

        InvokeRepeating("RegenHealth", 1f/stats.healthRegenRate,1f/stats.healthRegenRate);
    }

    void RegenHealth() {
        stats.curHealth += 1 ;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    private void Update() {
        if (transform.position.y <= fallBoundary) {
            DamagePlayer(99999999);
        }
    }

    void OnUpgradeMenuToggle(bool active) {
        //handle what happens when the upgrade menu is toggled
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null) {
            _weapon.enabled = !active;
        }
    }

    public void DamagePlayer(int damage) {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0) {
            audioManager.PlaySound(deathSoundName);
            _GameMaster.KillPlayer(this, FindObjectOfType<StatusIndicator>());
        } else {
            audioManager.PlaySound(damageSoundName);
        }
        if (statusIndicator != null) {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    private void OnDestroy() {
        _GameMaster.theGM.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}
