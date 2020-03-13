using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterRagdollController : MonoBehaviour
{
    private Animator _characterAnimator;
    private Collider _characterCollider;
    private Rigidbody _characterRigidbody;
    private CharacterDebugMover _mover;

    private List<Collider> _jointsColliders;
    private List<Rigidbody> _jointsRigidbodies;

    private Vector3 _velocityBeforeHit;
    
    void Start()
    {
        _characterAnimator = GetComponent<Animator>();
        _characterCollider = GetComponent<Collider>();
        _characterRigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<CharacterDebugMover>();

        _jointsColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        _jointsColliders.Remove(_characterCollider);
        _jointsRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        _jointsRigidbodies.Remove(_characterRigidbody);
        
        SetRagdollActive(false);
    }

    private void FixedUpdate()
    {
        if (_characterRigidbody != null)
        {
            _velocityBeforeHit = _characterRigidbody.velocity;
        }
    }

    private void SetRagdollActive(bool value)
    {
        if (!value)
        {
            _jointsColliders.ForEach(collider => collider.enabled = value);
            _jointsRigidbodies.ForEach(rigidbody => rigidbody.useGravity = value);
        }
        else
        {
        
            Destroy(_mover);
            Destroy(_characterRigidbody);
        
            _characterCollider.enabled = false;
            _characterAnimator.enabled = false;
            _jointsColliders.ForEach(collider => collider.enabled = true);
            _jointsRigidbodies.ForEach(rigidbody => rigidbody.useGravity = true);
            _jointsRigidbodies.ForEach(rigidbody => rigidbody.velocity = _velocityBeforeHit);
        }
    }
    
    [ContextMenu("ActivateRagdoll")]
    public void ActivateRagdollFromMenu()
    {
        SetRagdollActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        var impulse = other.impulse.magnitude;
        if (impulse >= 3.0f)
        {
            SetRagdollActive(true);
        }
    }
}
