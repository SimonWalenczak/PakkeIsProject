using Art.Script;
using DG.Tweening;
using Kayak.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Character.State
{
    public abstract class CharacterStateBase 
    {
        public CharacterMultiplayerManager CharacterMultiplayerManager;
        
        protected CharacterManager CharacterManagerRef;
        public MonoBehaviour MonoBehaviourRef;
        
        public bool CanBeMoved = true;
        public bool CanCharacterMove = true;
        public bool CanCharacterMakeActions = true;
        public bool CanOpenMenus = true;
        public bool CanCharacterOpenWeapons = true;
        public bool IsDead;

        public float RotationStaticForceY = 0f;
        public float RotationPaddleForceY = 0f;

        [ReadOnly] public Floaters Floaters;

        //events
        public UnityEvent OnPaddleLeft = new UnityEvent();
        public UnityEvent OnPaddleRight = new UnityEvent();
        
        protected Transform PlayerPosition;
        
        //anim
        public float TimeBeforeSettingPaddleAnimator;
        
        protected CharacterStateBase(CharacterMultiplayerManager character)
        {
            CharacterMultiplayerManager = character;
            
            if (CharacterMultiplayerManager != null)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            CharacterManagerRef = CharacterMultiplayerManager.CharacterManager;
            MonoBehaviourRef =CharacterMultiplayerManager.CharacterManager.CharacterMonoBehaviour;

            Floaters = CharacterManagerRef.KayakControllerProperty.FloatersRef;
        }

        public abstract void EnterState(CharacterManager character);

        public abstract void UpdateState(CharacterManager character);

        public abstract void FixedUpdate(CharacterManager character);

        public abstract void SwitchState(CharacterManager character);
        
        public abstract void ExitState(CharacterManager character);
        

        /// <summary>
        /// Move the velocity toward the player's facing direction
        /// </summary>
        protected void VelocityToward()
        {
            Vector3 oldVelocity = CharacterManagerRef.KayakControllerProperty.Rigidbody.velocity;
            float oldVelocityMagnitude = new Vector2(oldVelocity.x, oldVelocity.z).magnitude;
            Vector3 forward = CharacterManagerRef.KayakControllerProperty.transform.forward;
            
            Vector2 newVelocity = oldVelocityMagnitude * new Vector2(forward.x,forward.z).normalized;

            CharacterManagerRef.KayakControllerProperty.Rigidbody.velocity = new Vector3(newVelocity.x, oldVelocity.y, newVelocity.y);
        }
        
        public void LaunchNavigationState()
        {
            CharacterManager character = CharacterMultiplayerManager.CharacterManager;

            CharacterNavigationState characterNavigationState = new CharacterNavigationState(CharacterMultiplayerManager);
            character.SwitchState(characterNavigationState);
            
            CharacterManagerRef.IKPlayerControl.CurrentType = IKType.Paddle;
        }

        #region Wave/Floaters and Balance management

        /// <summary>
        /// Check the floater's level to see if the boat is unbalanced
        /// </summary>
        protected void CheckRigidbodyFloatersBalance()
        {
            float frontLeftY = Floaters.FrontLeft.transform.position.y;
            float frontRightY = Floaters.FrontRight.transform.position.y;
            float backLeftY = Floaters.BackLeft.transform.position.y;
            float backRightY = Floaters.BackRight.transform.position.y;

            float frontLevel = (frontLeftY + frontRightY) / 2;
            float backLevel = (backLeftY + backRightY) / 2;
            float leftLevel = (frontLeftY + backLeftY) / 2;
            float rightLevel = (frontRightY + backRightY) / 2;

            float multiplier = CharacterManagerRef.Data.FloatersLevelDifferenceToBalanceMultiplier;
            float frontBackDifference = Mathf.Abs(frontLevel - backLevel) * multiplier;
            float leftRightDifference = Mathf.Abs(leftLevel - rightLevel) * multiplier;
        }

        #endregion
    }
}