using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class SpeedParticle : MonoBehaviour
{
    [SerializeField] private CharacterMultiplayerManager _characterMultiplayerManager;

    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] ParticleSystem _particleSystem1;
    [SerializeField] ParticleSystem _particleSystem2;
    [SerializeField] float _emissionMin = 0f;
    [SerializeField] float _emissionMax = 15f;

    float _maxSpeed;

    private void Start()
    {
        _maxSpeed = _characterMultiplayerManager.CharacterManager.KayakControllerProperty.Data.KayakValues.MaximumFrontVelocity;
    }

    void Update()
    {
        
        var vel = _rigidbody.velocity.magnitude;

        var emission = _particleSystem1.emission;
        var emission2 = _particleSystem2.emission;
        emission.rateOverDistance = Mathf.Lerp(_emissionMin, _emissionMax, vel/ _maxSpeed);

    }
}
