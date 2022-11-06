using UnityEngine;
using System.Collections;

/// <summary>
/// Class that is used to control frequencies of function calls
/// </summary>
public class Debounce
{
  private bool isWaiting = false;

  public bool Wait { get => isWaiting; }

  private float waitTime = 0;

  public IEnumerator Invoke(float time)
  {
    waitTime = time;
    isWaiting = true;
    yield return new WaitForSeconds(waitTime);
    isWaiting = false;
  }

}