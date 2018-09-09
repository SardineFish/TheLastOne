using UnityEngine;
using System.Collections;

public enum ImpactEffectType
{
    Billboard,
    Penetrative,
    RangePlane,
    Collider
}
public class SkillImpactEffectRenderer : MonoBehaviour
{
    public float NearRange = 0;
    public float EffectVisibleTime = 1;
    public ImpactEffectType ImpactEffectType;
    public bool KeepVisible = false;
    // Use this for initialization
    void Start()
    {
        var skillImpact = GetComponentInParent<SkillImpact>();
        if(ImpactEffectType == ImpactEffectType.Penetrative)
        {
            transform.Translate(Vector3.forward * NearRange, Space.Self);
            transform.localScale = new Vector3(1, 1, skillImpact.PenetrateDistance-NearRange);
        }
        else if(ImpactEffectType == ImpactEffectType.RangePlane)
        {
            transform.localScale = new Vector3((skillImpact.ImpactRadius + NearRange) * 2, 1, skillImpact.ImpactRadius + NearRange);
        }
        else if (ImpactEffectType == ImpactEffectType.Collider)
        {
            transform.localScale = new Vector3(skillImpact.ImpactRadius * 2, 1, skillImpact.ImpactRadius);
        }
        if (!KeepVisible)
        {
            this.WaitForSecond(() =>
            {
                GetComponent<Renderer>().enabled = false;
            }, EffectVisibleTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ImpactEffectType == ImpactEffectType.Billboard)
        {
            var dir = transform.position - GameSystem.Instance.MainCamera.transform.position;
            transform.LookAt(transform.position + dir);
        }
    }
}
