using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidEnemy : MonoBehaviour {
    public Transform player;
    public Collider2D enemyChecker;
    private bool isHuntBegan = false;
    private Rigidbody2D rb;
    private Transform enemyLocation;
    private float moveSpeed = 2f;
    private Vector2 movement ;
    private float distancex, distancey, followDist;
    private Vector3 mainPosition;
    private Vector3 newPosition;
    

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        enemyLocation = this.rb.GetComponent<Transform>();
        mainPosition = rb.transform.position; 
        newPosition = mainPosition;
        NewMovementPosition();
  
    }

    private void Update() {
        
        
        
        
    }
    // Update is called once per frame
    void FixedUpdate() {
        Distance();//check distance if< move to plaer  else save point like current point
        if (isHuntBegan) {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
            MoveToPlayer(movement);
        }
        else {
            rb.velocity = Vector2.zero;
            if (transform.position == newPosition) {
                NewMovementPosition();
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * moveSpeed);
            }
            //MoveToNewPosition(newPosition);
        }

    }
    
    private void NewMovementPosition() {
        
        newPosition.x = mainPosition.x + RandArea(3f);
        newPosition.y = mainPosition.y + RandArea(3f);

        newPosition = new Vector3(newPosition.x, newPosition.y, 0);
        
    }

    private float RandArea(float number) {
        number = Random.Range(-number, number);
        
        return number;
    }

    private void MoveToPlayer(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime)); //move rb of enemy to player
    }


    private void Distance() {
        distancex = Mathf.Abs(player.transform.position.x - enemyLocation.transform.position.x); // abs distance
        distancey = Mathf.Abs(player.transform.position.y - enemyLocation.transform.position.y); // abs distance
        float temp = distancex * distancex + distancey * distancey;
        followDist = Mathf.Sqrt(temp);

        if (followDist >= 5) {
            isHuntBegan = false;
        }
        else if (isHuntBegan) {
            mainPosition = rb.transform.position;
            NewMovementPosition();
            Debug.Log(mainPosition + " main point");
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            isHuntBegan = true;
        }
    }
}
