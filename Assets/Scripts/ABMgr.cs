using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ֪ʶ��
/// 1.AB����ص�API
/// 2.����ģʽ
/// 3.ί��->Lambda���ʽ
/// 4.Э��
/// 5.�ֵ�
/// </summary>
public class ABMgr : MonoSingleton<ABMgr> {
    // AB�������� ���ⲿ������Ľ�����Դ����

    // ����
    private AssetBundle mainAB = null;
    //��������ȡ�õ� �����ļ�
    private AssetBundleManifest manifest = null;

    //AB�������ظ����� �ظ����ػᱨ��
    // �ֵ� �����洢���ع���AB��



    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();


    /// <summary>
    /// AB �����·�� �����޸�
    /// </summary>
    /// 
  
    private string PathUrl {
        get {
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// ������
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
        //��ȡ�����������Ϣ
        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);
        foreach (string str in strs) {
            // �жϰ��Ƿ���ع�
            if (!abDic.ContainsKey(str)) {
                ab = AssetBundle.LoadFromFile(PathUrl + str);
                abDic.Add(str, ab);
            }
        }
        // ������Դ��Դ��
        // ���û�м��ع� �ټ���
        if (!abDic.ContainsKey(abName)) {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);

        }


    }
    // ͬ������ ��ָ������

    public Object LoadRes(string abName,string resName) {
        //����AB�� 
        LoadAB(abName);
        // ������Դ
        Object obj = abDic[abName].LoadAsset(resName);
        if(obj is GameObject) {
            return Instantiate(obj);
        }
        else {
            return obj;
        }
        //��������
        //���������еĹؼ����� ��ȡ������
        // ����������

        //����Ŀ���

    }

    // ͬ������ ����typeָ������
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
    // ͬ������ ���ݷ���ָ������

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

    // �첽���صķ���
    // ������첽���� AB����û��ʹ���첽����
    // ��AB���м�����Դʱ ʹ���첽

    // ���������첽������Դ
    public void LoadResAsync(string abName,string resName,UnityAction<Object> callback) {
        StartCoroutine(ReallyLoadResAsync(abName,resName,callback));
    } 
    private IEnumerator ReallyLoadResAsync(string abName,string resName,UnityAction<Object> callback) {
        LoadAB(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //�첽���ؽ�����,ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ�� 
        if (abr.asset is GameObject) {
           callback(Instantiate(abr.asset));
        }
        else {
            callback(abr.asset);
        }
    }
    // ����Type�첽������Դ

    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callback) {
        StartCoroutine(ReallyLoadResAsync(abName, resName,type, callback));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callback) {
        LoadAB(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        //�첽���ؽ�����,ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ�� 
        if (abr.asset is GameObject) {
            callback(Instantiate(abr.asset));
        }
        else {
            callback(abr.asset);
        }
    }
    // ���ݷ����첽������Դ
    public void LoadResAsync<T>(string abName, string resName, UnityAction<Object> callback) where T:Object {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callback));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName , UnityAction<Object> callback)where T:Object {
        LoadAB(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //�첽���ؽ�����,ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ�� 
        if (abr.asset is GameObject) {
            callback(Instantiate(abr.asset));
        }
        else {
            callback(abr.asset);
        }
    }



    // ������ж��
    public void UnLoad(string abName) {
        if(abDic.ContainsKey(abName)) {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    // ���а�ж��
    public void ClearAB() {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}


