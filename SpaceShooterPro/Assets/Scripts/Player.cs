using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (int, float, bool, string)
    // every variable has a name
    // optional value assigned
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private GameObject _TripleLaser;
    [SerializeField] private bool IsTripleShotActive = false;
    // variable for IsTripleShotActive

    // Start is called before the first frame update
    void Start()
    {
        // take turrent position = new position (0,0,0);
        
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * _speed * Time.deltaTime; // Input.GetAxis returns float 
        float verticalInput = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0));

        // if player position on the y is greater than 0
        // y position = 0
        // else if position on the y is less than -3.8f
        // y pos = -3.8f
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        // if player on the x > 11
        // x pos = -11
        // else if player on the x is less than -11
        // x pos = 11

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }

        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); // Vector3.right = new Vector3 (1,0,0) // time.deltatime 1m per second (incorporate real time)
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        //Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        //transform.Translate(direction);

    }
    
    void FireLaser()
    {
        // if i hit the space key
        // spawn gameobject
        _canFire = Time.time + _fireRate;

         // Quaternion.identity = rotation 0

        if (IsTripleShotActive == true)
        {
            Instantiate(_TripleLaser, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
        // if space key press, fire 1 laser
        // if tripleshotActive is true
            // fire 3 lasers  (triple shot prefab)
        //else fire 1 laaser
        // instatiate 3 lasers (triple shot prefab)
    }

    public void Damage()
    {
        _lives--;
        // check if dead
        // destroy us
        if (_lives < 1)
        {
            //Find the GameObject then get Component
            // Communicate with SpawnManager
            //Let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        IsTripleShotActive = true;
        StartCoroutine("TripleShotPowerDownRoutine");
        //tripleShotActive becomes true
        // start the power down coroutine for triple shot
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        IsTripleShotActive = false;
    }
    //Ienumerator TripleShotPowerDownRoutine
    //wait 5 seconds
    // set the triple shot to false
}
