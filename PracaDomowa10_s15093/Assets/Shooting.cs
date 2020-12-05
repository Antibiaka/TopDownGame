using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject arrowPref;
    public Transform bowPoint;
    public float arrowForce = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 aimDir =Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = aimDir - bowPosition;
        transform.right = direction;


        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }
    void Shoot() {
        GameObject arrow = Instantiate(arrowPref, bowPoint.position, bowPoint.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = transform.right * arrowForce;
        Destroy(arrow, 5f);
       // arrow.transform.Rotate(0, 0, Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg);
       // rb.AddForce(bowPoint.up * arrowForce, ForceMode2D.Impulse); 
    }
}
