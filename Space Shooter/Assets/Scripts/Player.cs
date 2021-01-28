using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f; // use _name for private variables

    [SerializeField]
    private float _speedMultiplier = 2f;

    [SerializeField]
    private GameObject _laser;
    
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedBoostActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        //current player position = new position(0,0,0)
        transform.position = new Vector3(0,0,0);
        //Stored a copy of the spawn manager script inside our _spawnManager
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //if the _spawnManager is empty
        if(_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UIManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateMovement();

        //Calls method when space is pressed && Time.time > _canfire
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }


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

    void FireLaser()
    {
        //Debug.Log("Space was pressed");
        //every 0.5 of a second will allow us to fire
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //takes position of player and adds (0,0.8f,0) so the laser comes out from the front
            Instantiate(_laser, transform.position + new Vector3(0,1f,0), Quaternion.identity);
        }
        

    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;
        _uiManager.UpdateLives(_lives);
        Debug.Log(_lives);

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActiveTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        //wait for 5seconds
        yield return new WaitForSeconds(5f);
        //turn off tripleshot
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speed /= _speedMultiplier;
        _isSpeedBoostActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
