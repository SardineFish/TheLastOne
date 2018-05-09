using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SkillActionLib))]
    public class SkillActionLibEditor : AssetLibEditor<SkillActionLib, SkillAction>
    {
    }
}
