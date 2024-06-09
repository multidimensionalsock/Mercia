using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    PlayerInput _input;
    Vector3 _movementDirection;
    Coroutine _movementCoroutine;
    bool grounded = true;
    [SerializeField] float jumpForce;
    [SerializeField] float movementSpeed;

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
        _input.currentActionMap.FindAction("Jump").performed += Jump;
    }

    void StartMove(InputAction.CallbackContext context)
    {
        Vector2 temp = context.ReadValue<Vector2>();
        _movementDirection = new Vector3(temp.x, 0f, temp.y);
        if (_movementCoroutine != null) return;
        _movementCoroutine = StartCoroutine(Move());
        
    }

    void EndMove(InputAction.CallbackContext context) 
    {
        _movementDirection = Vector3.zero;
        
        StopCoroutine(_movementCoroutine);
        _movementCoroutine = null;
    }

    IEnumerator Move()
    {
        while (_movementDirection != Vector3.zero)
        {
            transform.position += _movementDirection * movementSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (!grounded) return;
        _rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
