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
    public Joystick joystick;

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 joystickDir = new Vector3(joystick.Horizontal, joystick.Vertical, 0);

        if (curState != State.Roll && curState != State.Jump)
        {
            direction = new Vector3(horizontal, 0, vertical).normalized + joystickDir;
        }

        anim.SetFloat("Magnitude", direction.magnitude);

        if (curState != State.Idle && curState != State.Run) return;

        if (direction.magnitude > 0) ChangeState(State.Run);
        else ChangeState(State.Idle);
    }

    private void AnimCheck()
    {
        if (curState != State.Idle && curState != State.Run) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) anim.SetTrigger("Anim1");
        else if (Input.GetKeyDown(KeyCode.Alpha2)) anim.SetTrigger("Anim2");
        else if (Input.GetKeyDown(KeyCode.Alpha3)) anim.SetTrigger("Anim3");
        else return;
        ChangeState(State.AnimCheck);
    }


    public void ChangeState(State state)
    {
        if (curState == state) return;

        if (state != State.Run)
        {
            anim.SetBool("Running", false);
        }

        switch (state)
        {
            case State.Idle:

                curState = State.Idle;
                break;

            case State.Run:
                anim.SetBool("Running", true);
                mover.speed = mover.runSpeed;

                curState = State.Run;
                break;

            case State.Roll:
                if (curState != State.Idle && curState != State.Run) return;
                anim.SetTrigger("Roll");
                mover.speed = mover.rollSpeed;

                curState = State.Roll;
                break;

            case State.Jump:
                if (curState != State.Idle && curState != State.Run) return;
                anim.SetTrigger("Jump");
                mover.speed = mover.jumpSpeed;

                curState = State.Jump;
                break;

            case State.AnimCheck:

                curState = State.AnimCheck;
                break;
        }
    }
}
