using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ObjectAssetLib),true)]
    public class ObjectAssetsLibEditor : AssetLibEditor<ObjectAssetLib, UnityEngine.Object>
    {

    }
}
