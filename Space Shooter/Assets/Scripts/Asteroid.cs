using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _rotationSpeed = 3f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the asteroid at 3m/s on the z axis
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if(_spawnManager != null)
            {
                _spawnManager.StartSpawning();
            }
            Destroy(this.gameObject, 0.25f);
        }
    }
}
