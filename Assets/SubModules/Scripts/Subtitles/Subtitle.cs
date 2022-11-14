using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
  [SerializeField] public string content;
  [SerializeField] public List<Color> ColorSet = new List<Color> { new Color(1, 1, 1, 1) };
  [SerializeField] public float time;

  private TMP_Text textMesh;
  private Color color;

  // Start is called before the first frame update
  void Start()
  {
    textMesh = GetComponent<TMP_Text>();
  }

  // Update is called once per frame
  void Update()
  { }

  public void SetContent(string value)
  {
    content = value;
  }

  public void ChooseColor(int index)
  {
    color = ColorSet[index];
  }

  public void SetTime(float value)
  {
    time = value;
  }

  public void ShowSubtitle()
  {
    textMesh.text = content;
    textMesh.color = new Color(color.r, color.g, color.b, 1.0f);
    StartCoroutine(hideSubtitle());
  }

  private IEnumerator hideSubtitle()
  {
    yield return new WaitForSeconds(time);
    textMesh.color = new Color(color.r, color.g, color.b, 0);
  }
}
