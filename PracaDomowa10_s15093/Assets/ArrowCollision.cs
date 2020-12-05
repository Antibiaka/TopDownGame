using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.GetComponent<BoxCollider2D>().enabled = false;
            rb.freezeRotation = true;
        }
    }
}
