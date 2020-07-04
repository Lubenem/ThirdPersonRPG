using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private CharacterController charController;
    private Animator anim;
    public Transform cam;
    public float speed = 6f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVel;
    private PlayerController playerController;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 direction = playerController.direction;
        if (direction.magnitude <= 0) return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        charController.Move(moveDir.normalized * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}