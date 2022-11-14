using System.Collections;
using System.Collections.Generic;
using com.ImmersiveMedia.Damage;
using UnityEngine;
using UnityEngine.Events;
using com.ImmersiveMedia.CharacterControl;

public class Bullet : MonoBehaviour
{
  [SerializeField] Rigidbody rb;
  [SerializeField] float forceScaler = 50;
  [SerializeField] float persistTime = 1.5f;

  private void Awake()
  {
    rb.AddForce(transform.forward * forceScaler, ForceMode.Impulse);
    StartCoroutine(DestroyAfterDelay());
  }

  private IEnumerator DestroyAfterDelay()
  {
    yield return new WaitForSeconds(persistTime);
    Destroy(gameObject);
  }
}
