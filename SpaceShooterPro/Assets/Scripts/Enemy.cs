using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _Player;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // move down at 4 meters per second
        // if bottom at screen
        // respwan at top a new random x position
        if (transform.position.y < -6 )
        {
            float RandomX = Random.Range(-9, 9);
            transform.position = new Vector3(RandomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is player
        //damage the player
        //Destroy us 
        if (other.tag == "Player")
        {

            //other.transform.GetComponent<Player>().Damage();
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_Player != null)
            {
                _Player.AddScore(10);
            }
            Destroy(this.gameObject);
        }
        // if other is laser
        // destroy laser
        // destroy us
    }
}
