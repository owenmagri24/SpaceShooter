using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4f;

    private Player _player;
    Animator _enemyAnimator;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }

        _enemyAnimator = gameObject.GetComponent<Animator>();
        if(_enemyAnimator == null)
        {
            Debug.LogError("Animator is null");
        }
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

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Hit: " + other.transform.name);
        if(other.tag == "Laser")
        {
            //Destroy laser
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 2f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Destroy enemy
            Destroy(this.gameObject, 2.633f);
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
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 2f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Damage the player
            Destroy(this.gameObject,2.633f);
        }
    }
}
