using NUnit.Framework.Constraints;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private KeyCode _movementKey;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _canJump;

    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _sildeMultiplier;
    [SerializeField] private float _slideDrag;


    private Rigidbody _playerRigidbody;
    
    private float _horizontaşInput, _verticalInput;

    private Vector3 _movemnetDirection;
    private bool _isSliding;

    private void Awake()
   {
      _playerRigidbody = GetComponent<Rigidbody>();
      _playerRigidbody.freezeRotation = true;
   }

   private void Update()
   {
      SetInputs();
      SetPlayerDrag();
      LimitPlayyerSpeed();
   }

   private void FixedUpdate()
   {
      SetPlayerMovement();
   }

   private void SetInputs()
   {
      _horizontaşInput = Input.GetAxisRaw("Horizontal");
      _verticalInput = Input.GetAxisRaw("Vertical");

      if(Input.GetKeyDown(_slideKey))
      {
          _isSliding = true;
      }

      else if (Input.GetKeyDown(_movementKey))
      {
          _isSliding = false;
      }

      else if(Input.GetKey(_jumpKey) && _canJump && IsGrounded())
      {
          _canJump = false;
          SetPlayerJumping();
          Invoke(nameof(ResetJumping), _jumpCooldown);
      }
   }

   private void SetPlayerMovement()
   {
      _movemnetDirection = _orientationTransform.forward * _verticalInput
          + _orientationTransform.right * _horizontaşInput;

      if(_isSliding)
      {
        _playerRigidbody.AddForce(_movemnetDirection.normalized * _movementSpeed * _sildeMultiplier, ForceMode.Force);
      }
      else
      {
        _playerRigidbody.AddForce(_movemnetDirection.normalized * _movementSpeed, ForceMode.Force);
      }
   }

   private void SetPlayerDrag()
   {
      if(_isSliding)
    {
       _playerRigidbody.linearDamping = _slideDrag;
    }
      else
      {
        _playerRigidbody.linearDamping = _groundDrag;
      }
   }

   private void LimitPlayyerSpeed()
   {
      Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

      if(flatVelocity.magnitude > _movementSpeed)
      {
        Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
        _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
      }
   }

   private void SetPlayerJumping()
   {
      _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
      _playerRigidbody.AddForce (transform.up * _jumpForce, ForceMode.Impulse);
   }

   private void ResetJumping()
   {
    _canJump = true;
   }

   private bool IsGrounded()
   {
       return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
   }
}
