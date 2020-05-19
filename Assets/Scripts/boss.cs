using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class boss : MonoBehaviour
{
    //private Transform _homePosition;
    [SerializeField]
    private int _dropSpeed = 1, _bossPower = 10;
    //[SerializeField]
    //private Transform _position;
    [SerializeField]
    private AudioClip _enemyExplosion;
    [SerializeField]
    private AudioClip[] _bossLazerSound;
    [SerializeField]
    private GameObject[] _bossLasers;
    [SerializeField]
    private GameObject _bossExplosion;

    //private Animator _animator;
    private Player _player;
    private AudioSource _audioSource;
 
    private bool _slide = true;
    //Move up 2 unitys over 2 to 3 seconds
    //Move back to home

    // Start is called before the first frame update
    void Start()
    {
 
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
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
        BossDrop();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BossFire();
        }
    }
    public void BossDrop()
    {
        //_homePosition = transform.GetComponent<Transform>();
        if (transform.position.y >= 1.5f)
        {
            transform.Translate(Vector3.down * _dropSpeed * Time.deltaTime);
        }
        if (transform.position.y <= 1.5)
        {
            BossSlide();
        }
    }
    public void BossSlide()
    {
        if (_slide == true)
        {
            transform.Translate(Vector3.left * _dropSpeed / 2 * Time.deltaTime);
            if (transform.position.x <= -3)
            {
                _slide = false;
            }
        }
        else
        {
            transform.Translate(Vector3.right * _dropSpeed / 2 * Time.deltaTime);
            if (transform.position.x >= 3)
            {
                _slide = true;
            }
        }
    }
    public void BossFire()
    {
        //fire 4 lazers in a timed rythm
        StartCoroutine(BossLeftLeftLazer());
    }
    IEnumerator BossLeftLeftLazer()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_bossLasers[0], transform.localPosition + new Vector3(-8, -7, 0), Quaternion.identity);
        _audioSource.PlayOneShot(_bossLazerSound[0], 1);
        StartCoroutine(BossLeftRightLazer());
    }
    IEnumerator BossLeftRightLazer()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_bossLasers[1], transform.localPosition + new Vector3(-2, -11, 0), Quaternion.identity);
        _audioSource.PlayOneShot(_bossLazerSound[1], 1);
        StartCoroutine(BossRightLeftLazer());
    }
    IEnumerator BossRightLeftLazer()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_bossLasers[2], transform.localPosition + new Vector3(2, -11, 0), Quaternion.identity);
        _audioSource.PlayOneShot(_bossLazerSound[2], 1);
        StartCoroutine(BossLeftLeftLazer2());
    }
    IEnumerator BossLeftLeftLazer2()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_bossLasers[0], transform.localPosition + new Vector3(-8, -7, 0), Quaternion.identity);
        _audioSource.PlayOneShot(_bossLazerSound[0], 1);
        StartCoroutine(BossLeftRightLazer2());
    }
    IEnumerator BossLeftRightLazer2()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_bossLasers[1], transform.localPosition + new Vector3(-2, -11, 0), Quaternion.identity);
        _audioSource.PlayOneShot(_bossLazerSound[1], 1);
        StartCoroutine(BossRightRightLazer());
    }
    IEnumerator BossRightLeftLazer2()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(_bossLasers[2], transform.localPosition + new Vector3(2, -11, 0), Quaternion.identity);
        _audioSource.PlayOneShot(_bossLazerSound[2], 1);
        //StartCoroutine(BossRightRightLazer());
    }
    IEnumerator BossRightRightLazer()
    {
        yield return new WaitForSeconds(1f);
        _audioSource.PlayOneShot(_bossLazerSound[3], 1);
        Instantiate(_bossLasers[3], transform.localPosition + new Vector3(8, -7, 0), Quaternion.identity);
        StartCoroutine(BossRightLeftLazer2());
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
                    //Instantiate(_bossExplosion, transform.position, Quaternion.identity);
                    //_animator.SetTrigger("OnEnemyDeath");
                    //GetComponent<BoxCollider2D>().enabled = false;
                    //Destroy(this.gameObject, 1f);
            }
        }

        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _bossPower--;
                if (_bossPower == 0)
                {
                    _player.Score(100);
                    _audioSource.PlayOneShot(_enemyExplosion, 1.5f);
                    Instantiate(_bossExplosion, transform.position, Quaternion.identity);
                    //_animator.SetTrigger("OnEnemyDeath");
                    GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(this.gameObject, 1f);
                }
            }
        }
    }
}