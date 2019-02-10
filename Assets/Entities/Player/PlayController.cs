using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour {

    public static float shipSpeed = 5.0f; // the speed at which the ship moves across the screen
    float xmin, xmax;
    float spriteWidth;
    public GameObject laserPrefab;
    public float laserSpeed, laserPeriod = 10.0f;
    public Vector3 playerLaserOffset; // offset from the player's ship from where the laser should fire
    public float health = 20;
    public AudioClip audioPlayerLaser;
    public AudioClip audioPlayerHit;
	// Use this for initialization
	void Start () {
        // find half of the sprite width to clamp position to edge of screen
        spriteWidth = GetComponent<SpriteRenderer>().size.x / 2.0f;
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + spriteWidth;
        xmax = rightmost.x - spriteWidth;
        
	}
	
	// Update is called once per frame
	void Update () {
        float deltaT = Time.deltaTime;
        float scaledSpeed = shipSpeed * deltaT;
        float currentX = transform.position.x;
        float newX = currentX;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            // subtract to go left
            newX = Mathf.Clamp(currentX - scaledSpeed, xmin, xmax);
            transform.localScale = new Vector3(0.7f, 1.0f, 1.0f);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            // add to go right
            newX = Mathf.Clamp(currentX + scaledSpeed, xmin, xmax);
            transform.localScale = new Vector3(0.7f, 1.0f, 1.0f);
        } else {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space)) {
            // Invoke repeating has bug with 0.0s as wait time so
            // call the function once straight away and then call
            // InvokeRepeating with period as wait time
            FireLaser();
            InvokeRepeating("FireLaser", laserPeriod, laserPeriod);
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("FireLaser");
        }
    }

    void FireLaser() {
        GameObject laser = Instantiate(laserPrefab, transform.position + playerLaserOffset, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserSpeed;
        AudioSource.PlayClipAtPoint(audioPlayerLaser, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Projectile myProjectile = collider.gameObject.GetComponent<Projectile>();
        if (myProjectile) {
            AudioSource.PlayClipAtPoint(audioPlayerHit, transform.position);
            health -= myProjectile.GetDamage();
            myProjectile.Hit();
            if (health <= 0) {
                Die();
            }
        }
    }

    private void Die() {
        Destroy(gameObject);
        LevelManager lvlMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        lvlMan.LoadLevel("EndScene");
    }


}
