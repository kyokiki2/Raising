using UnityEngine;
using UnityEngine.AI;

public class Player : CharacterBase
{   
    [SerializeField]
    private DynamicJoystick joyStick;

    [SerializeField]
    private float maxSpeed = 6f;

    public override void Init()
    {
        base.Init();
    }   

    public override void OnUpdate()
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

            charAnimation.Move(joystickSize);
        }
        else
        {
            charAnimation.Move(0);
        }
    }
}
