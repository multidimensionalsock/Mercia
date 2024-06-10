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
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform cameraTranform;

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
        GameObject cameraLook = transform.GetChild(0).gameObject;
        GameObject model = transform.GetChild(1).gameObject;


        while (_movementDirection != Vector3.zero)
        {
            //if (m_movementLock)
            //{
            //    yield return new WaitForFixedUpdate();
            //    continue;
            //}
            Vector3 prepos = new Vector3( transform.position.x, 0f, transform.position.z);
            transform.position += cameraTranform.forward * _movementDirection.z * movementSpeed * Time.fixedDeltaTime;
            transform.position += cameraTranform.right * _movementDirection.x * movementSpeed * Time.fixedDeltaTime;
            Vector3 newpos = new Vector3(transform.position.x, 0f, transform.position.z);
            model.transform.rotation = Quaternion.LookRotation(newpos - prepos, Vector3.up);

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
