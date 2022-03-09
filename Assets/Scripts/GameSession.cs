using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int _playerLives = 15;
    [SerializeField] int _scorePoints = 0;

    [SerializeField] Text _livesText;
    [SerializeField] Text _scoreText;


    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        _livesText.text = _playerLives.ToString();
        _scoreText.text = _scorePoints.ToString();
    }

    public void ChangeScoreBoard(int pointsToAdd)
    {
        _scorePoints += pointsToAdd;
        _scoreText.text = _scorePoints.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(_playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        _playerLives--;
        var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);

        _livesText.text = _playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
