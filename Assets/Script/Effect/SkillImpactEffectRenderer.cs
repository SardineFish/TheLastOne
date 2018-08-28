using UnityEngine;
using System.Collections;

public class SkillImpactEffectRenderer : MonoBehaviour
{
    public float NearRange = 0;
    // Use this for initialization
    void Start()
    {
        var skillImpact = GetComponentInParent<SkillImpact>();
        if(skillImpact.ImpactType == ImpactType.Penetrative)
        {
            transform.Translate(Vector3.forward * NearRange, Space.Self);
            transform.localScale = new Vector3(1, 1, skillImpact.PenetrateDistance-NearRange);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
