using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public event Action OnPlayerJumped;

   [Header("References")]
   [SerializeField] private Transform _orientationTransform;

   [Header("Movement Settings")]
   [SerializeField] private float _movementSpeed;
   [SerializeField] private KeyCode _movementKey;

   [Header("Jump Settings")]
   [SerializeField] private KeyCode _jumpKey;
   [SerializeField] private float _jumpForce;
   [SerializeField] private float _jumpCooldown;
   [SerializeField] private float _airMultiplier;
   [SerializeField] private float _airDrag;
   [SerializeField] private bool _canJump;

   [Header("Ground Check Settings")]
   [SerializeField] private float _playerHeight;
   [SerializeField] private LayerMask _groundLayer;
   [SerializeField] private float _groundDrag;

   [Header("Sliding Settings")]
   [SerializeField] private KeyCode _slideKey;
   [SerializeField] private float _sildeMultiplier;
   [SerializeField] private float _slideDrag;

   private StateController _stateController;


   private Rigidbody _playerRigidbody;

   private float _startingMovementSpeed, _startingJumpForce;

   private float _horizontaşInput, _verticalInput;

   private Vector3 _movemnetDirection;
   private bool _isSliding;

   private void Awake()
   {
      _stateController = GetComponent<StateController>();
      _playerRigidbody = GetComponent<Rigidbody>();
      _playerRigidbody.freezeRotation = true;

      _startingJumpForce = _jumpForce;
      _startingMovementSpeed = _movementSpeed;
   }

   private void Update()
   {
      SetInputs();
      SetStates();
      SetPlayerDrag();
      LimitPlayerSpeed();
   }

   private void FixedUpdate()
   {
      SetPlayerMovement();
   }

   private void SetInputs()
   {
      _horizontaşInput = Input.GetAxisRaw("Horizontal");
      _verticalInput = Input.GetAxisRaw("Vertical");

      if (Input.GetKeyDown(_slideKey))
      {
         _isSliding = true;
      }

      else if (Input.GetKeyDown(_movementKey))
      {
         _isSliding = false;
      }

      else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
      {
         _canJump = false;
         SetPlayerJumping();
         Invoke(nameof(ResetJumping), _jumpCooldown);
      }
   }

   private void SetStates()
   {
      var movementDirection = GetMovementDirection();
      var isGrounded = IsGrounded();
      var isSliding = IsSliding();
      var currentState = _stateController.GetCurrentState();

      var newState = currentState switch
      {
         _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
         _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
         _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
         _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
         _ when !_canJump && !isGrounded => PlayerState.Jump,
         _ => currentState
      };

      if (newState != currentState)
      {
         _stateController.ChangeState(newState);
      }
   }

   private void SetPlayerMovement()
   {
      _movemnetDirection = _orientationTransform.forward * _verticalInput
          + _orientationTransform.right * _horizontaşInput;

      float forceMultiplier = _stateController.GetCurrentState() switch
      {
         PlayerState.Move => 1f,
         PlayerState.Slide => _sildeMultiplier,
         PlayerState.Jump => _airMultiplier,
         _ => 1f
      };

      _playerRigidbody.AddForce(_movemnetDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
   }

   private void SetPlayerDrag()
   {
      _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
      {
         PlayerState.Move => _groundDrag,
         PlayerState.Slide => _slideDrag,
         PlayerState.Jump => _airDrag,
         _ => _playerRigidbody.linearDamping
      };
   }

   private void LimitPlayerSpeed()
   {
      Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

      if (flatVelocity.magnitude > _movementSpeed)
      {
         Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
         _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
      }
   }

   private void SetPlayerJumping()
   {
      OnPlayerJumped?.Invoke();
      _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
      _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
   }

   private void ResetJumping()
   {
      _canJump = true;
   }

   #region Helper Functions

   private bool IsGrounded()
   {
      return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
   }

   private Vector3 GetMovementDirection()
   {
      return _movemnetDirection.normalized;
   }

   private bool IsSliding()
   {
      return _isSliding;
   }

   public void SetMovementSpeed(float speed, float duraction)
   {
      _movementSpeed += speed;
      Invoke(nameof(ResetMovementSpeed), duraction);
   }

   private void ResetMovementSpeed()
   {
      _movementSpeed = _startingMovementSpeed;
   }

   public void SetJumpForce(float force, float duraction)
   {
      _jumpForce += force;
      Invoke(nameof(ResetJumpForce), duraction);
   }

   private void ResetJumpForce()
   {
      _jumpForce = _startingJumpForce;
   }

   #endregion
}
