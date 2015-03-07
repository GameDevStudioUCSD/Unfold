using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Controller3DLocal : MonoBehaviour
{
    public const float ROTATE_SPEED = 2.5f;
    public float movementSpeed = 20f;
    public bool debug_On;

    public CNAbstractController MovementJoystick;

    public CharacterController _characterController;
    
    private Transform _mainCameraTransform;
    private Transform _transformCache;
    private Transform _playerTransform;
    
    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    void Start()
    {
        // You can also move the character with an event system
        // You MUST CHOOSE one method and use ONLY ONE a frame
        // If you wan't the event based control, uncomment this line
        // MovementJoystick.JoystickMovedEvent += MoveWithEvent;

        _characterController = GetComponent<CharacterController>();
        _mainCameraTransform = Camera.main.GetComponent<Transform>();
        _transformCache = GetComponent<Transform>();
        _playerTransform = _transformCache;
    }

    
    // Update is called once per frame
    void Update()
    {
        	if (debug_On)
        		Debug.Log ("Controller is Mine");
        	var movement = new Vector3(
            MovementJoystick.GetAxis("Horizontal"),
            0f,
            MovementJoystick.GetAxis("Vertical"));
        	CommonMovementMethod(movement);
    }
    
    private void SyncedMovement()
    {
    	syncTime += Time.deltaTime;
    	GetComponent<Rigidbody>().position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime/syncDelay);
    }

    private void MoveWithEvent(Vector3 inputMovement)
    {
        var movement = new Vector3(
            inputMovement.x,
            0f,
            inputMovement.y);

        CommonMovementMethod(movement);
    }

    private void CommonMovementMethod(Vector3 movement)
    {
        movement = _mainCameraTransform.TransformDirection(movement);
        movement.y = 0f;
        //movement.Normalize(); Allow movement sensitivity

        FaceDirection(movement);
        _characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

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
    
    /*void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
    	Vector3 syncPosition = Vector3.zero;
    	if (stream.isWriting)
    	{
    		syncPosition = rigidbody.position;
    		stream.Serialize(ref syncPosition);
    	}
    	else
    	{
    		stream.Serialize (ref syncPosition);
    		
    		syncTime = 0f;
    		syncDelay = Time.time - lastSynchronizationTime;
    		lastSynchronizationTime = Time.time;
    		
    		syncStartPosition = rigidbody.position;
    		syncEndPosition = syncPosition;
    	}
    }*/

}
