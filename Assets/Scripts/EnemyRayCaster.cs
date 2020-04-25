using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayCaster : MonoBehaviour
{
    public Vector2 size;
    public float maxDistance = 5f;

    public float _rotateSpeed = 600f;
    public float _falling = 3.0f;

    //[SerializeField]
    private Transform _target;
    //private Transform _angle;
    [SerializeField]
    private AudioClip _enemyExplosion;

    [SerializeField]
    private Vector3 _origin;
    private Vector3 _direction;

    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _audioSource;
    private Player _player;

    private float currentHitDistance;

    // Start is called before the first frame update
    void Start()
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
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _origin = transform.position;
        _direction = new Vector2(0, -1);
        RaycastHit2D boxResult;
        boxResult = Physics2D.BoxCast(_origin, size, 0f, _direction, maxDistance);
        //Debug.Log(boxResult.collider.name);
        if (boxResult.collider.tag == "Player")
        {
            Vector2 direction = (Vector2)_target.position - _rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transform.up * _falling;

            if (_rb.position.y < -6.5)
            {
                Destroy(this.gameObject);
                //Debug.Log("POOP");
            }
        }
        else if(boxResult.collider.tag != "Player")
        {
            transform.Translate(Vector3.forward * _falling * Time.deltaTime);

            if (transform.position.y < -6.5)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Debug.DrawLine(_origin, _origin + _direction * currentHitDistance);
        Gizmos.DrawCube(_origin + _direction * currentHitDistance, size);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (_player != null)
            {
                player.Damage();
                _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                _animator.SetTrigger("OnEnemyDeath");
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
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject, 3f);
            }
        }
        if (other.tag == "Lazer")
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