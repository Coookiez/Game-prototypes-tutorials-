using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySampleAssets._2D;
[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour {
    [System.Serializable]
    public class EnemyStats {
        public int maxHealth = 100;
        private int _curHealth;
        public int curHealth {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        public int damage = 30;
        public void Init() {
            curHealth = maxHealth;
        }
    }
    public int fallBoundary = -9;
    public EnemyStats enemyStats = new EnemyStats();
    public Transform deathParticles;
    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;
    public string deathSoundName = "Explosion";
    public int moneyDrop = 10;
    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;


    private void Start() {
        enemyStats.Init();

        if (statusIndicator != null) {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
        _GameMaster.theGM.onToggleUpgradeMenu += OnUpgradeMenuToggle;
        if (deathParticles == null) {
            Debug.LogError("No deathParticles on enemy");
        }
    }
    private void Update() {
        if (transform.position.y <= fallBoundary) {
            DamageEnemy(99999999);
        }
    }
    public void DamageEnemy(int damage) {
        enemyStats.curHealth -= damage;
        if (enemyStats.curHealth <= 0) {
            _GameMaster.KillEnemy(this);
        }
        if (statusIndicator != null) {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }
    void OnUpgradeMenuToggle(bool active) {
        GetComponent<EnemyAI>().enabled = !active;
    }
    private void OnCollisionEnter2D(Collision2D _colInfo) {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null) {
            _player.DamagePlayer(enemyStats.damage);
            DamageEnemy(99999);
        }
    }
    private void OnDestroy() {
        _GameMaster.theGM.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}

