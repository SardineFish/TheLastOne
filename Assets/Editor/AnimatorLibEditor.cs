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
    [CustomEditor(typeof(AnimatorLib))]
    public class AnimatorLibEditor : AssetLibEditor<AnimatorLib, SkillAction>
    {
    }
}
