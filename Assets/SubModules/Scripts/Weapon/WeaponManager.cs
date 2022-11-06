using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
  [SerializeField] private bool rangeActive = false;
  [SerializeField] private bool meleeActive = false;
  [SerializeField] private bool rangeEquipped = false;
  [SerializeField] private bool meleeEquipped = false;

  [SerializeField] AnimationHelper animationHelper;

  public bool RangeActive
  {
    get => rangeActive;
    set
    {
      if (!rangeActive && value)
      {
        // unequip current weapon and equip new weapon
        MeleeEquipped = false;
        RangeEquipped = true;
      }
      rangeActive = value;
    }
  }

  public bool MeleeActive
  {
    get => meleeActive;
    set
    {
      if (!meleeActive && value)
      {
        // unequip current weapon and equip new weapon
        RangeEquipped = false;
        MeleeEquipped = true;
      }
      meleeActive = value;
    }
  }

  public bool RangeEquipped
  {
    get => rangeEquipped;
    set
    {
      animationHelper.SetAnimationVariableName("isRangeEquipped");
      animationHelper.SetBool(value);
      rangeEquipped = value;
    }
  }

  public bool MeleeEquipped
  {
    get => meleeEquipped;
    set
    {
      animationHelper.SetAnimationVariableName("isMeleeEquipped");
      animationHelper.SetBool(value);
      meleeEquipped = value;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Equip"))
    {
      if (MeleeEquipped)
      {
        MeleeEquipped = false;
        if (rangeActive)
        {
          RangeEquipped = true;
        }
      }
      else if (rangeEquipped)
      {
        RangeEquipped = false;
      }
      else if (meleeActive)
      {
        MeleeEquipped = true;
      }
    }
  }

}
