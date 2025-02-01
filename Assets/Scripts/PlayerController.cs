using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;
    
    private Vector2 moveInput;
    
    public Rig bodyRig;
    public Rig aimRig;
    
    private Animator animator;
    
    public float blendTreeSpeed = 0.1f;
    public float rotationSpeed = 10f;
    
    public Transform cameraTransform;
    
    private bool isAiming;

    public float tpsSpeed = 2f;
    public float rigTransitionSpeed = 5f;
    public float fovTransitionSpeed = 5f;

    private float targetRig;
    private float targetFov = 40f;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        
    }

    private void Update()
    {
        float threshold = 0.01f;
        float xInput = Mathf.Abs(moveInput.x) > threshold ? moveInput.x : 0f;
        float yInput = Mathf.Abs(moveInput.y) > threshold ? moveInput.y : 0f;

        animator.SetFloat("X", xInput, blendTreeSpeed, Time.deltaTime);
        animator.SetFloat("Y", yInput, blendTreeSpeed, Time.deltaTime);

        float currentSpeed = animator.GetFloat("Magnitude");
        float targetSpeed = moveInput.magnitude > 0 ? moveInput.magnitude : 0;
        float smoothedSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);
        animator.SetFloat("Magnitude", smoothedSpeed);
        
        if (aimRig != null && bodyRig != null)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, targetRig, Time.deltaTime * rigTransitionSpeed);
            bodyRig.weight = Mathf.Lerp(bodyRig.weight, targetRig, Time.deltaTime * rigTransitionSpeed);
        }
        
        if (cinemachineCam != null)
        {
            LensSettings lensSettings = cinemachineCam.Lens;
            lensSettings.FieldOfView = Mathf.Lerp(lensSettings.FieldOfView, targetFov, Time.deltaTime * fovTransitionSpeed);
            cinemachineCam.Lens = lensSettings;
        }

        if (isAiming)
        {
            Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);

            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;
            transform.position += moveDirection * tpsSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            
        }
        else
        {
            if (moveInput.sqrMagnitude < 0.01f) return;
            Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAim(InputValue value)
    {
        isAiming = value.isPressed;
        animator.SetBool("IsAiming", isAiming);

        targetRig = isAiming ? 1f : 0f;
        targetFov = isAiming ? 20f : 40f;
    }
}
