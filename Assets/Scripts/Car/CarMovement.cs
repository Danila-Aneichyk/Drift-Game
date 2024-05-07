using System;
using DriftSystem;
using Points;
using UnityEngine;

namespace Car
{
    [Serializable]
    public class Wheel
    {
        public Transform WheelTransform;
        public WheelCollider WheelCollider;
        public bool IsForwardWheel; 

        public void UpdateWheelMeshPosition()
        {
            Vector3 position;
            Quaternion rotation;

            WheelCollider.GetWorldPose(out position, out rotation);

            WheelTransform.position = position;
            WheelTransform.rotation = rotation;
        }
    }

    [RequireComponent(typeof(Rigidbody))]
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _steeringCurve;
        [SerializeField] private Timer _timer; 

        [SerializeField] private Wheel[] _wheels;

        [Header("Car movement values")]
        [SerializeField] private int _motorForce;
        [SerializeField] private Transform _centreOfMass;
        [SerializeField] private int _brakeForce;
        [SerializeField] private float _brakeInput;

        private float _verticalInput;
        private float _horizontalInput;

        private bool _isDrifting = false;

        private float _speed;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.centerOfMass = _centreOfMass.localPosition;
            _timer.TimerExpired += OnTimerExpired;
        }

        private void Update()
        {
            Move();
            Brake();
            Steering();
            CheckInput();
        }

        private void OnTimerExpired()
        {
            enabled = false; 
            _rb.velocity = Vector3.zero;
        }

        private void Move()
        {
            _speed = _rb.velocity.magnitude;

            foreach (Wheel wheel in _wheels)
            {
                wheel.WheelCollider.motorTorque = _motorForce * _verticalInput;
                wheel.UpdateWheelMeshPosition();
            }
        }

        private void CheckInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            float movingDirectional = Vector3.Dot(transform.forward, _rb.velocity);
            _brakeInput = (movingDirectional < -0.5f && _verticalInput > 0) || (movingDirectional > 0.5f && _verticalInput < 0) ? Mathf.Abs(_verticalInput) : 0;
        }

        private void Brake()
        {
            foreach (Wheel wheel in _wheels)
                wheel.WheelCollider.brakeTorque = _brakeInput * _brakeForce * (wheel.IsForwardWheel ? 0.7f : 0.3f);
        }

        private void Steering()
        {
            float steeringAngle = _horizontalInput * _steeringCurve.Evaluate(_speed);
            float slipAngle = Vector3.Angle(transform.forward, _rb.velocity - transform.forward);

            if (slipAngle < 120)
                steeringAngle += Vector3.SignedAngle(transform.forward, _rb.velocity, Vector3.up);

            steeringAngle = Mathf.Clamp(steeringAngle, -48, 48);

            foreach (Wheel wheel in _wheels)
            {
                if (wheel.IsForwardWheel)
                    wheel.WheelCollider.steerAngle = steeringAngle;
            }
        }
    }
}