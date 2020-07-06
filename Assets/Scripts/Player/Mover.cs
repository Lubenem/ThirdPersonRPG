using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private CharacterController charController;
    private PlayerController playerController;
    private Animator anim;
    public Transform cam;
    public float speed;
    public float runSpeed = 6f;
    public float rollSpeed = 10f;
    public float jumpSpeed = 10f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVel;

    private void Start()
    {
        speed = runSpeed;
        charController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    public void Update()
    {
        State curState = playerController.curState;

        if (curState == State.Run || curState == State.Roll || curState == State.Jump)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 direction = playerController.direction;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        charController.Move(moveDir.normalized * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}