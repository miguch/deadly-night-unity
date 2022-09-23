using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[UnityEditor.Timeline.CustomTimelineEditor(typeof(SubtitleClip))]
public class SubtitleClipEditor : UnityEditor.Timeline.ClipEditor
{
    public override void OnCreate(TimelineClip clip, TrackAsset track, TimelineClip clonedFrom)
    {
        if (clonedFrom == null)
        {
            clip.easeInDuration = 0.25;
            clip.easeOutDuration = 0.25;
        }
    }
}
 