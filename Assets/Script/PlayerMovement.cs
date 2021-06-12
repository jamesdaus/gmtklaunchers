using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlant;
    public GameObject movePoint;
    public float moveSpeed;
    private int direction;
    
    private const float TILE_SIZE = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.transform.parent = null;

        if (isPlant)
        {
            direction = 1;
        }
        else
        {
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
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime); // what does this do? Changing the last value doesn't change what happens... :/

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= 0.005f)
        {

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                movePoint.transform.position += new Vector3(TILE_SIZE * direction, 0, 0);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                movePoint.transform.position += new Vector3(-TILE_SIZE * direction, 0, 0);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                movePoint.transform.position += new Vector3(0, TILE_SIZE * direction, 0);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                movePoint.transform.position += new Vector3(0, -TILE_SIZE * direction, 0);
            }
        }

        //Collider2D collision = Physics2D.OverlapCircle(position, attackSize, enemy);
    }
}
