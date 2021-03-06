using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool _IsGameOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _IsGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _IsGameOver == true)
        {
            Application.Quit();
        }
    }
    public void GameOver()
    {
        _IsGameOver = true;
    }
}
