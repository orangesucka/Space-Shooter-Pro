using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private AudioClip _audioPowerUp;
    private AudioSource _audioSource;

    //0 = Triple shot, 1 = Speed, 2 = Shield
    [SerializeField]
    private int powerupID;
    private void Start()
    {
        _audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6f)
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
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedUpActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                }
            }
            _audioSource.PlayOneShot(_audioPowerUp, 1);
            Destroy(this.gameObject);
        }
    }
}
