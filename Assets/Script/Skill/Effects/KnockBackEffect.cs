using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="KnockBack", menuName ="SkillEffect/KnockBack")]
public class KnockBackEffect : SkillEffect
{
    public float Impulse;
    public override void ApplyEffect(Entity effectFrom, Entity target)
    {
        target.GetComponent<CustomRigidBody>().AddForce((target.transform.position - effectFrom.transform.position).normalized * Impulse, ForceMode.Impulse);
    }
}