using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hints : MonoBehaviour
{
  [SerializeField] public Subtitle subtitle;

  [SerializeField] public AudioSource audioSource;
  [SerializeField] public AudioClip weaponClip;
  [SerializeField] public AudioClip katanaClip;

  public void PlayKatanaHint()
  {
    StartCoroutine(katanaHintSequence());
  }

  private IEnumerator katanaHintSequence()
  {
    yield return new WaitForSeconds(1.2f);
    subtitle.ChooseColor(0);
    subtitle.SetContent("John: You need to go find a weapon");
    subtitle.SetTime(2);
    subtitle.ShowSubtitle();
    audioSource.clip = weaponClip;
    audioSource.Play();

    yield return new WaitForSeconds(2.3f);

    subtitle.SetContent("John: There's a katana at the left of the gate");
    subtitle.SetTime(2f);
    subtitle.ShowSubtitle();
    audioSource.clip = katanaClip;
    audioSource.Play();
  }
}
