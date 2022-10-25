using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleBehavior : PlayableBehaviour
{
    public string subtitleText;
    public Color subtitleColor;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI textMeshProUGUI = playerData as TextMeshProUGUI;
        if (textMeshProUGUI)
        {
            textMeshProUGUI.text = subtitleText;
            textMeshProUGUI.color = new Color(subtitleColor.r, subtitleColor.g, subtitleColor.b, info.weight);
        }
    }

}
