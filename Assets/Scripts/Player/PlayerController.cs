using System;
using UnityEngine;

public enum State
{
    Idle, Run, Roll, Jump, Attack, AnimCheck
}

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Mover mover;
    private CharacterController charController;
    // [HideInInspector]
    public Vector3 direction;
    public State curState;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        anim = GetComponent<Animator>();
        mover = GetComponent<Mover>();
        charController = GetComponent<CharacterController>();

        ChangeState(State.Idle);
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        GetMovementDirection();
        AnimCheck();
        if (Input.GetKeyDown(KeyCode.Space)) ChangeState(State.Jump);
        if (Input.GetKeyDown(KeyCode.LeftShift)) ChangeState(State.Roll);
    }

    private void GetMovementDirection()
    {
        if (curState != State.Idle && curState != State.Run) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0) ChangeState(State.Run);
        else ChangeState(State.Idle);
    }

    private void AnimCheck()
    {
        if (curState == State.AnimCheck) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) anim.SetTrigger("Anim1");
        else if (Input.GetKeyDown(KeyCode.Alpha2)) anim.SetTrigger("Anim2");
        else if (Input.GetKeyDown(KeyCode.Alpha3)) anim.SetTrigger("Anim3");
        else return;
        ChangeState(State.AnimCheck);
    }


    public void ChangeState(State state)
    {
        if (state != State.Run)
        {
            anim.SetBool("Running", false);
            direction = Vector3.zero;
            if (curState == state) return;
        }

        switch (state)
        {
            case State.Idle:

                curState = State.Idle;
                break;

            case State.Run:
                anim.SetBool("Running", true);
                mover.Move(direction);

                curState = State.Run;
                break;

            case State.Roll:
                if (curState != State.Idle && curState != State.Run) return;
                anim.SetTrigger("Roll");
                charController.Move(transform.forward * 0.5f);


                curState = State.Roll;
                break;

            case State.Jump:
                if (curState != State.Idle && curState != State.Run) return;
                anim.SetTrigger("Jump");

                curState = State.Jump;
                break;

            case State.AnimCheck:

                curState = State.AnimCheck;
                break;
        }
    }
}
