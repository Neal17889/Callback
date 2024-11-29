using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    class UIElement
    {
        public bool Cache;
        public string Resources;
        public GameObject Instance;
    }

    private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();

    public UIManager()
    {
        //this.UIResources.Add(typeof(UITest), new UIElement() { Resources = "UI/UITest", Cache = true });
        
    }

    ~UIManager() { }

    public T Show<T>()
    {
        //SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Open);
        Type type = typeof(T);
        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if (info.Instance != null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if (prefab == null)
                {
                    return default(T);
                }
                info.Instance = (GameObject)GameObject.Instantiate(prefab);
            }
            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }

    //public T Get<T>()
    //{
    //    //SoundManager.Instance.PlaySound("ui_open");
    //    Type type = typeof(T);
    //    if (this.UIResources.ContainsKey(type))
    //    {
    //        UIElement info = this.UIResources[type];
    //        if (info.Instance != null)
    //        {
    //            info.Instance.SetActive(true);
    //            return info.Instance.GetComponent<T>();
    //        }
    //        else
    //        {
    //            Debug.LogError("info.Instance == null");
    //        }
            
    //    }
    //    return default(T);
    //}

    public void Close(Type type)
    {
        //SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Close);
        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = UIResources[type];
            if (info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }

    public void Close<T>()
    {
        this.Close(typeof(T));
    }

}
