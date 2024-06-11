using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        AreaData.AreaEntered += SetCameraArea;
        animator = GetComponent<Animator>();
    }

    void SetCameraArea(Area area)
    {
        int areaCode = (int)area;
        animator.SetInteger("AreaCode", areaCode);
    }
}
