using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

        TextMeshProUGUI textMeshProUGUI = playerData as TextMeshProUGUI;

        string currentText = "";
        float currentAlpha = 0;
        Color currentColor = new Color(1, 1, 1, 0);

        if (!textMeshProUGUI) {
            return;
        }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0) {
                ScriptPlayable<SubtitleBehavior> inputPlayable = (ScriptPlayable<SubtitleBehavior>)playable.GetInput(i);
                SubtitleBehavior input = inputPlayable.GetBehaviour();
                currentAlpha = inputWeight;
                currentText = input.subtitleText;
                currentColor = input.subtitleColor;
            }
        }

        textMeshProUGUI.text = currentText;
        textMeshProUGUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
    }
}
