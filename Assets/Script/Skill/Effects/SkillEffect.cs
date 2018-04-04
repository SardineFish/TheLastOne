using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillEffect
{
    public string Name;
    public abstract void ApplyEffect(Entity effectFrom, Entity target);
}