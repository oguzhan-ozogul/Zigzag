using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    public Text scoreText, goScoretext;
    public Text highScoreText;
    public GameObject gameOver;
    public GameObject mainMenu;
    public GameObject ballPlayer;
    public BallMove ballMove;
    private int diamondScore;
    public Text diamondText;
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        diamondScore = 0;
    }


    public enum GameState
    {
        Prepare,
        MainGame,
        Gameover,
    }
    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    mainMenu.SetActive(true);
                    break;
                case GameState.MainGame:
                    mainMenu.SetActive(false);
                    break;
                case GameState.Gameover:
                    
                    break;
            }
            _currentGameState = value;
        }
    }

    public void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Prepare:
                //ballPlayer.SetActive(false);
                break;
            case GameState.MainGame:
                ballMove.PlayerMovement();
                HighScore();
                //ballPlayer.SetActive(true);
                break;
            case GameState.Gameover:
                gameOver.SetActive(true);
                break;
        }

    }
    public void UpdateScore()
    {
        score++;
        scoreText.text = goScoretext.text;
        scoreText.text = score.ToString();
        goScoretext.text = scoreText.text;
    }
    
    public void HighScore()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void PlayButton()
    {
        CurrentGameState = GameState.MainGame;
    }
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void CollectDiamond()
    {
        diamondScore++;
        diamondText.text = diamondScore.ToString();
    }
}