using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private CharacterController charController;
    private Animator anim;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVel;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Animate(direction);

        if (direction.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            charController.Move(moveDir.normalized * speed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void Animate(Vector3 direction)
    {
        if (direction.magnitude > 0) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
    }
}