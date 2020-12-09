using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 3f;
    public  Rigidbody2D rb;
    Vector2 move;

    // Start is called before the first frame update
    void Update() {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }

    
}
