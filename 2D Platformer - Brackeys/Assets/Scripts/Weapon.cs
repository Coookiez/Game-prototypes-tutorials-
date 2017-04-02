using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10f;
    public LayerMask whatToHit;

    float timeToFire = 0;
    private Transform firePoint;

    //Caching
    AudioManager audioManager;

    public Transform BulletTrailPrefab;
    public Transform hitPrefab;
    public Transform MuzzleFlashPrefab;
    public float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    //Handle camera shaking
    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    CameraShake camShake;

    public string weaponShootSound = "DefaultShot";
    private void Awake() {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("firePoint is null");
        }
    }

    private void Start() {
        camShake = _GameMaster.theGM.GetComponent<CameraShake>();
        if (camShake == null) {
            Debug.LogError("No CameraShake script found on GM objects.");
        }
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audioManager script found on GM objects.");
        }
    }

    // Update is called once per frame
    void Update() {
        if (fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                Shoot();
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot() {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.DamageEnemy((int)damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage");
            }
        }
        if (Time.time > timeToSpawnEffect) {
            Vector3 hitPos, hitNormal;
            if (hit.collider == null) {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            } else {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            //  Debug.Log("timetospawneffect przed: " + timeToSpawnEffect + "Time.time: " + Time.time);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
            //  Debug.Log("timetospawneffect po: " + timeToSpawnEffect + "Time.time: " + Time.time);
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal) {
        Transform trail = (Transform)Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        if (lr != null) {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
            //set positions
        }
        Destroy(trail.gameObject, 0.02f);
        if (hitNormal != new Vector3(9999, 9999, 9999)) {
            Transform hitParticle = (Transform)Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitParticle.gameObject, 1f);
        }
        Transform clone = (Transform)Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);

        //Shake the camera
        camShake.Shake(camShakeAmt, camShakeLength);

        //Play shoot sound
        audioManager.PlaySound(weaponShootSound);
    }
}
