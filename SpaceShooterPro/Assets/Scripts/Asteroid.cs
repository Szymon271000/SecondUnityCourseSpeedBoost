using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _rotation = 20f;
    [SerializeField] GameObject _Explosion;
    private SpawnManager _SpawnManager;
    private Player _Player;
    void Start()
    {
        _SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotation * Time.deltaTime));  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Instantiate(_Explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _Player.DistructionSound();
            _SpawnManager.StarSpawning();
            Destroy(this.gameObject, 0.25f); 
        }
    }
    // check for Laser collision (Trigger)
    // instantiate explosion at the position of the asteroid (us)
    // destroy the explosion after 3 seconds
}
