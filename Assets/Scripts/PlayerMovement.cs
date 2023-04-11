using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private Vector2 Velocity;
    private Vector2 mouseInput;
    private Vector3 movementInput;
    private bool jumpInput;
    private float xRotation;
    private float yRotation;

    private CharacterController Controller;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f;

    private void Start() {
        Controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        PlayerMove();
        CameraMove();
    }

    private void PlayerMove() {
        Vector3 moveVector = transform.TransformDirection(movementInput);
        if (Controller.isGrounded) {
            Velocity.y = -1f;
            if (jumpInput)
                Velocity.y = JumpForce;
        }
        else
            Velocity.y -= (-2f * Gravity * Time.deltaTime);
        Controller.Move(moveVector * Speed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);
    }

    private void CameraMove() {
        xRotation -= mouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.Rotate(0f, mouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
