using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    [SerializeField]
    private float _falling = 2f, _deceleration = 2.5f, _fireRate = 3.0f, _canFire = -1;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private SpriteRenderer _enemyShield;
    [SerializeField]
    private AudioClip _enemyExplosion;
    [SerializeField]
    private int _shieldPower = 2;
    private bool _shieldActiveBool = true;

    private Animator _animator;
    private Player _player;
    private AudioSource _audioSource;
    private SpriteRenderer _gameObjectShield;
    private CircleCollider2D _2dCircleColider;

    // Start is called befor the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The Animator is NULL");
        }

        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The _audioSource on Enemy is NULL");
        }
        _gameObjectShield = GameObject.Find("Shield").GetComponent<SpriteRenderer>();
        _2dCircleColider = GameObject.Find("Shield").GetComponent<CircleCollider2D> (); 
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        EnemyFire();
    }
    void EnemyFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0,-2,0), Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _falling * Time.deltaTime);
        
        if (transform.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                _shieldPower--;
                if (_shieldPower == 1)
               
                {
                    _shieldActiveBool = false;
                    _enemyShield.GetComponent<SpriteRenderer>().enabled = false;
                    _enemyShield.GetComponent<CircleCollider2D>().enabled = false;
                }
                if (_shieldPower == 0)
                {
                    player.Damage();
                    _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                    _animator.SetTrigger("OnEnemyDeath");
                    _falling = _deceleration;
                    GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(this.gameObject, 3f);
                }
            }
        }

        if (other.tag == "BFLTag")
        {
            if (_player != null)
            {
                if (_shieldActiveBool == true)
                {
                    _shieldActiveBool = false;
                    _enemyShield.GetComponent<SpriteRenderer>().enabled = false;
                    _enemyShield.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
            if (_shieldActiveBool == false)
            {
                _player.Score(10);
                _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                _animator.SetTrigger("OnEnemyDeath");
                _falling = _deceleration;
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _shieldPower--;
                if (_shieldPower == 1)
                { 
                    _shieldActiveBool = false;
                    _enemyShield.GetComponent<SpriteRenderer>().enabled = false;
                    _enemyShield.GetComponent<CircleCollider2D>().enabled = false;
                }        
                if (_shieldPower == 0)
                {
                    _player.Score(10);
                    _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                    _animator.SetTrigger("OnEnemyDeath");
                    _falling = _deceleration;
                    GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(this.gameObject, 3f);
                }
            }
        }

        if (other.tag == "Atmosphere")
        {
            if (_player != null)
            {
                _player.Score(-10);
            }
            _audioSource.PlayOneShot(_enemyExplosion);
            _animator.SetTrigger("OnEnemyDeath");
            _enemyShield.GetComponent<SpriteRenderer>().enabled = false;
            _enemyShield.GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 3f);
        }
    }
}
