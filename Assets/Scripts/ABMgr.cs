using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 知识点
/// 1.AB包相关的API
/// 2.单例模式
/// 3.委托->Lambda表达式
/// 4.协程
/// 5.字典
/// </summary>
public class ABMgr : MonoSingleton<ABMgr> {
    // AB包管理器 让外部更方便的进行资源加载

    // 主包
    private AssetBundle mainAB = null;
    //依赖包获取用的 配置文件
    private AssetBundleManifest manifest = null;

    //AB包不能重复加载 重复加载会报错
    // 字典 用来存储加载过的AB包



    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();


    /// <summary>
    /// AB 包存放路径 方便修改
    /// </summary>
    /// 
  
    private string PathUrl {
        get {
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// 主包名
    /// </summary>
    private string MainABName {
        get {
#if UNITY_IOS
    return "IOS";
#elif UNITY_ANDROID
    return "Android";
#else
            return "PC";
#endif
        }
    }

    //private void Start() {
    //    Object obj =LoadRes("model", "Cube");
    //    Instantiate(obj);
    //}

    public void LoadAB(string abName) {

        if (mainAB == null) {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包相关信息
        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);
        foreach (string str in strs) {
            // 判断包是否加载过
            if (!abDic.ContainsKey(str)) {
                ab = AssetBundle.LoadFromFile(PathUrl + str);
                abDic.Add(str, ab);
            }
        }
        // 加载资源来源包
        // 如果没有加载过 再加载
        if (!abDic.ContainsKey(abName)) {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);

        }


    }
    // 同步加载 不指定类型

    public Object LoadRes(string abName,string resName) {
        //加载AB包 
        LoadAB(abName);
        // 加载资源
        Object obj = abDic[abName].LoadAsset(resName);
        if(obj is GameObject) {
            return Instantiate(obj);
        }
        else {
            return obj;
        }
        //加载主包
        //加载主包中的关键配置 获取依赖包
        // 加载依赖包

        //加载目标包

    }

    // 同步加载 根据type指定类型
    public Object LoadRes(string abName,string resName,System.Type type) {
        LoadAB(abName);

        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject) {
            return Instantiate(obj);
        }
        else {
            return obj;
        }
    }
    // 同步加载 根据泛型指定类型

    public T LoadRes<T>(string abName, string resName) where T:Object {
        LoadAB(abName);

        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject) {
            return Instantiate(obj);
        }
        else {
            return obj;
        }
    }

    // 异步加载的方法
    // 这里的异步加载 AB包并没有使用异步加载
    // 从AB包中加载资源时 使用异步

    // 根据名字异步加载资源
    public void LoadResAsync(string abName,string resName,UnityAction<Object> callback) {
        StartCoroutine(ReallyLoadResAsync(abName,resName,callback));
    } 
    private IEnumerator ReallyLoadResAsync(string abName,string resName,UnityAction<Object> callback) {
        LoadAB(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //异步加载结束后,通过委托 传递给外部 外部来使用 
        if (abr.asset is GameObject) {
           callback(Instantiate(abr.asset));
        }
        else {
            callback(abr.asset);
        }
    }
    // 根据Type异步加载资源

    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callback) {
        StartCoroutine(ReallyLoadResAsync(abName, resName,type, callback));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callback) {
        LoadAB(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        //异步加载结束后,通过委托 传递给外部 外部来使用 
        if (abr.asset is GameObject) {
            callback(Instantiate(abr.asset));
        }
        else {
            callback(abr.asset);
        }
    }
    // 根据泛型异步加载资源
    public void LoadResAsync<T>(string abName, string resName, UnityAction<Object> callback) where T:Object {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callback));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName , UnityAction<Object> callback)where T:Object {
        LoadAB(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //异步加载结束后,通过委托 传递给外部 外部来使用 
        if (abr.asset is GameObject) {
            callback(Instantiate(abr.asset));
        }
        else {
            callback(abr.asset);
        }
    }



    // 单个包卸载
    public void UnLoad(string abName) {
        if(abDic.ContainsKey(abName)) {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    // 所有包卸载
    public void ClearAB() {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}


