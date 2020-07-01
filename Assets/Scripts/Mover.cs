using UnityEngine;

public class Mover : MonoBehaviour
{
    private CharacterController charController;
    private Animator anim;
    public Transform cam;
    public float speed = 6f;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        anim.SetFloat("Speed", direction.magnitude);

        if (direction.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            charController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}