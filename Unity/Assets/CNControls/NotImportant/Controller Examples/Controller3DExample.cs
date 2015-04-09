using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Controller3DExample : MonoBehaviour
{
    public const float ROTATE_SPEED = 15f;

    public float movementSpeed = 5f;

    public CNAbstractController MovementJoystick;

    public GameObject _player;

    private CharacterController _characterController;
    private Transform _mainCameraTransform;
    private Transform _transformCache;
    private Transform _playerTransform;
    
    private Spi _spi;

    void Start()
    {
        // You can also move the character with an event system
        // You MUST CHOOSE one method and use ONLY ONE a frame
        // If you wan't the event based control, uncomment this line
        // MovementJoystick.JoystickMovedEvent += MoveWithEvent;

        _characterController = _player.GetComponent<CharacterController>();
        _mainCameraTransform = Camera.main.GetComponent<Transform>();
        _transformCache = _player.GetComponent<Transform>();
        _playerTransform = _player.transform;
        
        _spi = _characterController.GetComponent<Spi>();
    }

    
    // Update is called once per frame
    void Update()
    {
        var movement = new Vector3(
            MovementJoystick.GetAxis("Horizontal"),
            0f,
            MovementJoystick.GetAxis("Vertical"));

        CommonMovementMethod(movement);
    }
	
	/* Kev' : Je ne pense pas que cette fonction ai un intéret */
	/*
    private void MoveWithEvent(Vector3 inputMovement)
    {
        var movement = new Vector3(
            inputMovement.x,
            0f,
            inputMovement.y);
	
	
        CommonMovementMethod(movement);
    }
    */

    private void CommonMovementMethod(Vector3 movement)
    {
        movement = _mainCameraTransform.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();
        
		_spi.Move (movement);
    }
	/*
    public void FaceDirection(Vector3 direction)
    {
        StopCoroutine("RotateCoroutine");
        StartCoroutine("RotateCoroutine", direction);
    }

    IEnumerator RotateCoroutine(Vector3 direction)
    {
        if (direction == Vector3.zero) yield break;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        do
        {
            _playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, lookRotation, Time.deltaTime * ROTATE_SPEED);
            yield return null;
        }
        while ((direction - _playerTransform.forward).sqrMagnitude > 0.2f);
    }
    */

}
