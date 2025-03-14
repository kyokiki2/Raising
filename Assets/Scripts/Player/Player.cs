using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private PlayerAnimation playerAnimation;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private float maxSpeed = 6f;

    [SerializeField]
    private Transform foodParent;

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

            playerAnimation.Move(joystickSize);
        }
        else
        {
            playerAnimation.Move(0);
        }
    }

    public void SetCarry(Chicken chicken)
    {
        if (playerAnimation.IsCarry == false)
            playerAnimation.Carry(true);

        chicken.transform.SetParent(foodParent);
    }

}
