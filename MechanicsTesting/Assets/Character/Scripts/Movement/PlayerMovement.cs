using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    PlayerInput _input;
    Vector2 _movementDirection;
    Coroutine _movementCoroutine;

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled -= EndMove;
        _input.currentActionMap.FindAction("Jump").performed += Jump;
    }

    void StartMove(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
        if (_movementCoroutine != null)
        {
            _movementCoroutine = StartCoroutine(Move());
        }
    }

    void EndMove(InputAction.CallbackContext context) 
    {
        _movementDirection = context.ReadValue<Vector2>();
        StopCoroutine(_movementCoroutine);
    }

    IEnumerator Move()
    {
        while (_movementDirection != Vector2.zero)
        {
            _rigidbody.AddForce(_movementDirection);
            yield return new WaitForFixedUpdate();
        }
    }

    void Jump(InputAction.CallbackContext context)
    {

    }

}
