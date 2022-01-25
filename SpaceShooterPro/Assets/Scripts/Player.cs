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
    private bool IsTripleShotActive = false;
    private bool IsSuperSpeedActive = false;
    [SerializeField] private float _SuperSpeed = 10.0f;
    private bool IsShieldsActive = false;
    [SerializeField] private GameObject _ShieldsVisualizer;
    private int _Score;
    private UIManager _UIManager;
    [SerializeField] GameObject _RightEngine;
    [SerializeField] GameObject _LeftEngine;

    // variable for IsTripleShotActive
    // variable reference to the shield visualizer

    // Start is called before the first frame update
    void Start()
    {
        // take turrent position = new position (0,0,0);
        _Score = 0;
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null");
        }
        if (_UIManager == null)
        {
            Debug.LogError("UI Manager is null");
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

        if (IsSuperSpeedActive == false)
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0));
        }
        else if (IsSuperSpeedActive == true)
        {
            SuperSpeedActive();
        }
        

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
        // if shileds is active
        // do nothing...
        //deactiv shields
        //return;
        if (IsShieldsActive == true)
        {
            IsShieldsActive = false;
            _ShieldsVisualizer.SetActive(false);
            //disable the visualizer
            return;       
        }
        else
        {
            _lives--;
            if (_lives == 2)
            {
                _RightEngine.SetActive(true);
            }
            else if (_lives == 1)
            {
                _LeftEngine.SetActive(true);
            }
            //if lives is 2
            //enable right engine
            // else if lives is 1
            // enable left engine
            _UIManager.UpdateLives(_lives);
            // check if dead
            // destroy us
            if (_lives < 1)
            {
                //Find the GameObject then get Component
                // Communicate with SpawnManager
                //Let them know to stop spawning
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                _UIManager.GameOver();
            }
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
    public void SuperSpeedActive()
    {
        IsSuperSpeedActive = true;
        StartCoroutine("SuperSpeed");
    }

    IEnumerator SuperSpeed() 
    {
        float horizontalInput = Input.GetAxis("Horizontal") * _SuperSpeed * Time.deltaTime; // Input.GetAxis returns float 
        float verticalInput = Input.GetAxis("Vertical") * _SuperSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0));
        yield return new WaitForSeconds(5.0f);
        IsSuperSpeedActive = false;
    }

    public void ShieldsActive()
    {
        IsShieldsActive = true;
        _ShieldsVisualizer.SetActive(true);
        //enable the visualizer
    }

    public void AddScore(int points)
    {
        _Score += points;
        _UIManager.UpdateScore(_Score);
    }
    // method to add 10 to the score
    //Communicate with the UI to update the score
}
