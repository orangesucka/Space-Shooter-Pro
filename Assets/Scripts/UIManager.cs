using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _gameOver, _restartLevel, _ammoCount, _boosterPercent;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _shieldImg_0, _shieldImg_1, _shieldImg_2;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);
        _ammoCount.text = "Ammo:" + 15;
        _boosterPercent.text = "Booster " + 100 + "%";

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
        _livesImg.sprite = _livesSprite[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    public void UpdateShields(int currentShields)
    {
        if(currentShields == 0)
        {
            _shieldImg_0.gameObject.SetActive(false);
            _shieldImg_1.gameObject.SetActive(false);
            _shieldImg_2.gameObject.SetActive(false);
        }

        else if (currentShields == 3)
        {
            _shieldImg_0.gameObject.SetActive(true);
            _shieldImg_1.gameObject.SetActive(true);
            _shieldImg_2.gameObject.SetActive(true);
        }
        else if(currentShields == 2)
        {
            _shieldImg_0.gameObject.SetActive(true);
            _shieldImg_1.gameObject.SetActive(true);
            _shieldImg_2.gameObject.SetActive(false);
        }
        else if(currentShields == 1)
        {
            _shieldImg_0.gameObject.SetActive(true);
            _shieldImg_1.gameObject.SetActive(false);
            _shieldImg_2.gameObject.SetActive(false);
        }
    }
    public void UpdateAmmo(int ammoCount)
    {
        _ammoCount.text = "Ammo:" + ammoCount;        
    }

    public void UpdateBoost(float boosterPercent)
    {
        _boosterPercent.text = "Booster " + boosterPercent + " %";
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
