using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _Player;
    private Animator _Animator;
    [SerializeField] private AudioClip _Distruction;
    private AudioSource _AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Player = GameObject.Find("Player").GetComponent<Player>();
        if (_Player == null)
        {
            Debug.LogError("The player is NULL");
        }
        if (_Animator == null)
        {
            Debug.LogError("Animator is null");
        }
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.clip = _Distruction;
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
            _Animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _AudioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_Player != null)
            {
                _Player.AddScore(10);
            }
            _Player.DistructionSound();
            _Animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }
        // if other is laser
        // destroy laser
        // destroy us
    }
}
