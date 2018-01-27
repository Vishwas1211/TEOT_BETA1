/*   
 *   Author:
 *
 *   Title:
 *   Topic:
 *
 *   FunctionDescription:
 *   1.
 *   2.
 *         
 *   Date:
 *   Version:
 *   Modify Recoder:         
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UIFramework
{


    public  class UIPage : UIPanel
    {

        /// <summary>
        /// 返回按钮，大部分Page都会有返回按钮
        /// </summary>
        [SerializeField]
        private Button _btnGoBack;

        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object _openArg;

        /// <summary>
        /// 该UI的当前实例是否曾经被打开过
        /// </summary>
        private bool _isOpenedOnce;

       
        public sealed override void Open(object arg = null)
        {
            Debug.Log("Open()");
            _openArg = arg;
            _isOpenedOnce = false;

            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
            _isOpenedOnce = true;
        }

        public sealed override void Close(object arg = null)
        {
            Debug.Log("Close()");
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose(arg);
        }


        /// <summary>
        /// 当点击“返回”时调用
        /// 但是并不是每一个Page都有返回按钮
        /// </summary>
        private void OnBtnGoBack()
        {
            Debug.Log("OnBtnGoBack()");
            //UIManager.Instance.GoBackPage();
        }



        /// <summary>
        /// 当UIPage被激活时调用
        /// </summary>
        protected void OnEnable()
        {
            Debug.Log("UIPage:OnEnable()");
            if (_btnGoBack != null)
            {
                _btnGoBack.onClick.AddListener(OnBtnGoBack);
            }

#if UNITY_EDITOR
            if (_isOpenedOnce)
            {
                //如果UI曾经被打开过，
                //则可以通过UnityEditor来快速触发Open/Close操作
                //方便调试
                OnOpen(_openArg);
            }
#endif
        }

        /// <summary>
        /// 当UI不可用时调用
        /// </summary>
        protected void OnDisable()
        {
            Debug.Log("UIPage:OnDisable()");
#if UNITY_EDITOR
            if (_isOpenedOnce)
            {
                //如果UI曾经被打开过，
                //则可以通过UnityEditor来快速触发Open/Close操作
                //方便调试
                OnClose();
            }
#endif
            if (_btnGoBack != null)
            {
                _btnGoBack.onClick.RemoveAllListeners();
            }
        }



    }//Class End
	
	
}//namespace End
