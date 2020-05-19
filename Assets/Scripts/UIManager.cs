using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _gameOver, _restartLevel, _ammoCount, _boosterPercent, _wave2, _wave3, _wave4, _wave5, _wave6, _wave7, _waveBoss, _waveEnd;
    [SerializeField]
    private Image _livesImg, _xImg;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _shieldImg_0, _shieldImg_1, _shieldImg_2;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _wave2.gameObject.SetActive(false);
        _wave3.gameObject.SetActive(false);
        _wave4.gameObject.SetActive(false);
        _wave5.gameObject.SetActive(false);
        _wave6.gameObject.SetActive(false);
        _wave7.gameObject.SetActive(false);
        _waveBoss.gameObject.SetActive(false);
        _waveEnd.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);
        _xImg.gameObject.SetActive(false);

        _ammoCount.text = "Ammo:" + 15 + "/15";
        _boosterPercent.text = "Booster " + 100 + "%";
        
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
        if (playerScore == 20)
        {
            WaveTwoSequence();
        }
        if (playerScore == 40)
        {
            WaveThreeSequence();
        }
        if (playerScore == 60)
        {
            WaveFourSequence();
        }
        if (playerScore == 80)
        {
            WaveFiveSequence();
        }
        if (playerScore == 100)
        {
            WaveSixSequence();
        }
        if (playerScore == 120)
        {
            WaveSevenSequence();
        }
        if(playerScore == 140)
        {
            WaveBossSequence();
        }
        if(playerScore >= 200)
        {
            WaveEndSequence();
        }
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
        _ammoCount.text = "Ammo:" + ammoCount + "/15";
        if(ammoCount == 0)
        {
            _xImg.gameObject.SetActive(true);
        }
        else if(ammoCount > 0)
        {
            _xImg.gameObject.SetActive(false);
        }
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

    void WaveTwoSequence()
    {

        _wave2.gameObject.SetActive(true);
        StartCoroutine(WaveTwoTimedRoutine());
        _spawnManager.Wavetwo();
        StartCoroutine(WaveTwoOver());
    }

    void WaveThreeSequence()
    {

        _wave3.gameObject.SetActive(true);
        StartCoroutine(WaveThreeTimedRoutine());
        _spawnManager.WaveThree();
        StartCoroutine(WaveThreeOver());
    }

    IEnumerator WaveTwoTimedRoutine()
    {
        while (true)
        {
            _wave2.text = "WAVE 2";
            yield return new WaitForSeconds(.5f);
            _wave2.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WaveThreeTimedRoutine()
    {
        while (true)
        {
            _wave3.text = "WAVE 3";
            yield return new WaitForSeconds(.5f);
            _wave3.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WaveTwoOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            _wave2.gameObject.SetActive(false);
        }
    }

    IEnumerator WaveThreeOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _wave3.gameObject.SetActive(false);
        }
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

    void WaveFourSequence()
    {

        _wave2.gameObject.SetActive(true);
        StartCoroutine(WaveFourTimedRoutine());
        _spawnManager.WaveFour();
        StartCoroutine(WaveFourOver());
    }

    void WaveFiveSequence()
    {

        _wave5.gameObject.SetActive(true);
        StartCoroutine(WaveFiveTimedRoutine());
        _spawnManager.WaveFive();
        StartCoroutine(WaveFiveOver());
    }

    IEnumerator WaveFourTimedRoutine()
    {
        while (true)
        {
            _wave4.text = "WAVE 4";
            yield return new WaitForSeconds(.5f);
            _wave4.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WaveFiveTimedRoutine()
    {
        while (true)
        {
            _wave5.text = "WAVE 5";
            yield return new WaitForSeconds(.5f);
            _wave5.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WaveFourOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            _wave4.gameObject.SetActive(false);
        }
    }

    IEnumerator WaveFiveOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _wave5.gameObject.SetActive(false);
        }
    }
    void WaveSixSequence()
    {

        _wave6.gameObject.SetActive(true);
        StartCoroutine(WaveSixTimedRoutine());
        _spawnManager.WaveSix();
        StartCoroutine(WaveSixOver());
    }

    void WaveSevenSequence()
    {

        _wave7.gameObject.SetActive(true);
        StartCoroutine(WaveSevenTimedRoutine());
        _spawnManager.WaveSeven();
        StartCoroutine(WaveSevenOver());
    }

    IEnumerator WaveSixTimedRoutine()
    {
        while (true)
        {
            _wave6.text = "WAVE 6";
            yield return new WaitForSeconds(.5f);
            _wave6.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WaveSevenTimedRoutine()
    {
        while (true)
        {
            _wave7.text = "WAVE 7";
            yield return new WaitForSeconds(.5f);
            _wave7.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator WaveSixOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            _wave6.gameObject.SetActive(false);
        }
    }

    IEnumerator WaveSevenOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _wave7.gameObject.SetActive(false);
        }
    }
    void WaveBossSequence()
    {

        _waveBoss.gameObject.SetActive(true);
        StartCoroutine(WaveBossTimedRoutine());
        _spawnManager.BossWave();
        StartCoroutine(WaveBossOver());
    }
    IEnumerator WaveBossTimedRoutine()
    {
        while (true)
        {
            _waveBoss.text = "WAVE BOSS";
            yield return new WaitForSeconds(.5f);
            _waveBoss.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
    IEnumerator WaveBossOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _waveBoss.gameObject.SetActive(false);
        }
    }
    void WaveEndSequence()
    {

        _waveEnd.gameObject.SetActive(true);
        StartCoroutine(WaveEndRoutine());
        _spawnManager.BossWave();
        StartCoroutine(WaveEndOver());
    }
    IEnumerator WaveEndRoutine()
    {
        while (true)
        {
            _waveEnd.text = "You Win!";
            yield return new WaitForSeconds(.5f);
            _waveEnd.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
    IEnumerator WaveEndOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            _waveEnd.gameObject.SetActive(false);
        }
    }
}