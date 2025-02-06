using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    //shake variables
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 0.7f;

    //get camera position
    Vector3 initialPosition;


    //store camera's transform
    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    //store camera's intial position
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //if shaking, adjust transform by random factor
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            //decrement shake for fade out effect
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            //revert position of camera when done shaking
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    //create function to trigger in NPC shooting script
    public void TriggerShake()
    {
        shakeDuration = 0.2f;
    }
}
