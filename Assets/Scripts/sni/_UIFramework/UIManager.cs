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


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIFramework
{


    public class UIManager : SingletonMono<UIManager>
    {
        class UIPageTrack
        {
            public string name;
            public string scene;
        }
        public static string MainScene = "Main";
        public static string MainPage = "UIMainPage";

        private Stack<UIPanel> _uiStack;
        private Dictionary<string, UIPanel> _loadedPanelDic;
        private Dictionary<string, UIPanel> _curOpenedUIDic;
        private Action<string> _sceneLoaded;//场景加载完的回调方法

        private Stack<UIPageTrack> _pageTrackStack;
        private UIPageTrack _currentPage;
        //private List<UIPanel> m_listLoadedPanel;


        private string _uiResRoot = "UI/";
        public Camera UiCamera
        {
            get { return _uiCamera; }
        }

        public Canvas RootCanvas
        {
            get { return _uiRootGo.GetComponent<Canvas>(); }
        }

        public Transform HpBarRoot
        {
            get { return _hpBarRoot; }
        }



        #region 节点
        private GameObject _uiRootGo;
        private Camera _uiCamera;
        private Transform _uiPageTrans;
        private Transform _uiWindowTrans;
        private Transform _uiWidgetTrans;
        private Transform _hpBarRoot;
        #endregion

        #region 初始化

         void Awake()
        {
           
            _pageTrackStack = new Stack<UIPageTrack>();
            //m_listLoadedPanel = new List<UIPanel>();

            _uiStack = new Stack<UIPanel>(10);
            _curOpenedUIDic = new Dictionary<string, UIPanel>(10);
            _loadedPanelDic = new Dictionary<string, UIPanel>(10);
            InitRoot();
        }







        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="uiResRoot">UI资源的根目录，默认为"UI/"</param>
        public void Init(string uiResRoot)
        {
            _uiResRoot = uiResRoot;
           
            //监听UnityScene加载事件
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (_sceneLoaded != null)
                {
                    _sceneLoaded(scene.name);
                }
            };
        }

        /// <summary>
        /// 节点初始化
        /// </summary>
        private void InitRoot()
        {
            _uiRootGo = GameObject.FindGameObjectWithTag("UIRoot");
            _uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
            if (_uiRootGo == null)
            {
                Debug.LogError("缺少 UIRoot");
                return;
            }

            _uiPageTrans = _uiRootGo.transform.Find("UIPageRoot");
            _uiWidgetTrans = _uiRootGo.transform.Find("UIWidgetRoot");
            _uiWindowTrans = _uiRootGo.transform.Find("UIWindowRoot");
            _hpBarRoot= _uiRootGo.transform.Find("HpBarRoot");

            if (_uiPageTrans == null)
            {
                CreatRoot("UIPageRoot", _uiRootGo.transform);
            }

            if (_uiWidgetTrans == null)
            {
                CreatRoot("UIWidgetRoot", _uiRootGo.transform);
            }
            if (_uiWindowTrans == null)
            {
                CreatRoot("UIWindowRoot", _uiRootGo.transform);
            }
            DontDestroyOnLoad(_uiRootGo);
        }

        private Transform CreatRoot(string rootName, Transform parent)
        {
            GameObject go = new GameObject(rootName);
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            return go.transform;
        }
        #endregion

        #region 打开UI
        private T Open<T>(string name, object arg = null) where T : UIPanel
        {
            //Debug.Log("Open:" + typeof(T).Name);

            T ui = Load<T>(name);
            if (ui != null)
            {
                ui.Open(arg);
            }
            else
            {
                Debug.LogError(string.Format("Open() Failed! Name:{0}", name));
            }
            return ui;
        }

        /// <summary>
        /// 加载UI，如果UIRoot下已经有了，则直接取UIRoot下的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private T Load<T>(string name) where T : UIPanel
        {
            UIPanel ui = null;
            //Debug.Log("打开"+ name);
            if (_loadedPanelDic.ContainsKey(name))
            {
                _loadedPanelDic.TryGetValue(name, out ui);
                return (T)ui;
            }

            if (ui == null)
            {
                GameObject original = LoadUIPrefab(name);
                if (original != null)
                {
                    GameObject go = GameObject.Instantiate(original);
                    ui = go.GetComponent<T>();
                    if (ui != null)
                    {
                        go.name = name;
                        SetParentRoot<T>(go);
                    }
                    else
                    {
                        Debug.LogError("Load() Prefab没有增加对应组件: " + name);
                    }
                }
                else
                {
                    Debug.LogError("Load() Res Not Found: " + name);
                }
            }

            SaveLoadedUI(ui);
            return (T)ui;
        }

        private GameObject LoadUIPrefab(string name)
        {
            //this.Log("LoadUIPrefab(" + _uiResRoot + name + ")");
            GameObject asset = (GameObject)Resources.Load(_uiResRoot + name);
            return asset;
        }

        private void SetParentRoot<T>(GameObject go) where T : UIPanel
        {
            //Debug.Log(typeof(T).BaseType);
            //Debug.Log(typeof(T));
            if (typeof(T).ToString().Equals("UIFramework.UIPage"))
            {

                go.transform.SetParent(_uiPageTrans, false);
            }
            else if (typeof(T).ToString().Equals("UIFramework.UIWidget"))
            {
                go.transform.SetParent(_uiWidgetTrans, false);
            }
            else if (typeof(T).ToString().Equals("UIFramework.UIWindow"))
            {
                go.transform.SetParent(_uiWindowTrans, false);
                //Debug.Log("typeof(UIWindow)");
            }
        }


        private void SaveLoadedUI(UIPanel ui)
        {
            if (ui != null)
            {
                if (!_loadedPanelDic.ContainsKey(ui.name))
                {
                    _loadedPanelDic.Add(ui.name, ui);
                }
            }
        }

        #endregion

        #region 关闭UI
        public void CloseAllLoadedPanels()
        {
            foreach (var item in _loadedPanelDic)
            {
                if (item.Value.IsOpen)
                    item.Value.Close();
            }
            _uiStack.Clear();
        }

        public UIPanel CloseUI(string uiName)
        {
            UIPanel ui = null;
            if (_loadedPanelDic.TryGetValue(uiName, out ui))
            {
                if (ui.IsOpen)
                {
                    ui.Close();     
                }
            }
            return ui;
        }


        #endregion

        //=======================================================================

        /// <summary>
        /// 进入主Page
        /// 会清空Page堆栈
        /// </summary>
        public void EnterMainPage()
        {
            _pageTrackStack.Clear();
            OpenPageWorker(MainScene, MainPage, null);
            Debug.Log("EnterMainPage()进入主城");
        }


        //=======================================================================
        #region UIPage管理






        #region 暂时封闭
        ///// <summary>
        ///// 当UIPlane关闭时调用
        ///// </summary>
        ///// <param name="name"></param>
        //internal void OnClose(string name)
        //{
        //    if (_curOpenedUIDic.ContainsKey(name))
        //    {
        //        _curOpenedUIDic.Remove(name);
        //    }

        //}

        ///// <summary>
        ///// 当UIPlane打开时调用
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="ui"></param>
        //internal void OnOpen(string name, UIPanel ui)
        //{
        //    if (!_curOpenedUIDic.ContainsKey(name))
        //    {
        //        _curOpenedUIDic.Add(name, ui);
        //    }
        //}

        #endregion

        /// <summary>
        /// 打开Page
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <param name="arg"></param>
        public void OpenPage(string scene, string page, object arg = null)
        {
            Debug.Log("OpenPage(" + scene + "," + page + "," + arg + ")");

            if (_currentPage != null)
            {
                _pageTrackStack.Push(_currentPage);
                Debug.Log("" + _currentPage.name + "," + _currentPage.scene + "[1]");
            }

            OpenPageWorker(scene, page, arg);
        }

        public UIPanel OpenPage(string page, object arg = null)
        {
            //this.OpenPage(MainScene, page, arg);
           return Open<UIPage>(page, arg);
        }


        private void OpenPageWorker(string scene, string page, object arg)
        {
            Debug.Log(string.Format("OpenPageWorker() scene:{0}, page:{1}, arg:{2} ", scene, page, arg));

            string oldScene = SceneManager.GetActiveScene().name;

            _currentPage = new UIPageTrack();
            _currentPage.scene = scene;
            _currentPage.name = page;

            //关闭当前Page时打开的所有UI
            CloseAllLoadedPanels();


            if (oldScene == scene)
            {
                Open<UIPage>(page, arg);
            }
            else
            {
                _sceneLoaded = (sceneName) =>//注册场景加载完成时的回调方法
                {
                    if (sceneName == scene)
                    {
                        //_sceneLoaded 这个回调只监听Page转换，用完即置空
                        _sceneLoaded = null;

                        Open<UIPage>(page, arg);
                    }
                };

                //UILoadingPage.UILoadingArg loadingArg=new UILoadingPage.UILoadingArg();
                //loadingArg.tip = "test";
                //loadingArg.sceneName = scene;
                //loadingArg.bgSpriteIndex = 1;
                //OpenPage("Loading", "UILoadingPage", loadingArg);

                SceneManager.LoadScene(scene);
            }
        }

        /// <summary>
        /// 返回上一个Page
        /// </summary>
        public void GoBackPage()
        {
            Debug.Log("GoBackPage()");
            if (_pageTrackStack.Count > 0)
            {
                var track = _pageTrackStack.Pop();
                OpenPageWorker(track.scene, track.name, null);
            }
            else if (_pageTrackStack.Count == 0)
            {
                EnterMainPage();
            }
        }

        #endregion

        //=======================================================================

        //todo:需要后续处理
        //public UIPage OpenPage(string name, object arg = null)
        //{
        //    this.Log("OpenPage(" + name + "," + arg + ")");
        //    CloseAllLoadedPanels();
        //    UIPage ui = Open<UIPage>(name, arg);
        //    return ui;
        //}


        #region UIWindow管理

        public UIWindow OpenWindow(string name, object arg = null)
        {
            Debug.Log("OpenWindow(" + name + "," + arg + ")");
            UIWindow ui = Open<UIWindow>(name, arg);

            return ui;
        }
        public void Goback()
        {
            if (_uiStack.Count > 0)
            {
                var ui = _uiStack.Pop();
            }
        }

        #endregion

        //=======================================================================

        #region UIWidget管理

        public UIWidget OpenWidget(string name, object arg = null)
        {
            Debug.Log("OpenWidget(" + name + "," + arg + ")");
            UIWidget ui = Open<UIWidget>(name, arg);
            return ui;
        }





        public void Test01()
        {
            foreach (var item in _curOpenedUIDic)
            {
                Debug.Log(item.Key);
            }
        }

        #endregion


        //================================================




        void Update()
        {
            Test_DebugOpenedUI();

        }


        void Test_DebugOpenedUI()
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                foreach (var item in _loadedPanelDic)
                {
                    Debug.Log(item.Key + ":" + item.Value);
                }
            }

        }



       



    }//Class End





}//namespace End



