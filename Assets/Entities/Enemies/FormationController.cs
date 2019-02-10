using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float formationSpeed = 1f;
    public float spawnDelay = 1f;
    private bool movingRight = true;
    float xmin, xmax;

    // make Enemy formation visible in editor
    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }
    // Use this for initialization
    void Start () {
        SpawnFormation();

    }
	
	// Update is called once per frame
	void Update () {
        if (AllMembersDead()) {
            //spawn new members
            Debug.Log("All Members Dead");
            SpawnUntilFull();
        }
        MoveFormation();
	}

    void MoveFormation() {
        float newX;
        Vector3 newPosition = transform.position;
        // calculate new Enemy Formation position
        // moving only x
        if (movingRight) {
            newX = transform.position.x + formationSpeed * Time.deltaTime;
        } else {
            newX = transform.position.x - formationSpeed * Time.deltaTime;
        }
        // change direction if we reach xmin or xmax
        if (newX > xmax || newX < xmin) {
            movingRight = !movingRight;
            newX = Mathf.Clamp(newX, xmin, xmax);
        }
        // move formation
        newPosition.x = newX;
        transform.position = newPosition;
    }

    private void SpawnEnemy(Transform enemyPosition) {
        GameObject enemy = Instantiate(enemyPrefab, enemyPosition.position, Quaternion.identity);
        enemy.transform.parent = enemyPosition;
    }

    private void SpawnFormation() {
        // find xmin and xmax for Enemy Formation
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + width / 2;
        xmax = rightmost.x - width / 2;

        // spawn an enemyPrefab into every Position
        foreach (Transform child in transform) {
            SpawnEnemy(child);
        }
    }

    private void SpawnUntilFull() {
        Transform nextPosition = NextFreePosition();
        if (nextPosition) {
            SpawnEnemy(nextPosition);
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    private bool AllMembersDead() {
        foreach(Transform childTransform in transform) {
            if (childTransform.childCount > 0) return false;
        }
        return true;
    }

    private Transform NextFreePosition() {
        foreach (Transform childTransform in transform) {
            if (childTransform.childCount == 0) return childTransform;
        }
        return null;
    }
}
