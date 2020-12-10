using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4f;
    void Start()
    {
        transform.position = new Vector3(0,7.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        //translate the enemy down
        //it needs to move at a speed of 4m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6f){
            //creates a random value between the range
            float randomX = Random.Range(-9.2f , 9.2f);
            //teleports back to the top
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Hit: " + other.transform.name);
        if(other.tag == "Laser")
        {
            //Destroy laser
            Destroy(other.gameObject);
            //Destroy enemy
            Destroy(this.gameObject);
        }
        if(other.tag == "Player")
        {
            //fetches the player script and accesses Damage() function
            Player player = other.GetComponent<Player>();
            //if player script is not empty (this is used to prevent crashes)
            if(player != null)
            {
                player.Damage();
            }
            //Damage the player
            Destroy(this.gameObject);
        }
    }
}
