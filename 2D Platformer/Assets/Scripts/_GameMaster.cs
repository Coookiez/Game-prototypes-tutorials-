using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameMaster : MonoBehaviour {

    public static _GameMaster theGM;
    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives {
        get { return _remainingLives; }
    }

    public AudioClip respawnAudio;
    [SerializeField]
    private int startingMoney;
    public static int money;
    public Transform playerPrefab;
    public Transform statPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public string respawnCountdownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";
    public string gameOverSoundName = "GameOver";
    public CameraShake cameraShake;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject upgradeMenu;
    [SerializeField]
    private WaveSpawner waveSpawner;
    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    //cache
    private AudioManager audioManager;
    void Awake() {
        if (theGM == null) {
            theGM = GameObject.FindGameObjectWithTag("GM").GetComponent<_GameMaster>();
        }
    }
    private void Start() {
        if (cameraShake == null) {
            Debug.LogError("No cameraShake referenced in the _GameMaster");
        }
        _remainingLives = maxLives;
        money = startingMoney;
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No AudioManager found in the scene. (_GameMaster script)");
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            ToggleUpgradeMenu();
        }
    }
    private void ToggleUpgradeMenu() {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }
    public void EndGame() {
        audioManager.PlaySound(gameOverSoundName);
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }
    public IEnumerator _RespawnPlayer() {
        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound(spawnSoundName);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(statPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone, 3f);
        Debug.Log("TODO: Add spawn particles");
    }
    public static void KillPlayer(Player player, StatusIndicator stats) {
        Destroy(player.gameObject);
        Destroy(stats.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0) {
            theGM.EndGame();
        } else {
            theGM.StartCoroutine(theGM._RespawnPlayer());
        }
    }
    public static void KillEnemy(Enemy enemy) {
        theGM._KillEnemy(enemy);
        //    theGM.StartCoroutine(theGM.RespawnPlayer()); //TODO: Change so that it respawns enemies
    }
    public void _KillEnemy(Enemy _enemy) {
        audioManager.PlaySound(_enemy.deathSoundName);
        money += _enemy.moneyDrop;
        //Add particles
        GameObject clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(clone.gameObject, 5);
        //Go camerashake
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
