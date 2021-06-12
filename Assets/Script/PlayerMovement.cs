using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlant;
    public GameObject movePoint;
    public float moveSpeed;
    public SpriteRenderer playerSprite;

    private int direction;
    
    private const float TILE_SIZE = 1.0f;
    private const float MAX_WIDTH = 10.0f;
    private const float MAX_HEIGHT = 5.0f;
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
        GetComponent<SpriteRenderer>().sortingOrder = (int)(((-transform.position.y)+25)*100)+1; //Ordering madness
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime); // what does this do? Changing the last value doesn't change what happens... :/

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= 0.005f)
        {

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                    //Clamps position between MAX_WIDTH
                    movePoint.transform.position = new Vector3(Mathf.Clamp(movePoint.transform.position.x + (TILE_SIZE * direction), -MAX_WIDTH, MAX_WIDTH), 
                    movePoint.transform.position.y,
                    movePoint.transform.position.z);
                    playerSprite.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                //movePoint.transform.position += new Vector3(-TILE_SIZE * direction, 0, 0);
                //Clamps position between MAX_WIDTH
                movePoint.transform.position = new Vector3(Mathf.Clamp(movePoint.transform.position.x + (-TILE_SIZE * direction), -MAX_WIDTH, MAX_WIDTH), 
                movePoint.transform.position.y,
                movePoint.transform.position.z);
                playerSprite.flipX = false;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                //movePoint.transform.position += new Vector3(0, TILE_SIZE * direction, 0);
                //Clamps position between MAX_HEIGHT
                movePoint.transform.position = new Vector3(movePoint.transform.position.x, 
                Mathf.Clamp(movePoint.transform.position.y + (TILE_SIZE * direction), -MAX_HEIGHT, MAX_HEIGHT-1),
                movePoint.transform.position.z);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                //Clamps position between MAX_HEIGHT
                movePoint.transform.position = new Vector3(movePoint.transform.position.x, 
                Mathf.Clamp(movePoint.transform.position.y + (-TILE_SIZE * direction), -MAX_HEIGHT, MAX_HEIGHT-1),
                movePoint.transform.position.z);
            }
        }

        //Collider2D collision = Physics2D.OverlapCircle(position, attackSize, enemy);
    }
}
