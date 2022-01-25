using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to Text
    [SerializeField] private Text _ScoreText;
    [SerializeField] private Image _LivesImage;
    [SerializeField] private Sprite[] _LiveSprites;
    [SerializeField] private Text _GameOverText;
    [SerializeField] private Text _RestartText;
    private Reload reload;
    private bool _IsGameOver;
    // Start is called before the first frame update
    void Start()
    {
        _IsGameOver = false;
        _RestartText.enabled = false;
        _GameOverText.enabled = false;
        _ScoreText.text = "Score: 0";
        reload = GameObject.Find("ReloadManager").GetComponent<Reload>();
    }

    // Update is called once per frame
    public void UpdateScore(int PlayerScore)
    {
        _ScoreText.text = $"Score {PlayerScore.ToString()}";
    }

    public void UpdateLives (int CurrentLives)
    {
        // display img sprite 
        // give it a new one based on the current lives
        _LivesImage.sprite = _LiveSprites[CurrentLives];
    }

    public void GameOver()
    {
        _IsGameOver = true;
        if (_IsGameOver == true)
        {
            _GameOverText.enabled = true;
            _RestartText.enabled = true;
            reload.GameOver();
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            _GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _GameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
