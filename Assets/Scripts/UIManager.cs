using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to Text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartLevel;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprite;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }


    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprite[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
     void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOver.gameObject.SetActive(true);
        _restartLevel.gameObject.SetActive(true);
        StartCoroutine(GameOverTimedRoutine());
        
    }


    IEnumerator GameOverTimedRoutine()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
}
