using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Characters.PlayableCharacters
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private Vector2 _inputVector;
        private Vector3 _movementVector;
        
        private float _movementSpeed;
        [SerializeField] private float _gravity = -2f;

        [HideInInspector]
        public float MovementSpeed;
    
        private void Start()
        {
            _movementSpeed = MovementSpeed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _characterController.Move(_movementVector * _movementSpeed * Time.deltaTime);
        }

        void OnMove(InputValue value)
        {
            _inputVector = value.Get<Vector2>();
            CalculateMovementVector();
        }

        void CalculateMovementVector()
        {
            _movementVector = new Vector3(_inputVector.x, _gravity, _inputVector.y);
        }
    
    }
}
