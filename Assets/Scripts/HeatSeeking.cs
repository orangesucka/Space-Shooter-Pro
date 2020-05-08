using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeeking : MonoBehaviour
{
    public Vector2 size;
    [SerializeField]
    private Vector3 _origin, _direction;
    public float maxDistance = 5f, _rotateSpeed = 600, _falling = 3.0f;
    public int i;

    [SerializeField]
    private GameObject[] _target;
    [SerializeField]
    private Transform[] _destination;
    [SerializeField]
    private AudioClip _enemyExplosion;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _target = GameObject.FindGameObjectsWithTag("Enemy");
        _destination = new Transform[_target.Length];
        for(int i = 0; i < _target.Length; i++)
        {
            _destination[i] = _target[i].transform;
        }

        MoveUP();
        _origin = transform.position;
        _direction = new Vector2(0, -1);
        RaycastHit2D boxResult;
        boxResult = Physics2D.BoxCast(_origin, size, 0f, _direction, maxDistance);

        if (boxResult.collider.tag == "Enemy")
        {
            Vector2 direction = (Vector2)_destination[i].position - _rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
            _rb.velocity = transform.up * _falling;

            if (_rb.position.y < -6.5)
            {
                Destroy(this.gameObject);
            }
        }
        else if (boxResult.collider.tag != "Enemy")
        {
            MoveUP();

            if (transform.position.y < -6.5)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
    void MoveUP()
    {
        transform.Translate(Vector3.up * _falling * Time.deltaTime);

        if (transform.position.y > 7.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }
}