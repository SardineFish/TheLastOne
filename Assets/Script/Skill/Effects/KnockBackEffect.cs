using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="KnockBack", menuName ="SkillEffect/KnockBack")]
public class KnockBackEffect : SkillEffect
{
    public float Impulse;
    public override void ApplyEffect(SkillImpact impact, Entity target)
    {
        //target.GetComponent<CustomRigidBody>().AddForce((target.transform.position - impact.transform.position).ClipY().normalized * Impulse, ForceMode.Impulse);
        target.GetComponent<SkillController>().MovementSkill.KnockBack((target.transform.position - impact.transform.position).ClipY().normalized * Impulse);
    }
}