using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BagModule
{


    /// <summary>
    /// 格子类：存储物品
    /// </summary>
    public class Grid
    {

        private int _count;
        private Item _item;
        private int _identifier;
        //public const uint maxStackCount = 10;
        public Action<bool> updateDisplay;//true  改变图片的显示  false 只改变数字
        public ItemType itemType;
        public bool isActive;//是否被激活使用格子中的物品


        public int Count
        {
            get { return _count; }
        }

        public Item Item
        {
            get { return _item; }
        }

        public int Identifier
        {
            get { return _identifier; }
        }
        
        internal void AddItem(uint id)
        {

        }

        internal void AddItem(Item item)
        {
            if (item == null) return;
            if (_item != null)
            {
                if (_item.Id == item.Id)
                {
                    _count++;
                    if (updateDisplay != null)
                    {
                        updateDisplay(false);
                    }
                    //else
                    //{
                    //    Debug.Log("<color=green> updateDisplay   false is null false</color>");
                    //}
                    return;
                }
                Debug.Log(string.Format("<color=red> grid  AddItem()发生错误  {0}:{1}=>原始ID:{2}</color>", item.ItemType,item.Id, _item.Id));
            }
            
            _item = item;
            _count++;
            _identifier = item.Identifier;
            if (updateDisplay != null)
            {
                Debug.Log("<color=green> 执行更新事件 true</color>");
                updateDisplay(true);
            }
            //else
            //{
            //    Debug.Log(string.Format("<color=green> updateDisplay  true is null false</color>"));
            //}
            //Debug.Log(2);
        }

        internal void  DelItem(int num = 1)
        {
            if (_item == null) return;
            if (num <= 0) return;
            if (num > _count) num = _count;
            _count -= num;
            if (_count == 0)
            {
                _item = null;
                _count = 0;
                _identifier = -1;
            }
            if (updateDisplay != null)
            {
                updateDisplay(false);
            }
        }

        internal void DelAllItem()
        {
            DelItem(_count);
        }
    }
}
