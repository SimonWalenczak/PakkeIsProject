using System;
using UnityEngine;

namespace Character
{
    public class InputManagement : MonoBehaviour
    {
        private GameplayInputs _gameplayInputs;

        public GameplayInputs GameplayInputs { get { return _gameplayInputs; } private set { _gameplayInputs = value; } }
        [SerializeField] float DeadzoneJoystick = 0.3f;
        [SerializeField] float DeadzoneJoystickTrigger = 0.3f;
        [field:SerializeField] public InputsEnum Inputs { get; private set; }

        private void Awake()
        {
            _gameplayInputs = new GameplayInputs();
            _gameplayInputs.Enable();
        }

        private void Update()
        {
            GatherInputs();
        }


        private void GatherInputs()
        {
            InputsEnum inputsEnum = Inputs;
            
            inputsEnum.PaddleLeft = _gameplayInputs.Boat.PaddleLeft.ReadValue<float>() > DeadzoneJoystickTrigger;
            inputsEnum.PaddleRight = _gameplayInputs.Boat.PaddleRight.ReadValue<float>() > DeadzoneJoystickTrigger;

            inputsEnum.RotateLeft = _gameplayInputs.Boat.StaticRotateLeft.ReadValue<float>();
            inputsEnum.RotateRight = _gameplayInputs.Boat.StaticRotateRight.ReadValue<float>();
            
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