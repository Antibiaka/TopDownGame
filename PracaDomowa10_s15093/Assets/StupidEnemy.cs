﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidEnemy : MonoBehaviour {
    public Transform player;
    public Collider2D enemyChecker;
    private bool isHuntBegan = false;
    private Rigidbody2D rb;
    private Transform enemyLocation;
    private float moveSpeed = 2f;
    private Vector2 movement , movement2test;
    private float distancex, distancey;

    private Vector3 stopPosition;
    private Vector3 newPosition;
    

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        enemyLocation = this.rb.GetComponent<Transform>();
        stopPosition = rb.transform.position;
        newPosition = stopPosition;
        NewMovementPosition();


        //save current point
    }

    private void Update() {

        Distance();//check distance if< move to plaer  else save point like current point

    }
    // Update is called once per frame
    void FixedUpdate() {

        if (isHuntBegan) {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
            MoveToPlayer(movement);
        }
        else {
            rb.velocity = Vector2.zero;
            stopPosition = rb.transform.position;//save current point calculate rnd point and move there
            LookingAround();  
        }

    }
    private void LookingAround() {
        while (!isHuntBegan) {
            Vector3 direction = newPosition;
            direction.Normalize();
            movement2test = direction;
            MoveToNewPosition(movement2test);// break when got there 
            //    if (stopPosition == newPosition) {  
            //        NewMovementPosition();
            //    }
            //    else {
            //        
            //    }
        }
    }
    private void NewMovementPosition() {
        
        newPosition.x = stopPosition.x + RandArea(5f);
        newPosition.y = stopPosition.y + RandArea(5f);
        
        
    }

    private float RandArea(float number) {
        number = Random.Range(-number, number);
        
        return number;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            isHuntBegan = true;
        }
    }
    private void MoveToPlayer(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime)); //move rb of enemy to player
    }

    private void MoveToNewPosition(Vector2 direction) {
     
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        NewMovementPosition();
    }

    private void Distance() {
        distancex = Mathf.Abs(player.transform.position.x - enemyLocation.transform.position.x); // abs distance
        distancey = Mathf.Abs(player.transform.position.y - enemyLocation.transform.position.y); // abs distance
        if (distancex >= 5 || distancey >= 5) {
            isHuntBegan = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Arrow")) {
            gameObject.GetComponent<Transform>().SetParent(this.rb.transform);
        }
    }
}
