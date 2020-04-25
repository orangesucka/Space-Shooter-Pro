using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikaziScript : MonoBehaviour
{
    [SerializeField]
    private float _falling = 3.0f;
    [SerializeField]
    private float _rotateSpeed = 600f;
    [SerializeField]
    private AudioClip _enemyExplosion;

    //[SerializeField]
    private Transform _target;
    private Transform _angle;

    private Animator _animator;
    private AudioSource _audioSource;
    private Player _player;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Player").transform;
        if(_target == null)
        {
            Debug.LogError("_target is null");
        }
        _player = GameObject.Find("Player").GetComponent <Player>();
        if(_player == null)
        {
            Debug.LogError("Player in Kamikazi = Null");
        }
        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("Animator in Kamikazi = Null");
        }
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource in Kamikazi = Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if (_target == null)
        {
            transform.Translate(Vector3.down * _falling * Time.deltaTime);
            Destroy(this.gameObject);
        }
        else
        {
            //Heat seaking  Script
            Vector2 direction = (Vector2)_target.position - _rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transform.up * _falling;
        }
        if (_rb.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(_player != null)
            {
                player.Damage();
                _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                _animator.SetTrigger("OnEnemyDeath");
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
        if(other.tag == "BFLTag")
        {
            if (_player != null)
            {
                _player.Score(10);
                _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                _animator.SetTrigger("OnEnemyDeath");
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
        if(other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.Score(10);
                _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                _animator.SetTrigger("OnEnemyDeath");
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
        if (other.tag == "Atmosphere")
        {
            if (_player != null)
            {
                _player.Score(10);
                _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                _animator.SetTrigger("OnEnemyDeath");
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
    }
}
