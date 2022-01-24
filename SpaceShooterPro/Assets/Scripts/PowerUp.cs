using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    //ID for Powerups
    //0 = Triple Shot
    //1 = Speed
    //2 = Shields
    [SerializeField]private int PowerUpId; // 0 = TripleShoy 1 = Speed 2 = Shields
    // Start is called before the first frame update
    void Start()
    {
        
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
                switch (PowerUpId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SuperSpeedActive();
                        break;
                    case 2:
                        Debug.Log("Shields Up");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
    // OnTriggerCollision
    // only be collectable by the player (use tags)
    // on collected, destroy
}
