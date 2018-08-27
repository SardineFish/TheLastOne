using UnityEngine;
using System.Collections;

public class SkillImpactEffectRenderer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var skillImpact = GetComponentInParent<SkillImpact>();
        if(skillImpact.ImpactType == ImpactType.Penetrative)
        {
            transform.localScale = new Vector3(1, 1, skillImpact.PenetrateDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
