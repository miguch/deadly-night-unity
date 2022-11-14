using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
  [SerializeField] PlayableDirector playableDirector;

  [SerializeField] bool skippable = true;

  [SerializeField] float skipTime = 60;

  // Update is called once per frame
  void Update()
  {
    if (skippable && Input.GetButtonDown("Jump"))
    {
      playableDirector.time = skipTime;
      skippable = false;
    }
  }
}
