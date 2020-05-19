using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRighttoLeftZag : MonoBehaviour
{
    [SerializeField]
    private float _falling = 2f, _rotationSpeed, _fireRate = 1f, _canFire = -1, _deceleration = 1f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private AudioClip _enemyExplosion;
    
    private Animator _animator;
    private Player _player;
    private AudioSource _audioSource;


    // Start is called befor the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }
      
        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("The Animator is NULL");
        }

        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The _audioSource on Enemy is NULL");
        }
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
            _fireRate = Random.Range(.1f, 1f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _falling * Time.deltaTime);
        if (transform.position.y < 1)
        {
            transform.Translate(Vector3.left * _falling * Time.deltaTime);
        }
        if (transform.position.y > 1)
        {
            transform.Translate(Vector3.right * _falling * Time.deltaTime);
        }
        if (transform.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
            _animator.SetTrigger("OnEnemyDeath");
            _falling = _deceleration;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 1f);
        }
        if(other.tag == "BFLTag")
        {
            if(_player != null)
            {
                _player.Score(10);
            }
            _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
            _animator.SetTrigger("OnEnemyDeath");
            _falling = _deceleration;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 1f);
        }

         if(other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.Score(10);
            }
            _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
            _animator.SetTrigger("OnEnemyDeath");
            _falling =_deceleration;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 1f);
        }

         if(other.tag == "Atmosphere")
        {
            if(_player != null)
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
