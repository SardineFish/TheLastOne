using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class SkillEffect:ScriptableObject,IWeightedObject,IDocumented
{
    [SerializeField]
    public string DisplayName;
    public string Description;
    public Sprite Icon;
    public abstract void ApplyEffect(SkillImpact impact, Entity target, float multiple);
    [SerializeField]
    private float weight = 1;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public string Name => name;

    string IDocumented.DisplayName => DisplayName;

    string IDocumented.Description => Description;

    Sprite IDocumented.Icon => Icon;
}