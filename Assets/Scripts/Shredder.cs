using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    
    // destroy anything that meets the shredder
    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(collider.gameObject);
    }

    public void OnDrawGizmos() {
        Vector3 size = new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y, 0);
        Gizmos.DrawWireCube(transform.position, size);
    }
}
