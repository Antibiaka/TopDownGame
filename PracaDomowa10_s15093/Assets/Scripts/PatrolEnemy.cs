using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour {
    [SerializeField]
    Transform[] patrolPoints;
    private float moveSpeed = 1.5f;
    int patrolIndex = 0;
    private bool isHuntBegan = false;
    public Transform player, enemyLocation;
    private Vector2 movement;
    private Rigidbody2D rb;   
    private float distancex, distancey,followDist;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        transform.position = patrolPoints[patrolIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Distance();

    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            isHuntBegan = true;
        }
    }
    void FixedUpdate() {

        if (isHuntBegan) {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
            MoveToPlayer(movement);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolIndex].transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == patrolPoints[patrolIndex].transform.position) {
                patrolIndex++;
            }
            if (patrolIndex == patrolPoints.Length) {
                patrolIndex = 0;
            }
        }
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

    }
}
