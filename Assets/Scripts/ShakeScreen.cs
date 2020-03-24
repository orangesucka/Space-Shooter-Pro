using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    //Transform of the GameObject you want to shake
    private Transform _transform;

    //Desired duration of the shake effect
    private float shakeDuration = 0f;

    //A measure of magnitude for the shake.  Tweak based on your preference
    private float shakeMagnitude = 0.7f;

    //A measure of how quickly the shake effect ends
    private float dampingSpeed = 1.0f;

    //The initial position of the GameObject
    Vector3 initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {

        if (_transform == null)
        {
            _transform = GetComponent<Transform>().transform;
        }
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 1.0f;
    }
}
