using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //set variable for speed
    // Start is called before the first frame update
    [SerializeField]
    private float _laserSpeed = 8f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoots laser upwards at set speed
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //if laser position > (0,8,0) --> destroy laser
        if(transform.position.y > 8)
        {
            //check if there is a parent
            if(transform.parent != null)
            {
                //destroy the parent
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
