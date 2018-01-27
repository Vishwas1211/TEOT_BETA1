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


namespace UIFramework
{

    //[RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void Open(object arg = null)
        {
            Debug.Log("Open(" + arg + ")");
        }

        public virtual void Close(object arg = null)
        {
            Debug.Log("Close("+arg+")");
        }

        /// <summary>
        /// 当前UI是否打开
        /// </summary>
        public bool IsOpen { get { return this.gameObject.activeSelf; } }


        /// <summary>
        /// 当UI关闭时，会响应这个函数
        /// 该函数在重写时，需要支持可重复调用
        /// </summary>
        protected virtual void OnClose(object arg = null)
        {
            Debug.Log("OnClose(" + arg + ")");
            //UIManager.GetInstance().OnClose(this.gameObject.name);
        }

        /// <summary>
        /// 当UI打开时，会响应这个函数
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void OnOpen(object arg = null)
        {
            //Debug.Log("OnOpen() ");         
            Debug.Log("OnOpen(" + arg + ")");
            //UIManager.GetInstance().OnOpen(this.gameObject.name, this);
        }





    }//Class End
	
	
}//namespace End
