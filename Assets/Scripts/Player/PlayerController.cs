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
    [HideInInspector] public Vector3 direction;
    public State curState;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        mover = GetComponent<Mover>();
        ChangeState(State.Idle);
    }

    private void Update()
    {
        if (curState == State.Idle || curState == State.Run)
        {
            GetMovementDirection();
        }

        AnimCheck();
    }

    private void GetMovementDirection()
    {
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
        if (curState != State.Run)
        {
            anim.SetBool("Running", false);
            direction = Vector3.zero;
        }

        switch (state)
        {
            case State.Idle:

                curState = State.Idle;
                break;

            case State.Run:
                if (direction.magnitude > 0) anim.SetBool("Running", true);

                curState = State.Run;
                break;

            case State.Roll:

                curState = State.Roll;
                break;

            case State.Jump:

                curState = State.Jump;
                break;

            case State.AnimCheck:

                curState = State.AnimCheck;
                break;

            default:

                print("default");
                break;
        }
    }
}
