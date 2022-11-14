using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ImmersiveMedia.Utility
{
    public class ApplicationExit : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
