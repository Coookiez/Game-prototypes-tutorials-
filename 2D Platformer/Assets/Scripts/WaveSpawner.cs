using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }
    public Wave[] waves;
    public Transform[] spawnPoints;
    private int nextWave = 0;
    public int NextWave {
        get { return nextWave + 1; }
    }
    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown {
        get { return waveCountdown; }
    }
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State {
        get { return state; }
    }
    // Use this for initialization
    void Start() {
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update() {
        if (state == SpawnState.WAITING) {
            if (!EnemyIsAlive()) {
                // Begin a new round
                WaveCompleted();
                return;
            } else {
                return;
            }
            // Check if enemies are still alive
        }
        if (waveCountdown <= 0f) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0) {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; ++i) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void WaveCompleted() {
        Debug.Log("Wave completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("All waves completed! Looping...");
        } else {
            nextWave++;
        }
    }
    void SpawnEnemy(Transform _enemy) {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        if (spawnPoints.Length == 0) {
            Debug.LogError("No spawnPoints referenced");
        }
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
