using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalHand : MonoBehaviour
{
    [SerializeField] private GameObject _followObject;
    [SerializeField] private float _followSpeed = 30f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;

    private Transform _followTarget;
    private Rigidbody _rigidbody;

    void Start()
    {
        _followTarget = _followObject.transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.mass = 20f;

        _rigidbody.position = _followTarget.position;
        _rigidbody.rotation = _followTarget.rotation;
    }

    void Update()
    {
        PhysicalMovement();
    }

    void PhysicalMovement()
    {
        // // Position 
        var distance = Vector3.Distance(_followTarget.position, transform.position);
        _rigidbody.velocity = (_followTarget.position - transform.position).normalized * (_followSpeed * distance);

        // // Rotation
        var rotation = _followTarget.rotation * Quaternion.Inverse(_rigidbody.rotation);
        rotation.ToAngleAxis(out float angle, out Vector3 axis);
        _rigidbody.angularVelocity = angle * (axis * Mathf.Deg2Rad * _rotationSpeed);
    }
}