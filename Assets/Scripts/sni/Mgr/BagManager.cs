using BagModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagModule
{


    public class BagManager : SingletonMono<BagManager>
    {
        
        
        private Bag _bag=new Bag();

        
        public Grid[] GetBagGrid(ItemType itemType)
        {
            return _bag.gridArrayDic[itemType];
        }



        public List<Item> GetTempProp()
        {
            return _bag.tempPropList;
        }


        public int GetCount(Item item)
        {
           return _bag.GetCount(item);
        }

       
        public void PutInBag(Item item)
        {
            //Debug.Log(0);
            _bag.PutInBag(item);
            
        }

        /// <summary>
        /// 从背包中取出
        /// </summary>
        /// <param name="grid"></param>
       public void TakeOutBag(Grid grid)
        {
            if (grid.Item == null) return;
            TakeOutBag(grid.Item);
        }

        /// <summary>
        /// 从背包中取出
        /// </summary>
        /// <param name="item"></param>
        public void TakeOutBag(Item item)
        {
            Debug.Log(123);
            if (item == null) return;
            _bag.TakeOutBag(item);
        }


        /// <summary>
        /// 从背包中删除
        /// </summary>
        /// <param name="grid"></param>
        public void DelInBag(Grid grid)
        {
            if (grid.Item == null) return;
            DelInBag(grid.Item);
        }

       /// <summary>
       /// 从背包中删除
       /// </summary>
       /// <param name="item"></param>
       public void DelInBag(Item item)
        {
            if (item == null) return;
            _bag.DelInBag(item);
        }

        
        public void Show()
        {
            _bag.Display();
        }


       
    }






}
