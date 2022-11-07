using System.Collections;
using System.Collections.Generic;
using com.ImmersiveMedia.Damage;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
  [SerializeField] private bool rangeActive = false;
  [SerializeField] private bool meleeActive = false;
  [SerializeField] private bool rangeEquipped = false;
  [SerializeField] private bool meleeEquipped = false;

  [SerializeField] public Damager rangeDamager;
  [SerializeField] public Damager meleeDamager;

  [SerializeField] public UnityEvent onMeleeEquip;
  [SerializeField] public UnityEvent onRangeEquip;

  [SerializeField] public UnityEvent onMeleeAttack;
  [SerializeField] public UnityEvent onRangeAttack;

  [SerializeField] Animator animator;

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
      if (!rangeEquipped && value)
      {
        onRangeEquip?.Invoke();
      }
      animator.SetBool("isRangeEquipped", value);
      rangeEquipped = value;
    }
  }

  public bool MeleeEquipped
  {
    get => meleeEquipped;
    set
    {
      if (!meleeEquipped && value)
      {
        onMeleeEquip?.Invoke();
      }
      animator.SetBool("isMeleeEquipped", value);
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
    if (Input.GetButtonDown("Fire1"))
    {
      if (meleeEquipped && !animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack") && !animator.GetNextAnimatorStateInfo(0).IsName("MeleeAttack"))
      {
        animator.SetTrigger("meleeAttack");
        onMeleeAttack?.Invoke();
      }
    }
    if (meleeEquipped)
    {
      meleeDamager.Activated = animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack") || animator.GetNextAnimatorStateInfo(0).IsName("MeleeAttack");
    }
  }

}
