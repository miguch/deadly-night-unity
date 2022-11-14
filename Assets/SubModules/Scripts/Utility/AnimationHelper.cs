using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    [SerializeField] Animator animator;

    private string varName;

    public void SetAnimationVariableName(string name)
    {
        varName = name;
    }

    public void SetBool(bool val)
    {
        animator.SetBool(varName, val);
    }

    public void SetInt(int val)
    {
        animator.SetInteger(varName, val);
    }

    public void SetFloat(float val)
    {
        animator.SetFloat(varName, val);
    }

    public void SetTrigger()
    {
        animator.SetTrigger(varName);
    }
}
