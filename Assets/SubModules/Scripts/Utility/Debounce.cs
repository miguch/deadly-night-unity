using UnityEngine;
using System.Collections;

/// <summary>
/// Class that is used to control frequencies of function calls
/// </summary>
public class Debounce
{
  private bool isWaiting = false;

  public bool Wait { get => isWaiting; }

  public IEnumerator Invoke(float time)
  {
    isWaiting = true;
    yield return new WaitForSeconds(time);
    isWaiting = false;
  }

}