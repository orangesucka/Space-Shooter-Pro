using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int __rotationSpeed = 4;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _astroidExplosion;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * __rotationSpeed * Time.deltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Lazer")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _audioSource.PlayOneShot(_astroidExplosion, 1.5f);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, .25f);
        }
    }
}
