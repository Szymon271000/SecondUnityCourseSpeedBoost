using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _PowerUp;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn gameobjects every 5 seconds
    // Create a coroutine of type Ienumerator -- Yield Events
    // while loop

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            //yield return null; // wait 1 frame
            // then this line is called
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
            // Instantiate enemy prefab
            // yield wait for 5 seconds
        }
        // we e=never get here
    }

    IEnumerator SpawnPowerUp()
    {
        // every 3- 7 seconds spawn in a power up
        while (_stopSpawning == false)
        {
            Vector3 PowerUpSpawn = new Vector3(Random.Range(-8f, 8), 7, 0);
            Instantiate(_PowerUp, PowerUpSpawn, Quaternion.identity);
            int WaitTime = Random.Range(3, 7);
            yield return new WaitForSeconds(WaitTime);
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
