using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class SkillEffect:ScriptableObject,IWeightedObject
{
    [SerializeField]
    public string DisplayName;
    public abstract void ApplyEffect(SkillImpact impact, Entity target);
    [SerializeField]
    private float weight = 1;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }
}