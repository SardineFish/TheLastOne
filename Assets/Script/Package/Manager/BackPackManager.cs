using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Script.Package.Modle;

namespace Assets.Script.Package.Manager
{
    public class BackPackManager : MonoBehaviour
    {
        //save all items' data
        public BaseItem[] items;
        //control the hint
        public UpdateHint hintUI;
        //get the empty grid of the girdpanel
        public GetEmptyGrid GetTheEmpty;
        //control the hint's visibility
        private bool IsShow = false;

        void Awake()
        {
            GridEvents.OnEnter += onEnter;
            GridEvents.OnExit += onExit;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                storeItem(0);
            //使提示框和拖拽时出现的物体跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform,
                Input.mousePosition, Camera.main, out position);     //把鼠标的屏幕坐标转换为UI的相对坐标
            if(IsShow)
            {
                hintUI.show();
                hintUI.setPos(position);
            }
        }
        private void onEnter(Transform GridTrans)
        {
            BaseItem item = ItemModel.getItem(GridTrans.name);
            hintUI.UpdateHintUI(item);
            Debug.Log("enter");
            IsShow = true;
        }

        private void onExit()
        {
            hintUI.hidden();
            IsShow = false;
            Debug.Log("exit");
        }

        //store item by grid and item
        public void storeItem(int itemID)
        {
            if (items[itemID]==null)
                return;
            else
            {
                Transform EmptyGridTrans = GetTheEmpty.GetEmpty();
                if (EmptyGridTrans == null)
                {
                    //creat another gridpanel
                }
                else
                {
                    creatItem(items[itemID], EmptyGridTrans);
                }
            }
        }
        //creat item int the special grid
        public void creatItem(BaseItem newItem, Transform GridTransform)
        {
            GameObject tempItem = Resources.Load<GameObject>("Prefabs/Item");
            GameObject ItemNow = Instantiate(tempItem);

            ItemNow.GetComponent<Image>().sprite = newItem.ItemImage;
            ItemNow.transform.SetParent(GridTransform);
            ItemNow.transform.localPosition = Vector3.zero;
            ItemNow.transform.localScale = new Vector3(1, 1, 1);
            ItemNow.transform.localRotation = Quaternion.identity;

            ItemModel.storeItem(GridTransform.name, newItem);
        }
    }
}
