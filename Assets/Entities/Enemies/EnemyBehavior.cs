using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float health = 10f;
    public float laserSpeed = 20f;
    private Vector3 projectileOffset = new Vector3(0f, -0.618f, 0f);
    public float shotsPerSecond = 0.2f;
    public int scoreValue = 1;
    private ScoreTracker scoreTracker;
    public GameObject projectile;
    public AudioClip audioEnemyLaser,audioEnemyDeath;
    public GameObject explosionParticle;

    private void Start() {
        scoreTracker = GameObject.Find("Score").GetComponent<ScoreTracker>();
    }
    private void Update() {
        float pFire = Time.deltaTime * shotsPerSecond;
        if(Random.value < pFire) {
            FireLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Projectile myProjectile = collider.gameObject.GetComponent<Projectile>();
        if(myProjectile) {
            health -= myProjectile.GetDamage();
            myProjectile.Hit();
            if(health <= 0) {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(audioEnemyDeath, transform.position);
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                scoreTracker.Score(scoreValue);
            }
        }
    }

    private void FireLaser() {
        GameObject enemyProjectile = Instantiate(projectile, transform.position + projectileOffset, Quaternion.identity);
        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -laserSpeed, 0f);
        AudioSource.PlayClipAtPoint(audioEnemyLaser, transform.position);
    }
}
