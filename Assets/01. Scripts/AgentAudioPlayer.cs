using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudioPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip _stepClip;
    [SerializeField]
    protected AudioClip _rollClip;

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(_stepClip);
    }

    public void PlayRollSound()
    {
        PlayClipWithVariablePitch(_rollClip);
    }
}
