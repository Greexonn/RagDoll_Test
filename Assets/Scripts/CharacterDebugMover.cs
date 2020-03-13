using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterDebugMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Transform _characterTransform;
    private Rigidbody _characterRigidbody;
    
    void Start()
    {
        _characterTransform = transform;
        _characterRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // _characterTransform.Translate(-_characterTransform.right * (_speed * Time.deltaTime));
        _characterRigidbody.AddForce(_characterTransform.forward * _speed);
    }
}
