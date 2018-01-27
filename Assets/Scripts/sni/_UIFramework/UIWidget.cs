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


    public class UIWidget : UIPanel
    {

        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 调用它打开UI挂件
        /// </summary>
        /// <param name="arg"></param>
        public sealed override void Open(object arg = null)
        {
            Debug.Log(string.Format("Open() arg:{0}", arg));
            m_openArg = arg;
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
        }

        /// <summary>
        /// 调用它以关闭UI挂件
        /// </summary>
        public sealed override void Close(object arg = null)
        {
            Debug.Log(string.Format("Close() arg:{0}", arg));
            if (this.gameObject.activeSelf)
            {

                this.gameObject.SetActive(false);
            }

            OnClose(arg);
        }


        protected virtual void OnEnable()
        {
            OnOpen();
        }

        protected virtual void OnDisable()
        {
            OnClose();
        }




    }//Class End
	
	
}//namespace End
