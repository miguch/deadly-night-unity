using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
  [SerializeField] private bool active = false;
  [SerializeField] bool equipped = false;

  [SerializeField] AnimationHelper animationHelper;


  public bool Active
  {
    get => active;
    set
    {
      if (!active && value)
      {
        animationHelper.SetAnimationVariableName("GetWeapon");
        animationHelper.SetTrigger();
        equipped = true;
        animationHelper.SetAnimationVariableName("isEquipped");
        animationHelper.SetBool(equipped);
      }
      active = value;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Equip"))
    {
      equipped = !equipped;
      animationHelper.SetAnimationVariableName("isEquipped");
      animationHelper.SetBool(equipped);
    }
  }
}
