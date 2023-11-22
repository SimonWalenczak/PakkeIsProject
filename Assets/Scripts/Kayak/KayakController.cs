using Character;
using Character.State;
using Kayak.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Kayak
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class KayakController : MonoBehaviour
    {
        public KayakData Data;
        
        [field:SerializeField, Tooltip("Reference of the kayak rigidbody")] public Rigidbody Rigidbody { get; private set; }
        [ReadOnly, Tooltip("If this value is <= 0, the drag reducing will be activated")] public float DragReducingTimer;
        [ReadOnly, Tooltip("= is the drag reducing method activated ?")] public bool CanReduceDrag = true;
        [SerializeField, Tooltip("The floaters associated to the kayak's rigidbody")] public Floaters FloatersRef;
       
        [Header("VFX"), SerializeField] public ParticleSystem LeftPaddleParticle;
        [SerializeField] public ParticleSystem RightPaddleParticle;
        
        [Header("Events")] 
        public UnityEvent OnKayakCollision;
        public UnityEvent OnKayakSpeedHigh;
        [SerializeField] private float _magnitudeToLaunchEventSpeed;
        [SerializeField] private Vector2 _speedEventRecurrenceRandomBetween;
        
        //privates
        private float _speedEventCountDown;
        private float _particleTimer = -1;
        private CharacterNavigationState.Direction _particleSide;
        
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            ClampVelocity();
            ManageParticlePaddle();
            ManageHighSpeedEvent();
        }

        private void FixedUpdate()
        {
            DragReducing();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log($"collision V.M. :{Math.Round(collision.relativeVelocity.magnitude)} -> {Math.Round(value,2)}");
            OnKayakCollision.Invoke();
        }

        /// <summary>
        /// Clamp the kayak velocity x & z between -maximumFrontVelocity & maximumFrontVelocity
        /// </summary>
        private void ClampVelocity()
        {
            Vector3 velocity = Rigidbody.velocity;
            KayakParameters kayakValues = Data.KayakValues;

            float velocityX = velocity.x;
            
            float maxClamp = kayakValues.MaximumFrontVelocity;
            velocityX = Mathf.Clamp(velocityX, -maxClamp, maxClamp);
            
            float velocityZ = velocity.z;
            velocityZ = Mathf.Clamp(velocityZ, -maxClamp, maxClamp);
            
            Rigidbody.velocity = new Vector3(velocityX, velocity.y, velocityZ);
        }

        /// <summary>
        /// Artificially reduce the kayak drag to let it slide longer on water
        /// </summary>
        private void DragReducing()
        {
            if (DragReducingTimer > 0 || CanReduceDrag == false)
            {
                DragReducingTimer -= Time.deltaTime;
                return;
            }
            
            Vector3 velocity = Rigidbody.velocity;
            float absX = Mathf.Abs(velocity.x);
            float absZ = Mathf.Abs(velocity.z);

            if (absX + absZ > 1)
            {
                Rigidbody.velocity = new Vector3(
                    velocity.x * Data.DragReducingMultiplier * Time.deltaTime, 
                      velocity.y, 
                    velocity.z * Data.DragReducingMultiplier * Time.deltaTime);
            }
        }

        public void PlayPaddleParticle(CharacterNavigationState.Direction side)
        {
            _particleTimer = Data.TimeToPlayParticlesAfterPaddle;
            _particleSide = side;
        }

        private void ManageParticlePaddle()
        {
            if (_particleTimer > 0)
            {
                _particleTimer -= Time.deltaTime;
                if (_particleTimer <= 0)
                {
                    _particleTimer = -1;
                    switch (_particleSide)
                    {
                        case CharacterNavigationState.Direction.Left:
                            if (LeftPaddleParticle != null)
                            {
                                LeftPaddleParticle.Play();
                            }
                            break;
                        case CharacterNavigationState.Direction.Right:
                            if (RightPaddleParticle != null)
                            {
                                RightPaddleParticle.Play();
                            }
                            break;
                    }
                }
            }
        }

        private void ManageHighSpeedEvent()
        {
            _speedEventCountDown -= Time.deltaTime;
            if (_speedEventCountDown > 0 || Rigidbody.velocity.magnitude < _magnitudeToLaunchEventSpeed)
            {
                return;
            }

            OnKayakSpeedHigh.Invoke();
            _speedEventCountDown = UnityEngine.Random.Range(_speedEventRecurrenceRandomBetween.x, _speedEventRecurrenceRandomBetween.y);
        }
    }
}