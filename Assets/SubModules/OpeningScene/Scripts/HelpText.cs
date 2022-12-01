using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Help")) {
            GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0.8f);
        } else {
            GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
        }
    }
}
