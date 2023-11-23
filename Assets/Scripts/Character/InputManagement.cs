using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class InputManagement : MonoBehaviour
    {
        private GameplayInputs _gameplayInputs;

        private PlayerConfiguration _playerConfig;
        
        public GameplayInputs GameplayInputs { get { return _gameplayInputs; } private set { _gameplayInputs = value; } }
        [SerializeField] float DeadzoneJoystick = 0.3f;
        [SerializeField] float DeadzoneJoystickTrigger = 0.3f;
        [field:SerializeField] public InputsEnum Inputs { get; private set; }

        private void Awake()
        {
            _gameplayInputs = new GameplayInputs();
            _gameplayInputs.Enable();
            
        }

        public void InitializePlayer(PlayerConfiguration pc)
        {
            _playerConfig = pc;

            _playerConfig.Input.onActionTriggered += GatherInputs;
        }
        
        private void GatherInputs(InputAction.CallbackContext context)
        {
            InputsEnum inputsEnum = Inputs;
            
            //Paddle
            if (context.action.name == _gameplayInputs.Boat.PaddleLeft.name)
                inputsEnum.PaddleLeft = context.ReadValue<float>() > DeadzoneJoystickTrigger;
                
            if (context.action.name == _gameplayInputs.Boat.PaddleRight.name)
                inputsEnum.PaddleRight = context.ReadValue<float>() > DeadzoneJoystickTrigger;
            
            //Rotate
            if (context.action.name == _gameplayInputs.Boat.StaticRotateLeft.name)
                inputsEnum.RotateLeft = context.ReadValue<float>();
            
            if (context.action.name == _gameplayInputs.Boat.StaticRotateRight.name)
                inputsEnum.RotateRight = context.ReadValue<float>();

            inputsEnum.Deadzone = DeadzoneJoystick;

            Inputs = inputsEnum;
        }
    }

    [Serializable]
    public struct InputsEnum
    {
        public bool PaddleLeft;
        public bool PaddleRight;
        
        public float RotateLeft;
        public float RotateRight;
        
        public float Deadzone;
    }
}