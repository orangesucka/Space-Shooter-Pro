using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _falling = 4f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private AudioClip _enemyExplosion;

    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private float _deceleration = 2.5f;
    private Animator _animator; //handle to animator component
    private Player _player;
    private AudioSource _audioSource;

    // Start is called befor the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        //null check player
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }
        //assign the component to Anim
        _animator = GetComponent<Animator>();

        if(_animator == null)
        {
            Debug.LogError("The Animator is NULL");
        }

        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        //null check AuidoSource
        if (_audioSource == null)
        {
            Debug.LogError("The _audioSource on Enemy is NULL");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _falling * Time.deltaTime);

        if (transform.position.y < -4.5)
        {
            float randomX = Random.Range(-9f, 9f);
            if (transform.position.y < -4)
            {
                Destroy(this.gameObject);
            }
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
            //trigger anim
            _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
            _animator.SetTrigger("OnEnemyDeath");
            _falling = _deceleration;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 3f);
        }

         if(other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.Score(10);
            }

            //trigger anim
            _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
            _animator.SetTrigger("OnEnemyDeath");
            _falling =_deceleration;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 3f);
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
