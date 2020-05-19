using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFL_LazerScript : MonoBehaviour
{
    [SerializeField]
    private float _downSpeed = 2f;
    [SerializeField]
    private GameObject _enemyLazerExplosionPrefab;
    [SerializeField]
    private AudioClip _enemyLazerExplosion;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        {
            transform.Translate(Vector3.down * _downSpeed * Time.deltaTime);
        }
        if (transform.position.y < -15f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
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
            }
            //Destroy(this.gameObject);
        }

        if (other.tag == "PowerUo")
        {
            Instantiate(_enemyLazerExplosionPrefab, transform.position, Quaternion.identity);
            _audioSource.PlayOneShot(_enemyLazerExplosion, 1.5f);
            Destroy(other.gameObject);
        }
    }
}