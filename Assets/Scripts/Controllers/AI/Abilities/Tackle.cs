using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;
using System.Collections;
using NXT;
/// <summary>
/// 
/// 
/// Algorithm
/// 
/// 1 Start Coroutine 
/// 2 Switch to AI state and move forward
/// 3 Apply camera blur
/// 4 Wait for user click, then raycast to see if collision occured
/// </summary>

public class Tackle : MoveBehavior
{

    private bool isWaitingSecondaryAction = false;
    private MotorController motorController;
    private CameraController m_camera;
    private bool isCheckingCollision = false;

    private MotorState lastState;
    public override void Start()
    {

        base.Start();
        m_camera = Camera.main.GetComponent<CameraController>();
        motorController = GetComponent<MotorController>();
        lastState = motorController.motorState;
    }

    public override void UseMove()
    {
        if (isWaitingSecondaryAction)
        {
            isWaitingSecondaryAction = false;
            TriggerSecondaryAction();
        }
        else
        {
            Debug.Log("Tackle");
            StartCoroutine(QuickAttackCo());
        }
        
      
     
        
    }
    
    protected override void StopMove()
    {
        StopCoroutine(QuickAttackCo());
        
        motorController.SetState(lastState);
    }

    void initializeMove()
    {

    }
    void Update()
    {

        if (isCheckingCollision && animController.GetCurrentAnimatorStateInfo(0).IsName("Quick Attack") && animController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f) 
        {
            //TODO Physics raycast
            Debug.Log("QUICK ATKKKKKKKKKKKKK");
            isCheckingCollision = false;
        }
    }
    


    void onColliderHit(Collider collider)
    {
        //TODO: CHECK IF VALID LAYERS
        //isWaitingSecondaryAction = false;
        //StopCoroutine(QuickAttackCo());
        //TriggerSecondaryAction();
    }
    void TriggerSecondaryAction()
    {
        Debug.Log("TriggerSecondary");
       
        motorController.RestoreBaseSpeed();
        if(lastState == MotorState.Input)
        m_camera.DeactivateBlur();

        GetComponent<Animator>().CrossFade(Animator.StringToHash("Quick Attack"), 1f, 0, 0);
        isCheckingCollision = true;
       
        motorController.SetState(lastState);
    }
    IEnumerator QuickAttackCo()
    {
        isWaitingSecondaryAction = true;
        if (motorController.motorState == MotorState.Input)
            m_camera.ActivateBlur();
       // lastState = motorController.motorState;
        motorController.SetState(MotorState.None);
        GetComponent<Animator>().CrossFade(Animator.StringToHash("Quick Attack Run"), 0.1f, 0, 0);
        
//        float factor = 1.1f;
       

        while (isWaitingSecondaryAction)
        {
            motorController.IncreaseSpeedByFactor(1.01f,20f);
            motorController.FaceCamera();
            motorController.Move(Vector3.forward);
            yield return null;
        }
     
        yield return null;
    }
   


}