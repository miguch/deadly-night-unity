using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillScreen : MonoBehaviour
{
    [SerializeField] public Camera cam;    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AspectRatioFitter>().aspectRatio = cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
