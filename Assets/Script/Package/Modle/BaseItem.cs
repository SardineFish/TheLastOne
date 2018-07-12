using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Package.Modle
{

    public enum PropTypes
    {
        Improve,
        Shorten,
        Enlarge,
        Attach,
        Player,
        None,
    }

    public class BaseItem : MonoBehaviour, IDocumented
    {
        [HideInInspector]
        public string name;
        [Header("Base property")]
        //item image
        public Sprite ItemImage;
        //chinese name 
        public string ChineseName;
        // the introduction of the item
        public string Introduction;
        //effect introduction
        public string EffectIntroduce;

        [Header("Special property")]
        public PropTypes propType = PropTypes.None;
        //xy的值分别对应不同作用的两个数值
        public Vector2 Effect = Vector2.zero;
        //风火雷电等技能选择
        public SkillEffect SkillEffect;

        public string Name => name;

        public string DisplayName => ChineseName;

        public string Description => Introduction;

        public Sprite Icon => ItemImage;

        void initProp()
        {

        }
      
    }


}
