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
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework
{


    public   class UIWindow : UIPanel
    {


        //=======================================================================

        public delegate void CloseEventHandle(object arg = null);

        //=======================================================================
        /// <summary>
        /// 关闭按钮，大部分窗口都会有关闭按钮
        /// </summary>
        [SerializeField]
        private Button m_btnClose;

        

        /// <summary>
        /// 窗口关闭委托
        /// </summary>
        public  CloseEventHandle OnWindowClose;

        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 该UI的当前实例是否曾经被打开过
        /// </summary>
        private bool m_isOpenedOnce;

        /// <summary>
        /// 当UI可用时调用
        /// </summary>
        protected virtual void OnEnable()
        {
            Debug.Log("OnEnable()");
            if (m_btnClose != null)
            {
                m_btnClose.onClick.AddListener(OnBtnClose);
            }
        }

        /// <summary>
        /// 当UI不可用时调用
        /// </summary>
        protected virtual void OnDisable()
        {
            Debug.Log("OnDisable()");

            if (m_btnClose != null)
            {
                m_btnClose.onClick.RemoveAllListeners();
            }
        }

        /// <summary>
        /// 当点击关闭按钮时调用
        /// 但是并不是每一个Window都有关闭按钮
        /// </summary>
        private void OnBtnClose()
        {
            Debug.Log("OnBtnClose()");
            Close(0);
        }


        /// <summary>
        /// 调用它打开UIWindow
        /// </summary>
        /// <param name="arg"></param>
        public sealed override void Open(object arg = null)
        {
            Debug.Log("Open("+arg+")");
            m_openArg = arg;
            m_isOpenedOnce = false;
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            this.GetComponent<RectTransform>().SetAsLastSibling();
            OnOpen(arg);
            m_isOpenedOnce = true;
        }

        /// <summary>
        /// 调用它以关闭UIWindow
        /// </summary>
        public sealed override void Close(object arg = null)
        {
            Debug.Log("Close("+arg+")");
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose(arg);
            if (OnWindowClose != null)
            {   
                OnWindowClose(arg);
                //Debug.Log("窗口关闭事件置空！！");
                //OnWindowClose = null;  
            }
        }

    }//Class End
	
	
}//namespace End
