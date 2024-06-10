using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationLogic : MonoBehaviour
{
    PlayerInput _input;
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponentInParent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _input.currentActionMap.FindAction("Move").performed += StartMove;
        _input.currentActionMap.FindAction("Move").canceled += EndMove;
    }

    void StartMove (InputAction.CallbackContext context)
    {
        _animator.SetBool("Moving", true);
    }

    void EndMove(InputAction.CallbackContext context)
    {
        _animator.SetBool("Moving", false);
    }

}
