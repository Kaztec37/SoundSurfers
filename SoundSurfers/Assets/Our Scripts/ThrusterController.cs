using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrusterController: MonoBehaviour
{
    public Transform rightHandTransform;
    public Transform leftHandTransform;
    public Rigidbody playerRigidbody;
    public float thrusterAmount = 1.0f;

    [SerializeField] private InputActionReference toggleRightHandAction;
    [SerializeField] private InputActionReference toggleLeftHandAction;

    private bool isRightHandActive;
    private bool isLeftHandActive;
    private float halfThrusterAmount;

    private void Awake()
    {
        toggleRightHandAction.action.started += OnRightHandStarted;
        toggleRightHandAction.action.canceled += OnRightHandCanceled;

        toggleLeftHandAction.action.started += OnLeftHandStarted;
        toggleLeftHandAction.action.canceled += OnLeftHandCanceled;
    }

    private void Update()
    {
        halfThrusterAmount = thrusterAmount / 2;
    }

    private void OnEnable()
    {
        toggleRightHandAction.action.Enable();
        toggleLeftHandAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleRightHandAction.action.Disable();
        toggleLeftHandAction.action.Disable();
    }

    private void OnRightHandStarted(InputAction.CallbackContext context)
    {
        isRightHandActive = true;
    }

    private void OnRightHandCanceled(InputAction.CallbackContext context)
    {
        isRightHandActive = false;
    }

    private void OnLeftHandStarted(InputAction.CallbackContext context)
    {
        isLeftHandActive = true;
    }

    private void OnLeftHandCanceled(InputAction.CallbackContext context)
    {
        isLeftHandActive = false;
    }

    private void FixedUpdate()
    {
        Vector3 rightHandThrustDirection = rightHandTransform.right;
        Vector3 leftHandThrustDirection = -leftHandTransform.right;

        Vector3 thrustDirection = Vector3.zero;

        if (isRightHandActive && isLeftHandActive)
        {
            thrustDirection = (rightHandThrustDirection + leftHandThrustDirection).normalized;
            playerRigidbody.AddForce(thrustDirection * thrusterAmount, ForceMode.Acceleration);
        }
        else if (isRightHandActive)
        {
            thrustDirection = rightHandThrustDirection;
            playerRigidbody.AddForce(thrustDirection * halfThrusterAmount, ForceMode.Acceleration);
        }
        else if (isLeftHandActive)
        {
            thrustDirection = leftHandThrustDirection;
            playerRigidbody.AddForce(thrustDirection * halfThrusterAmount, ForceMode.Acceleration);
        }
    }
}
