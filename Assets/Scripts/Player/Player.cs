using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private float maxSpeed = 6f;

    private DynamicJoystick joyStick;


    public void Init(DynamicJoystick joyStick)
    {
        this.joyStick = joyStick;
    }

    public void OnUpdate()
    {
        MoveUpdate();
    }

    public void MoveUpdate()
    {
        float joystickSize = new Vector2(joyStick.Horizontal, joyStick.Vertical).magnitude;
        float moveSpeed = Mathf.Lerp(0f, maxSpeed, joystickSize);

        Vector3 moveDirection = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveDirection);

            animator.SetFloat("Speed", joystickSize);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

}
