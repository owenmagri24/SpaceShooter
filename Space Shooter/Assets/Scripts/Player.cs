using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f; // use _name for private variables
    // Start is called before the first frame update
    void Start()
    {
        //current player position = new position(0,0,0)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateMovement();

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Vector3.right == Vector3(1,0,0)
        //Vector3.left == Vector3(-1,0,0)
        //Vector3(1,0,0) * 1 * 3.5 * realtime - When key pressed
        //Vector3(1,0,0) * 0 * 3.5 * realtime - When no key pressed
        //option 1
        // transform.Translate(Vector3.right * horizontalInput *speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);
        //option 2
        //transform.Translate(new Vector3(horizontalInput,verticalInput,0) * speed * Time.deltaTime);
        //option 3
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //if the player position on the y axis > 0
        // if(transform.position.y >= 0)
        // {
        //     transform.position = new Vector3(transform.position.x,0,0);
        // }
        //if the player position on y axis <= -3.8f
        // else if(transform.position.y <= -3.8f)
        // {
        //     transform.position = new Vector3(transform.position.x,-3.8f,0);
        // }
        
        //Mathf.clamp(the value we wish to restrict, min value, max value)
        transform.position  =   new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);


        //if the player position on the x axis > 11 or < -11, send to other side
        if(transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y,0);
        }
        else if(transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y,0);
        }
    }
}
