using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9.9f;
    private float _downSpeed = 3f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUP();
        }
        else
        {
            MoveDown();
        }

    }

    void MoveUP()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 7.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * _downSpeed * Time.deltaTime);

        if(transform.position.y < -7.5f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }
        
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

}
