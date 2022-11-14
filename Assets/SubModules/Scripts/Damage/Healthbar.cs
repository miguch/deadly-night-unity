using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.ImmersiveMedia.Damage
{
  /// <summary>
  /// Provides a health bar to show the user how much health an object has remaining
  /// </summary>
  public class Healthbar : MonoBehaviour
  {
    [SerializeField] public bool lookAt = true;
    [SerializeField] private Image healthBar;
    private Camera cam;

    private void Awake()
    {
      cam = FindObjectOfType<Camera>();
    }

    /// <summary>
    /// Sets the healthbar's fil percentage
    /// </summary>
    /// <param name="percentage"></param>
    public void SetHealthBarPercentage(float percentage)
    {
      healthBar.fillAmount = percentage;
    }

    void Update()
    {
      if (lookAt)
      {
        transform.LookAt(cam.transform.position); // Always look at the user in all dimensions so they can see the healthbar
      }
    }
  }
}
