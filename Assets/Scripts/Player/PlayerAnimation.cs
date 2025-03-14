using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public bool IsCarry { get { return animator.GetLayerWeight(1) == 1f &&
                                       animator.GetBool(AnimationKey.CARRY); } }

    public void Move(float value)
    {
        animator.SetFloat("Speed", value);
    }

    public void Carry(bool isOn)
    {
        float weight = isOn ? 1f : 0f;
        animator.SetLayerWeight(1, weight);
        animator.SetBool(AnimationKey.CARRY, isOn);
    }
}
