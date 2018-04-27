using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Package.Modle;

namespace Assets.Script.Package.Manager
{
    public class UpdateHint : MonoBehaviour
    {
        public Text ItemName, Effect, Introduction;

        //update introduction
        public void UpdateHintUI(BaseItem item)
        {
            ItemName.text = item.ChineseName;
            Effect.text = item.EffectIntroduce;
            Introduction.text = item.Introduction;
        }
        //show the introduction
        public void show()
        {
            gameObject.SetActive(true);
        }
        //hidden the introduction
        public void hidden()
        {
            gameObject.SetActive(false);
        }
        //update the hint position
        public void setPos(Vector3 newPos)
        {
            gameObject.transform.localPosition = newPos;
        }

    }
}
