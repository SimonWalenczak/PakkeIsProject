using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WaterAndFloating;

namespace IcebergFallingGPE
{
    public class IcebergFalling : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        [SerializeField, ReadOnly] public bool Fall;
        [SerializeField, ReadOnly] public bool HasFallen;

        [Header("Parameters"), SerializeField] private float _fallDuration;
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private Vector3 _endRotation;
        [SerializeField] private AnimationCurve _fallSpeedCurve;
        [SerializeField] private float _timeToHitWater;

        [Header("VFX"), SerializeField] private ParticleSystem _particleWhenHitWater;

        private float _timer;
        private Vector3 _beginPosition, _targetPosition;

        [Header("Events")] public UnityEvent OnWaterCollision;
        public UnityEvent OnFallStarted;

        private void Start()
        {
            _targetPosition = transform.position + _endPosition;
            _beginPosition = transform.position;
        }

        private void Update()
        {
            if (Fall)
            {
                HandleFall();
            }

            if (Fall && _timeToHitWater > 0)
            {

            }
        }

        /// <summary>
        /// manage and check the moment to launch the wave
        /// </summary>


        /// <summary>
        /// Start the fall
        /// </summary>
        public void SetFall()
        {
            if (Fall == false && _timer < 1)
            {
                Fall = true;
                HasFallen = true;
                _timer = 0;
                OnFallStarted.Invoke();
            }
        }

        /// <summary>
        /// Manage the iceberg fall
        /// </summary>
        private void HandleFall()
        {
            _timer += Time.deltaTime;
            float fallProgress = _timer / _fallDuration;
            Vector3 targetPosition =
                Vector3.Lerp(_beginPosition, _targetPosition, _fallSpeedCurve.Evaluate(fallProgress));
            Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_endRotation),
                _fallSpeedCurve.Evaluate(fallProgress));
            transform.position = targetPosition;
            transform.rotation = targetRotation;

            if (fallProgress >= 1)
            {
                Fall = false;
            }
        }
    }
}