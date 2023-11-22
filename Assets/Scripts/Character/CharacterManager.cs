using System;
using Art.Script;
using Art.Test.Dissolve;
using Character.Data.Character;
using Character.State;
using Kayak;
using SceneTransition;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class CharacterManager : MonoBehaviour
    {
        public CharacterMultiplayerManager CharacterMultiplayerManager;
        
        #region Properties

        [field: SerializeField] public CharacterStateBase CurrentStateBaseProperty { get; private set; }
        [field: SerializeField] public KayakController KayakControllerProperty { get; private set; }
        [field: SerializeField] public InputManagement InputManagementProperty { get; private set; }
        [field: SerializeField] public Animator PaddleAnimatorProperty { get; private set; }
        [field: SerializeField] public Animator CharacterAnimatorProperty { get; private set; }
        [field: SerializeField] public MonoBehaviour CharacterMonoBehaviour { get; private set; }
        [field: SerializeField] public IKControl IKPlayerControl { get; private set; }

        #endregion

        [Header("Character Data")]
        public CharacterData Data;
        [Range(0, 360)] public float BaseOrientation;
        
        [Header("VFX")]
        public ParticleSystem SplashLeft;
        public ParticleSystem SplashRight;

        [Header("Events")] public UnityEvent StartGame;
        public UnityEvent OnPaddle;

        [ReadOnly] public bool InWaterFlow = false;
        

        private void Awake()
        {
            Cursor.visible = false;
            CharacterMonoBehaviour = this;
        }

        private void Start()
        {
            CharacterNavigationState navigationState = new CharacterNavigationState(CharacterMultiplayerManager);
            CurrentStateBaseProperty = navigationState;
            CurrentStateBaseProperty.Initialize();

            CurrentStateBaseProperty.EnterState(this);

            //rotate kayak
            Transform kayakTransform = KayakControllerProperty.transform;
            kayakTransform.eulerAngles = new Vector3(0, BaseOrientation, 0);

        }
        
        private void Update()
        {
            CurrentStateBaseProperty.UpdateState(this);
            
            //anim
            if (IKPlayerControl.CurrentType != IKType.Paddle || IKPlayerControl.Type == IKType.Paddle)
            {
                return;
            }
            CurrentStateBaseProperty.TimeBeforeSettingPaddleAnimator -= Time.deltaTime;
            if (CurrentStateBaseProperty.TimeBeforeSettingPaddleAnimator <= 0)
            {
                IKPlayerControl.SetPaddle();
            }
        }
        private void FixedUpdate()
        {
            CurrentStateBaseProperty.FixedUpdate(this);
        }
        
        public void SwitchState(CharacterStateBase stateBaseCharacter)
        {
            CurrentStateBaseProperty.ExitState(this);
            CurrentStateBaseProperty = stateBaseCharacter;
            stateBaseCharacter.EnterState(this);
        }
        

        public void SendDebugMessage(string message)
        {
            Debug.Log(message);
        }
    }
}
