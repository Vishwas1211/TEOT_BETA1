using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BagModule
{
    /// <summary>
    /// 背包：存储格子
    /// </summary>
    public class Bag
    {
        private Dictionary<ItemType, Grid[]> _gridArrayDic;
        private List<Item> _tempPropList;
        internal Bag()
        {
            _gridArrayDic = new Dictionary<ItemType, Grid[]>();
            _tempPropList = new List<Item>();
            Init();
        }

        internal Dictionary<ItemType, Grid[]> gridArrayDic
        {
            get { return _gridArrayDic; }
        }
        internal List<Item> tempPropList
        {
            get { return _tempPropList; }
        }

        #region 背包的初始化
        private void Init()
        {
            Grid[] rifleArray = new Grid[2];//步枪
            Grid[] pistolArray = new Grid[1];//手枪
            Grid[] grenadeArray = new Grid[3];//手雷
            Grid[] closeWeaponArray = new Grid[1];//近战武器

            Grid[] antidoteArray = new Grid[1];//解毒剂
            Grid[] bloodPackArray = new Grid[1];//血包

            Grid[] diskArray = new Grid[10];//硬盘
            Grid[] dataPaperArray = new Grid[10];//资料页
            Grid[] workCardArray = new Grid[3];//工作牌

            Grid[] rifleBulletArray = new Grid[1];//步枪子弹
            Grid[] pistolBulletArray = new Grid[1];//手枪武器

            AddGridToArray(rifleArray, ItemType.Rifle);
            AddGridToArray(pistolArray, ItemType.Pistol);
            AddGridToArray(grenadeArray, ItemType.Grenade);
            AddGridToArray(closeWeaponArray, ItemType.CloseWeapon);

            AddGridToArray(antidoteArray, ItemType.Antidote);
            AddGridToArray(bloodPackArray, ItemType.BloodPack);

            AddGridToArray(diskArray, ItemType.Disk);
            AddGridToArray(dataPaperArray, ItemType.DataPaper);
            AddGridToArray(workCardArray, ItemType.WorkCard);

            AddGridToArray(rifleBulletArray, ItemType.RifleBullet);
            AddGridToArray(pistolBulletArray, ItemType.PistolBullet);


            _gridArrayDic.Add(ItemType.Rifle, rifleArray);
            _gridArrayDic.Add(ItemType.Pistol, pistolArray);
            _gridArrayDic.Add(ItemType.Grenade, grenadeArray);
            _gridArrayDic.Add(ItemType.CloseWeapon, closeWeaponArray);

            _gridArrayDic.Add(ItemType.Antidote, antidoteArray);
            _gridArrayDic.Add(ItemType.BloodPack, bloodPackArray);

            _gridArrayDic.Add(ItemType.Disk, diskArray);
            _gridArrayDic.Add(ItemType.DataPaper, dataPaperArray);
            _gridArrayDic.Add(ItemType.WorkCard, workCardArray);

            _gridArrayDic.Add(ItemType.RifleBullet, rifleBulletArray);
            _gridArrayDic.Add(ItemType.PistolBullet, pistolBulletArray);
        }
        private void AddGridToArray(Grid[] array, ItemType itemType)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Grid grid = new Grid();
                grid.itemType = itemType;
                array[i] = grid;
            }
        }
        #endregion

        #region 向背包中添加物品
        internal void PutInBag(Item item)
        {
            if (item == null) return;
            if (item.ItemType == ItemType.TempProp)
            {
                PutTempPropToBag(item);
                return;
            }

            Grid[] gridArray = _gridArrayDic[item.ItemType];
            switch (item.ItemType)
            {
                case ItemType.DataPaper:
                case ItemType.Disk:
                case ItemType.WorkCard:
                    PutTypeOne(gridArray, item);
                    break;
                case ItemType.Pistol:
                case ItemType.CloseWeapon:
                case ItemType.BloodPack:
                case ItemType.Antidote:
                    PutTypeTwo(gridArray, item);
                    break;
                case ItemType.Grenade:
                    PutTypeThree(gridArray, item);
                    break;
                case ItemType.Rifle:
                    PutTypeFour(gridArray, item);
                    break;
            }
        }

        /// <summary>
        /// 资料类型的添加
        /// </summary>
        /// <param name="gridArray"></param>
        /// <param name="item"></param>
        private void PutTypeOne(Grid[] gridArray, Item item)
        {
            //if (item.ItemType == ItemType.DataPaper || item.ItemType == ItemType.Disk || item.ItemType == ItemType.WorkCard)
            //{
            if (item.Identifier <= 0 || item.Identifier > gridArray.Length)
            {
                Debug.Log(string.Format("<color=red> 设计文件出错  识别符出错：{0}</color>", item.Identifier));
                return;
            }
            for (int i = 0; i < gridArray.Length; i++)
            {
                if (item.Identifier - 1 == i)
                {
                    if (gridArray[i].Item != null) return;
                    gridArray[i].AddItem(item);
                    return;
                }
            }
        }

        /// <summary>
        ///  同类物品只能拾取一种的添加(同类物品只能使用一个格子)
        /// </summary>
        /// <param name="gridArray"></param>
        /// <param name="item"></param>
        private void PutTypeTwo(Grid[] gridArray, Item item)
        {
            //if (item.ItemType == ItemType.Pistol || item.ItemType == ItemType.CloseWeapon)
            //{
            if (!item.IsStack)
            {
                if (gridArray[0].Item != null)
                {
                    if (gridArray[0].Item.Id == item.Id)
                    {
                        if (GlobalEvent.discardEvent != null)
                        {
                            GlobalEvent.discardEvent.Invoke(gridArray[0].Item);
                            Debug.Log(string.Format("<color=yellow>丢弃物品:{0}(物品实体丢弃在脚下)</color>", gridArray[0].Item.Name));
                        }
                        return;
                    }
                    else//不同则替换
                    {
                        //TODO:此处需要执行一个丢弃物品的事件(物品实体丢弃在脚下)
                        if (GlobalEvent.discardEvent != null)
                        {
                            GlobalEvent.discardEvent.Invoke(gridArray[0].Item);
                            Debug.Log(string.Format("<color=yellow>丢弃物品:{0}(物品实体丢弃在脚下)</color>", gridArray[0].Item.Name));
                        }
                        gridArray[0].DelAllItem();
                    }
                }
            }

            gridArray[0].AddItem(item);
            //return;
            //}
        }

        /// <summary>
        /// 同类物品可拾取多种，可叠加物品的添加
        /// </summary>
        /// <param name="gridArray"></param>
        /// <param name="item"></param>
        private void PutTypeThree(Grid[] gridArray, Item item)
        {
            //if (item.ItemType == ItemType.Grenade || item.ItemType == ItemType.BloodPack || item.ItemType == ItemType.Antidote)
            //{
            for (int i = 0; i < gridArray.Length; i++)
            {
                if (item.Identifier - 1 == i)
                {
                    gridArray[i].AddItem(item);
                    return;
                }
            }
            //    return;
            //}
        }

        /// <summary>
        /// 不可叠加，同类物品可以拾取两件
        /// </summary>
        /// <param name="gridArray"></param>
        /// <param name="item"></param>
        private void PutTypeFour(Grid[] gridArray, Item item)
        {
            //ItemType.Rifle:
            int activeIndex = -1;
            for (int i = 0; i < gridArray.Length; i++)
            {
                if (gridArray[i].isActive)
                {
                    activeIndex = i;
                }

                if (gridArray[i].Item == null)
                {
                    gridArray[i].AddItem(item);
                    return;
                }
            }
            //格子已满，需要清除格子内的物品

            if (activeIndex != -1)
            {
                if (GlobalEvent.discardEvent != null)
                {
                    GlobalEvent.discardEvent.Invoke(gridArray[activeIndex].Item);
                    Debug.Log(string.Format("<color=yellow>丢弃物品:{0}(物品实体丢弃在脚下)</color>", gridArray[activeIndex].Item.Name));
                }
                gridArray[activeIndex].DelAllItem();
                gridArray[activeIndex].AddItem(item);
            }
            else
            {
                if (GlobalEvent.discardEvent != null)
                {
                    GlobalEvent.discardEvent.Invoke(gridArray[0].Item);
                    Debug.Log(string.Format("<color=yellow>丢弃物品:{0}(物品实体丢弃在脚下)</color>", gridArray[0].Item.Name));
                }
                gridArray[0].DelAllItem();
                gridArray[0].AddItem(item);
            }

        }

        private void PutTempPropToBag(Item item)
        {
            if (item == null) return;
            if (item.ItemType != ItemType.TempProp) return;
            _tempPropList.Add(item);
        }

        #endregion

        #region  Debug bag
        internal void Display()
        {
            foreach (var gridDic in _gridArrayDic)
            {
                for (int i = 0; i < gridDic.Value.Length; i++)
                {
                    if (gridDic.Value[i].Item != null)
                        Debug.Log(gridDic.Key + ":" + gridDic.Value[i].Item.Name);
                }
            }
        }

        #endregion

        #region 取出或丢弃

        internal void TakeOutBag(Item item)
        {
            if (item == null) return;
            if (item.ItemType == ItemType.Rifle)
            {
                Grid[] gridArray = _gridArrayDic[item.ItemType];
                int index = -1;
                int count = 0;
                for (int i = 0; i < gridArray.Length; i++)
                {
                    if (gridArray[i].Item == null) continue;
                    if (gridArray[i].Item.Id == item.Id && gridArray[i].Item.Identifier == item.Identifier)
                    {
                        if (count <= 0)//只记录第一个找到的索引
                        {
                            index = i;
                        }
                        count++;

                    }
                }
                if (count > 1)
                {
                    Debug.Log(string.Format("<color=red>同种步枪的识别符 未设置  步枪名字为{0}</colro>", item.Name));
                }
                for (int i = 0; i < gridArray.Length; i++)
                {
                    if (index == i)
                    {
                        gridArray[i].isActive = true;
                        continue;
                    }
                    gridArray[i].isActive = false;
                }

            }
            if (item.ItemType == ItemType.Pistol || item.ItemType == ItemType.Grenade || item.ItemType == ItemType.CloseWeapon)
            {
                Grid[] gridArray = _gridArrayDic[ItemType.Rifle];
                for (int i = 0; i < gridArray.Length; i++)
                {
                    gridArray[i].isActive = false;
                }
            }

            if (item.ItemType == ItemType.TempProp)//取出临时物品
            {
                Debug.Log(_tempPropList.Count + "1");
                for (int i = _tempPropList.Count - 1; i >= 0; i--)
                {
                    if (_tempPropList[i].Id == item.Id && _tempPropList[i].Identifier == item.Identifier)
                    {
                        _tempPropList.RemoveAt(i);
                        break;
                    }
                }
                Debug.Log(_tempPropList.Count + "2");
            }
        }

        internal void DelInBag(Item item)
        {
            if (item == null) return;
            Grid[] gridArray = _gridArrayDic[item.ItemType];
            for (int i = 0; i < gridArray.Length; i++)
            {
                if (gridArray[i].Item.Id == item.Id)
                {
                    gridArray[i].DelItem();
                    return;
                }
            }
        }

        /// <summary>
        /// 装备类型的(步枪、手枪、手雷、近战武器)
        /// </summary>
        /// <param name="gridArray"></param>
        /// <param name="item"></param>
        private void TakeTypeOne(Grid[] gridArray, Item item)
        {
            for (int i = 0; i < gridArray.Length; i++)
            {
                if (gridArray[i].Item == null) continue;
                if (gridArray[i].Item.Id == item.Id)
                {
                    //test
                    gridArray[i].DelItem();
                }
                break;
            }
        }

        #endregion
        internal int GetCount(Item item)
        {
            if (_gridArrayDic.ContainsKey(item.ItemType))
            {
                Grid[] grids = _gridArrayDic[item.ItemType];
                for (int i = 0; i < grids.Length; i++)
                {
                    if (grids[i].Item.Id == item.Id && grids[i].Item.Identifier == item.Identifier)
                    {
                        return grids[i].Count;
                    }
                }
            }
            return -1;
        }
    }
}
