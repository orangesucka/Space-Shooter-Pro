using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _horizontalSpeed = 5.5f;
    [SerializeField]
    private float _verticalSpeed = 5.5f;
    [SerializeField]
    private GameObject _lazerPrefabs;      
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _Triple_Shot;
    [SerializeField]
    private float _fireRate = .1f;
    [SerializeField]
    private int _speedBoostAmount = 2;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private int _score;
    [SerializeField]
    private AudioClip _lazerSound, _explosion, _powerUPSound;
   
    private AudioSource _audioSource;
    private UIManager _uiManager;
    private bool _shieldActiveBool = false;
    private SpawnManager _spawnManager;
    private float _canFire = 0f;
    private bool _tripleShot = false;
    private bool _speedBoostActive = false;

    // Start is called before the first frame update
    void Start()
    {
       
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if(_uiManager == null)
        {
            Debug.Log("The UI Manager is NULL");
        }
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculations();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLazer();
        }
    }

    void MovementCalculations()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.up * verticalInput * _verticalSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * _horizontalSpeed * Time.deltaTime);

        if (_speedBoostActive == true)
        {
            transform.Translate(Vector3.up * verticalInput * (_verticalSpeed + _speedBoostAmount) * Time.deltaTime);
            transform.Translate(Vector3.right * horizontalInput * (_horizontalSpeed + _speedBoostAmount) * Time.deltaTime);
        }
        if (transform.position.y >= 7.5f)
        {
            transform.position = new Vector3(transform.position.x, -5.5f, 0);
        }
        else if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(transform.position.x, 7.5f, 0);
        }

        if (transform.position.x >= 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
    }

    void FireLazer()
    {
        //I want to not allow more than "3 lazers" on the screen at once
        _canFire = Time.time + _fireRate;

        if (_tripleShot == true)
        {
            Instantiate(_Triple_Shot, transform.localPosition + new Vector3(-0.3f, 1.2f, 0f), Quaternion.identity);
            _audioSource.PlayOneShot(_lazerSound, 1);
        }
        else
        {
            Instantiate(_lazerPrefabs, transform.localPosition + new Vector3(0f, 1.5f, 0), Quaternion.identity);
            _audioSource.PlayOneShot(_lazerSound,1f);
        }        
        //Play lazer audio clip
    }

    public void SpeedUpActive()
    {
        _speedBoostActive = true;
        _audioSource.PlayOneShot(_powerUPSound, 1);
        StartCoroutine(SpeedUpPowerDownRoutine());
    }
    
    IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
    }

    public void ShieldActive()
    {
        _shieldActiveBool = true;
        _audioSource.PlayOneShot(_powerUPSound, 1);
        _shieldPrefab.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Damage()
    {        
        if (_shieldActiveBool == true)
        {
            _shieldActiveBool = false;
            _shieldPrefab.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        _lives--;
        if(_lives == 2)
        {
            _rightEngine.GetComponent<SpriteRenderer>().enabled = true;
            _audioSource.PlayOneShot(_explosion, 1.5f);
        }
        else if(_lives == 1)
        {
            _leftEngine.GetComponent<SpriteRenderer>().enabled = true;
            _audioSource.PlayOneShot(_explosion, 1.5f);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _tripleShot = true;
        _audioSource.PlayOneShot(_powerUPSound, 1);
        StartCoroutine(TripleShotPowerDownRoutine());
    }
 
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShot = false;
    }

    public void Score(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}