using UnityEngine;
using System.Collections;
using Cinemachine;

[CreateAssetMenu(fileName = "CameraShakeEffect", menuName = "SkillEffect/CameraShakeEffect")]
public class CameraShakeEffect : PropertyEffect
{
    public float ShakeTime = 0.2f;
    public float ShakeStrength = 2;
    public override void ApplyEffect(SkillImpact impact, float multiple)
    {
        GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = multiple * ShakeStrength;
        impact.WaitForSecond(() =>
        {
            GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        }, ShakeTime);
    }
}
