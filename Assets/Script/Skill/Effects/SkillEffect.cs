using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class SkillEffect:ScriptableObject
{
    [SerializeField]
    public string Name;
    public abstract void ApplyEffect(SkillImpact impact, Entity target);
}