using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ObjectModel 
{
    [SerializeField] Rigidbody PlayerRb;


    [Header("ANIMATION")]
    [SerializeField] Animator PlayerAC;
    [SerializeField] AnimationNames CurrentAnimation;



    public void ChangeAnimation(AnimationNames animationName)
    {
        if (!CheckAnimation(animationName))
            return;
        PlayerAC.SetBool(CurrentAnimation.ToString(), false);
        PlayerAC.SetBool(animationName.ToString(), true);

        CurrentAnimation = animationName;
    }

    public bool CheckAnimation(AnimationNames animationName)
    {
        if (CurrentAnimation == animationName)
            return false;
        return true;
    }


    public void Die(bool isOpen)
    {
        if (isOpen)
        {
            PlayerRb.isKinematic = false;
        }
        else
        {
            PlayerRb.isKinematic = true;
        }
    }

}
