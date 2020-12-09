using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject arrowPref;
    public Transform bowPoint;
    public float arrowForce = 3;
    public float distancex, distancey,arrowOn;
    private GameObject tempGo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = aimDir - bowPosition;
        transform.right = direction;
        

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
            
        }
    }
  
    void Shoot() {
        
        GameObject arrow = Instantiate(arrowPref, bowPoint.position, bowPoint.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = transform.right * arrowForce;
        arrow.GetComponent<BoxCollider2D>().isTrigger = true;

        tempGo = arrow;
        Invoke("MakeTrigger", .1f);

        
        //distancex = Mathf.Abs(arrow.GetComponent<Rigidbody2D>().transform.position.x - bowPoint.transform.position.x); // abs distance
        //distancey = Mathf.Abs(arrow.GetComponent<Rigidbody2D>().transform.position.y - bowPoint.transform.position.y); // abs distance
        //float temp = distancex * distancex + distancey * distancey;
        //arrowOn = Mathf.Sqrt(temp);
        //if(arrowOn > 1f) {
        //    arrow.GetComponent<BoxCollider2D>().isTrigger = false;
        //}
        Destroy(arrow, 5f);
        
        // arrow.transform.Rotate(0, 0, Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg);
        // rb.AddForce(bowPoint.up * arrowForce, ForceMode2D.Impulse); 
    }
    public void MakeTrigger() {
        tempGo.GetComponent<BoxCollider2D>().isTrigger = false;
    }


}
