using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        //GameObject obj = ABMgr.Instance.LoadRes("model", "Cube",typeof(GameObject))     as GameObject;
        //obj.transform.position = Vector3.zero;
        //GameObject obj1 = ABMgr.Instance.LoadRes("model", "Cube") as GameObject;
        //obj1.transform.position = Vector3.up;
        //GameObject obj2 = ABMgr.Instance.LoadRes<GameObject>("model", "Cube") as GameObject;
        //obj2.transform.position = Vector3.down;

        ABMgr.Instance.LoadResAsync("model", "Cube", (obj) =>{
            (obj as GameObject).transform.position = Vector3.up;
        });
        ABMgr.Instance.LoadResAsync<GameObject>("model", "Sphere", (obj) => {
            (obj as GameObject).transform.position = Vector3.down;
        });
        ABMgr.Instance.LoadResAsync("model", "Sphere",typeof(GameObject), (obj) => {
            (obj as GameObject).transform.position = Vector3.down;
        });
        //    //1.加载AB包
        //    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");

        //    // 关于AB包的依赖，一个资源如果用到了别的AB包的资源，这个时候如果只加载自己的包会资源丢失

        //    // 依赖包的关键知识-利用主包获取依赖信息
        //    // 加载主包
        //    AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //    // 加载主包中的固定文件，从固定文件中得到依赖信息
        //    AssetBundleManifest abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //    // 得到依赖包的名字
        //    string[] strs = abManifest.GetAllDependencies("model");
        //    for(int i = 0; i < strs.Length; i++) {
        //        Debug.Log(strs[i]);
        //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);

        //    }




        //    // 2.加载AB包中的资源
        //    // 只使用名字加载 会出现同名不同类型资源分不清
        //    // 建议用泛型加载 或者是 Type指定类型
        //    //GameObject obj = ab.LoadAsset<GameObject>("Cube");
        //    // lua 不支持泛型


        //    GameObject obj = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //    Instantiate(obj);

        //    //GameObject obj1 = ab.LoadAsset("Sphere", typeof(GameObject)) as GameObject;
        //    //Instantiate(obj1);


        //    // AB包不能重复加载，否则报错


        //    // 异步加载AB包 -> 协程

        //    //StartCoroutine(LoadABRes("model","Cube"));


        //    // 卸载所有加载的AB包 false:卸载AB包（常用）  true：卸载ＡＢ包，同时销毁从包里加载的资源
        //    //AssetBundle.UnloadAllAssetBundles(false);
        //    //// 卸载自身
        //    //ab.Unload(false);
        //}
        //IEnumerator LoadABRes(string ABName, string resName) {

        //    // 1.加载AB包
        //    AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);

        //    yield return abcr;
        //    // 2.加载AB包中的资源
        //    AssetBundleRequest abq =  abcr.assetBundle.LoadAssetAsync(resName,typeof(GameObject));
        //    yield return abq;
        //    GameObject obj = abq.asset as GameObject;
        //    Instantiate(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
