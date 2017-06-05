using UnityEngine;
using System.Collections;

public class HurtEnemy : MonoBehaviour {
    public int damageToGive;
    public GameObject BloodBurst;
    public Transform hitPoint;
    public GameObject damageNumber;

    private PlayerStats thePS;
    private int currentDamage;
    // Use this for initialization
    void Start() {
        thePS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            currentDamage = damageToGive + thePS.currentAttack;
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
            Instantiate(BloodBurst, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }
}
