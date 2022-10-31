using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerformActionAfterEnabled : MonoBehaviour
{
    [SerializeField] float actionWaitTime;
    [SerializeField] UnityEvent onWaitTimeComplete;


    private void OnEnable()
    {
        StartCoroutine(PerformAfterDuration());
    }

    private IEnumerator PerformAfterDuration()
    {
        yield return new WaitForSeconds(actionWaitTime);
        onWaitTimeComplete?.Invoke();
    }
}
