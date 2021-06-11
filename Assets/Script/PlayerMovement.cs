using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlant;
    public GameObject movePoint;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {

        movePoint.transform.parent = null;

        if (isPlant) {
            direction = 1;
        }
        else {
            direction = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() 
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, 0.1f);

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= .005f) {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) {
                movePoint.transform.position += new Vector3(.1f * direction,0,0);
            }
            if (Input.GetAxisRaw("Horizontal") < 0) {
                movePoint.transform.position += new Vector3(-.1f * direction,0,0);
            }
        }
    }
}
