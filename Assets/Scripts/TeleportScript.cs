using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private GameObject _vaporTrails;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("The Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _vaporTrails.GetComponent<SpriteRenderer>().enabled = true;
            _animator.SetTrigger("OnTeleport");
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            _vaporTrails.GetComponent<SpriteRenderer>().enabled = false;
            //_animator.SetTrigger("");
        }
    }
}
