using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _horizontalSpeed = 5.5f, _fireRate = 0.15f, _canFire = -1f, _verticalSpeed = 5.5f, _turboThrusters = 1, _shieldRotationSpeed = 10, _startTime = 0f, _timer = 1f, _boostPer, _rotateSpeed;
    [SerializeField]
    private int _score, _shieldPower, _ammo;
    [SerializeField]
    private int _lives = 3, _speedBoostAmount = 3;

    public Joystick _joystickL, _joysticR;
    Vector2 _origin;

    [SerializeField]
    private GameObject _lazerPrefabs, _Triple_Shot, _bFL, _shieldPrefab, _oneUp, _thrusters, _turboThruster, _boostThruster, _rightEngine, _leftEngine;
    [SerializeField]
    private AudioClip _lazerSound, _explosion, _powerUPSound;

    private AudioSource _audioSource;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private ShakeScreen _shake;

    private bool _tripleShot = false;
    private bool _bFLBool = false;
    private bool _speedBoostActive = false;
    private bool _shieldActiveBool = false;
    private bool _outOfAmmo = false;
    private bool _ammoEmpty = false;
    private bool _thrustersbool = false;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 100;
        _shake = GameObject.Find("Main Camera").GetComponent<ShakeScreen>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager in Player is NULL.");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("The UI Manager in Player is NULL");
        }
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource in Player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        
        MovementCalculations();
        ShieldSpinny();

        if(_joysticR.Horizontal > 0)
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            _canFire = Time.time + _fireRate;
            FireLazer();
        }
        if ((_joysticR.Horizontal < 0) && _thrustersbool == false);
        //if (Input.GetKey(KeyCode.LeftShift) && _thrustersbool == false)
        {
            _timer--;
            if (_timer <= 0)
            {
                _timer = 0;
            }
            //Debug.Log(_timer);
            Boost(_timer);
            Thruster();
            StartCoroutine(ThrusterPowerDownRoutine());
        }      
    }

        
    void MovementCalculations()
        {
        float verticalInput = _joystickL.Vertical;
        float horizontalInput = _joystickL.Horizontal;
        //float verticalInput = Input.GetAxis("Vertical");
        //float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.up * verticalInput * _verticalSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * _horizontalSpeed * Time.deltaTime);
        _thrusters.GetComponent<SpriteRenderer>().enabled = true;
        _boostThruster.GetComponent<SpriteRenderer>().enabled = false;


        if (_speedBoostActive == true)
        {
            transform.Translate(Vector3.up * verticalInput * (_verticalSpeed + _speedBoostAmount) * Time.deltaTime);
            transform.Translate(Vector3.right * horizontalInput * (_horizontalSpeed + _speedBoostAmount) * Time.deltaTime);
        }
        if (transform.position.y >= 4.25f)
        {
            transform.position = new Vector3(transform.position.x, 4.25f, 0);
        }
        else if (transform.position.y <= -3.95f)
        {
            transform.position = new Vector3(transform.position.x, -3.95f, 0);
        }

        if (transform.position.x >= 8.9f)
        {
            transform.position = new Vector3(8.9f, transform.position.y, 0);
        }

        else if (transform.position.x <= -8.9f)
        {
            transform.position = new Vector3(-8.9f, transform.position.y, 0);
        }
    }

    void FireLazer()
    {

        _canFire = Time.time + _fireRate;

        if (_ammo <= 0)
        {
            _outOfAmmo = true;
        }

        else if (_tripleShot == true)
        {
            Instantiate(_Triple_Shot, transform.localPosition + new Vector3(0f, 0f, 0f), Quaternion.Euler(transform.localEulerAngles));
            _audioSource.PlayOneShot(_lazerSound, 1);
        }

        else if (_outOfAmmo == false)
        {
            Ammo(1);//Amount of ammo a "FireLazer" uses
            Instantiate(_lazerPrefabs, transform.localPosition + new Vector3(0f, 0f, 0), Quaternion.Euler(transform.localEulerAngles));
            _audioSource.PlayOneShot(_lazerSound, 1f);
        }
    }
    public void Thruster()
    {
        float verticalInput = _joystickL.Vertical;
        float horizontalInput = _joystickL.Horizontal;
        //float verticalInput = Input.GetAxis("Vertical");
        //float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.up * verticalInput * (_verticalSpeed + _turboThrusters) * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * (_horizontalSpeed + _turboThrusters) * Time.deltaTime);
        _boostThruster.GetComponent<SpriteRenderer>().enabled = true;
        _thrusters.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(ThrusterPowerDownRoutine());
    }

    IEnumerator ThrusterPowerDownRoutine()
    {
        yield return new WaitForSeconds(1f);
        _thrusters.GetComponent<SpriteRenderer>().enabled = true;
        _boostThruster.GetComponent<SpriteRenderer>().enabled = false;
        _thrustersbool = true;
        StartCoroutine(SomethingInBetween());
    }

    IEnumerator SomethingInBetween()
    {
        yield return new WaitForSeconds(1f);

        _timer++;
        if(_timer >= 100)
        {
            _timer = 100;
        }
        Boost(_timer);
        //Debug.Log(_timer);
        StartCoroutine(ThrustersRecharge());
    }

    IEnumerator ThrustersRecharge()
    {
        yield return new WaitForSeconds(1f);
        _thrustersbool = false;
    }

    public void SpeedUpActive()
    {
        _speedBoostActive = true;
        _thrusters.GetComponent<SpriteRenderer>().enabled = false;
        _turboThruster.GetComponent<SpriteRenderer>().enabled = true;
        _audioSource.PlayOneShot(_powerUPSound, 1);
        StartCoroutine(SpeedUpPowerDownRoutine());
    }

    IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _turboThruster.GetComponent<SpriteRenderer>().enabled = false;
        _thrusters.GetComponent<SpriteRenderer>().enabled = true;
        _speedBoostActive = false;
       
    }
    public void ShieldSpinny()
    {
        _uiManager.UpdateShields(_shieldPower);
        if (_shieldPower == 3)
        {
            _shieldPrefab.GetComponent<Transform>().Rotate(0, 14 * _shieldRotationSpeed * Time.deltaTime, 0);
            ChangeColor(_shieldPrefab, Color.white);
        }
        if (_shieldPower == 2)
        {
            _shieldPrefab.GetComponent<Transform>().Rotate(0, 7 * _shieldRotationSpeed * Time.deltaTime, 0);
            ChangeColor(_shieldPrefab, Color.blue);
        }
        if (_shieldPower == 1)
        {
            _shieldPrefab.GetComponent<Transform>().Rotate(0, 1 * _shieldRotationSpeed * Time.deltaTime, 0);
            ChangeColor(_shieldPrefab, Color.grey);
        }
    }
    private void ChangeColor(GameObject obj, Color colorToAssign)
    {
        _shieldPrefab.GetComponent<SpriteRenderer>().material.color = colorToAssign;
    }
    public void ShieldActive()
    {
        //Debug.Log("Shield Active");
        _shieldActiveBool = true;
        _audioSource.PlayOneShot(_powerUPSound, 1);
        _shieldPrefab.GetComponent<SpriteRenderer>().enabled = true;
        _shieldPower = 3;
    }

    public void Damage()
    {
        
        if (_shieldActiveBool == true)
        {
            _shieldPower--;
            if (_shieldPower == 0)
            {
                _shieldActiveBool = false;
                _shieldPrefab.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else if (_shieldActiveBool == false)
        {
            _lives--;
            if (_lives == 2)
            {
                _shake.TriggerShake();
                _rightEngine.GetComponent<SpriteRenderer>().enabled = true;
                _audioSource.PlayOneShot(_explosion, 1.5f);
            }
            else if (_lives == 1)
            {
                _shake.TriggerShake();
                _leftEngine.GetComponent<SpriteRenderer>().enabled = true;
                _audioSource.PlayOneShot(_explosion, 1.5f);
            }

            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _shake.TriggerShake();
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }
    public void PowerDownActive()
    {
        _ammoEmpty = true;
        _ammo = 0;
        _uiManager.UpdateAmmo(_ammo);
        StartCoroutine(PowerDownRoutine());
    }
    IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        _ammoEmpty = false;
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

    public void AmmoRefillActive()
    {
        _ammo = 15;
        _uiManager.UpdateAmmo(_ammo);
        _outOfAmmo = false;
    }

    public void BigFNLaserActive()
    {
        _bFLBool = true;
        _outOfAmmo = true;
        _audioSource.PlayOneShot(_powerUPSound, 1);
        _bFL.GetComponent<SpriteRenderer>().enabled = true;
        _bFL.GetComponent<BoxCollider2D>().enabled = true;
        _audioSource.PlayOneShot(_lazerSound, 1);
        StartCoroutine(BFNPowerDown());
    }
    IEnumerator BFNPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _bFL.GetComponent<SpriteRenderer>().enabled = false;
        _bFL.GetComponent<BoxCollider2D>().enabled = false;
        _bFLBool = false;
        _outOfAmmo = false;
    }

    public void OneUp()
    {
        if (_lives < 3)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
        }
        else if (_lives >= 3)
        {
            _lives = 3;
        }
    }

    public void Score(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void Ammo(int bullets)
    {
        _ammo -= bullets;
        _uiManager.UpdateAmmo(_ammo);
    }

    public void Boost(float fuel)
    {
        _boostPer = fuel;
        _uiManager.UpdateBoost(_boostPer);
    }
}