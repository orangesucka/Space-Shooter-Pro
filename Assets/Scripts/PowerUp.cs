using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private AudioClip _audioPowerUp;
    private AudioSource _audioSource;

    //0 = Triple shot, 1 = Speed, 2 = Shield, 3 = Ammo Refill, 4 = One up, 5 = BFL, 6 = PowerDown;
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
                        //Debug.Log("Case 2 Active");
                        player.ShieldActive();
                        break;
                    case 3:
                        //Debug.Log("Case 3 Active");
                        player.PowerDownActive();
                        break;
                    case 4:
                        //Debug.Log("Case 4 Active");
                        player.OneUp();
                        break;
                    case 5:
                        //Debug.Log("Case 5 Active");
                        player.BigFNLaserActive();
                        break;
                    case 6:
                        //Debug.Log("Case 6 Active");
                        player.AmmoRefillActive();
                        break;
                }
            }
            _audioSource.PlayOneShot(_audioPowerUp, 1);
            Destroy(this.gameObject);
        }
    }
}
