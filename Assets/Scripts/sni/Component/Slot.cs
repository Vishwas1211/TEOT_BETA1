using BagModule;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;




public class Slot : MonoBehaviour
{
    public ItemType itemType;
    private Grid _grid;
    private Button _btn;
    public Image itemImage;
    public Text itemCount;

    private DOTweenAnimation _doTween;
    private DOTweenVisualManager _doTweenMgr;
    private Tweener _tweener;

    private Vector3 _orig;

    void Awake()
    {
        _grid = new Grid();
        _btn = GetComponent<Button>();
        DoTweenInit();
    }

    void OnEnable()
    {

        _grid.updateDisplay += Display;
        Display(true);
        if (_btn)
        {
            _btn.onClick.AddListener(Takeout);
        }
        else
        {
            Debug.Log("<color=red>缺少button 组件</color>");
        }
    }

    void OnDisable()
    {
        _grid.updateDisplay -= Display;
        _btn.onClick.RemoveListener(Takeout);
        DoTweenReset();
    }

    public void SetGrid(Grid grid)
    {
        _grid = grid;
    }

    public void SetOrigPos(Vector3 pos)
    {
        _orig = pos;
    }

    void Display(bool isChangeItem)
    {

        if (itemImage == null || itemCount == null)
        {
            //Debug.Log("<color=red>Slot 缺少Image 实例或者 Text 实例</color>");
            //return;
        }




        if (_grid.Item == null)
        {
            if (itemImage)
                itemImage.enabled = false;
            if (itemCount)
                itemCount.text = "";
        }
        else
        {
            if (itemImage)
                itemImage.enabled = true;
            if (isChangeItem)
            {
                if (_grid.Item.image != null)
                {

                    if (itemImage)
                        itemImage.sprite = _grid.Item.image;
                }
                else
                {
                    //Debug.Log(string.Format("<color> xb</color>"));
                    Sprite s = Resources.Load<Sprite>(_grid.Item.ImagePath);
                    if (itemImage)
                        itemImage.sprite = s;
                }

            }
            if (_grid.Count == 1)
            {
                if (itemCount)
                    itemCount.text = "";
                return;
            }
            if (itemCount)
                itemCount.text = _grid.Count.ToString();
        }
    }


    private void Takeout()
    {
        UIManager.Instance.CloseUI(AppConst.UIPATH_UIRadialMenuWindow);
        UIManager.Instance.CloseUI(AppConst.UIPATH_UIPropDataWindow);
        GameObject.Find("FreeLookCameraRig").transform.GetComponent<FreeLookCam>().enabled = true;
        if (PlayerManager.Instance.motionController != null)
            PlayerManager.Instance.motionController.IsEnabled = true;
        if (_grid == null || _grid.Item == null) return;
        if (_grid.Item.ItemType == ItemType.Rifle || _grid.Item.ItemType == ItemType.Pistol || _grid.Item.ItemType == ItemType.Grenade || _grid.Item.ItemType == ItemType.CloseWeapon)
        {
            BagManager.Instance.TakeOutBag(_grid);
        }

        if (_grid.Item.ItemType == ItemType.BloodPack)
        {
            BagManager.Instance.DelInBag(_grid);

        }

        // 剩下三种为资料  暂时直接传给玩家就OK
        if (GlobalEvent.useItemEvent != null)
        {
            Debug.Log(string.Format("<color=yellow>玩家使用物品：{0}</color>", _grid.Item.Name));
            GlobalEvent.useItemEvent.Invoke(_grid.Item);
        }



    }


    #region DotweenCtrl
    void DoTweenInit()
    {
        _doTween = GetComponent<DOTweenAnimation>();
        if (_doTween == null)
        {
            _doTween = this.gameObject.AddComponent<DOTweenAnimation>();
        }

        _doTween.autoPlay = false;
        _doTween.autoKill = false;
        _doTween.animationType = DG.Tweening.Core.DOTweenAnimationType.LocalMove;
        _doTween.isFrom = false;

    }

    public void DoTweenReset()
    {

        if (_tweener != null && !_tweener.IsComplete())
        {

            _tweener.Complete();
        }


        if (_orig != Vector3.zero)
            (this.transform as RectTransform).anchoredPosition3D = _orig;

    }

    public void PlayDoTween(float dur)
    {
        _tweener = transform.DOLocalMove(_doTween.endValueV3, dur);

    }

    public void SetDoTween(Vector3 toPos)
    {

        _doTween.endValueV3 = toPos;



    }
    #endregion





}
