using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class EnemyUpShotScript: MonoBehaviour
{
    [SerializeField]
    private float _falling = 2f, _deceleration = 2.5f;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    GameObject _laserPrefab;
    [SerializeField]
    private Vector2 _size;
    [SerializeField]
    private AudioClip _enemyExplosion;

    private float _fireRate = 3.0f, _canFire = -1;

    private Animator _animator;
    private Player _player;
    private AudioSource _audioSource;
    private Rigidbody2D _rb;
    private Transform _target;

    [SerializeField]
    private Vector3 _origin;
    private Vector3 _direction;
    private bool _rayCastBool;

    private float currentHitDistance;
    // Start is called befor the first frame update
    private void Start()
    {
        _origin = transform.position;

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
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        Raycaster();
    }
    void Raycaster()
    {
    _origin = transform.position + new Vector3(0, 9, 0);
    _direction = new Vector2(1, 0);
    RaycastHit2D boxResult;
    boxResult = Physics2D.BoxCast(_origin, _size, 0f, _direction, maxDistance);
        //Debug.Log(boxResult.collider.name);
        if (boxResult.collider.tag != "Player")
        {
            boxResult.collider.tag = null;
            _rayCastBool = false;
            if (transform.position.y < -6.5)
            {
                Destroy(this.gameObject);
            }
        }
        else if (boxResult.collider.tag == "Player")
        {
            _rayCastBool = true;
            Debug.Log("POOP");
            EnemyFire();
            
            if (_rb.position.y< -6.5)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Debug.DrawLine(_origin, _origin + _direction * currentHitDistance);
        Gizmos.DrawCube(_origin + _direction * currentHitDistance, _size);
    }

    void EnemyFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        if (_rayCastBool == true && transform.position.x <= -1)
        {
            transform.Translate(Vector3.right * _falling * Time.deltaTime);
        }
        if(_rayCastBool == true && transform.position.x >= 1)
        {
            transform.Translate(Vector3.left * _falling * Time.deltaTime);
        }
        else if(_rayCastBool == false)
        transform.Translate(Vector3.down * _falling * Time.deltaTime);

        if (transform.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }
        if(transform.position.x > 9.5 || transform.position.x < -9.5 )
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
                    player.Damage();
                    _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                    _animator.SetTrigger("OnEnemyDeath");
                    _falling = _deceleration;
                    GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(this.gameObject, 3f);
                }
            }
      

        if (other.tag == "BFLTag")
        {
            if (_player != null)
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
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 3f);
        }
    }
}
