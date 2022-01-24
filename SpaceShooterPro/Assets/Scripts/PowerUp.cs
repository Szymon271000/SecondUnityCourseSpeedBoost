using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    //private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        //_player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // move down at a speed of 3 (adjust in the inspector)
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
        // when we leave the screen, destroy this object
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // communicate with the other script
            //collision.transform.GetComponent<Player>().TripleShotActive();
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
                Destroy(this.gameObject);
            }
            
        }
    }
    // OnTriggerCollision
    // only be collectable by the player (use tags)
    // on collected, destroy
}
