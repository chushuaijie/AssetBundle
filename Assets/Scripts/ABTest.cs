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
        //    //1.����AB��
        //    AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");

        //    // ����AB����������һ����Դ����õ��˱��AB������Դ�����ʱ�����ֻ�����Լ��İ�����Դ��ʧ

        //    // �������Ĺؼ�֪ʶ-����������ȡ������Ϣ
        //    // ��������
        //    AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        //    // ���������еĹ̶��ļ����ӹ̶��ļ��еõ�������Ϣ
        //    AssetBundleManifest abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //    // �õ�������������
        //    string[] strs = abManifest.GetAllDependencies("model");
        //    for(int i = 0; i < strs.Length; i++) {
        //        Debug.Log(strs[i]);
        //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);

        //    }




        //    // 2.����AB���е���Դ
        //    // ֻʹ�����ּ��� �����ͬ����ͬ������Դ�ֲ���
        //    // �����÷��ͼ��� ������ Typeָ������
        //    //GameObject obj = ab.LoadAsset<GameObject>("Cube");
        //    // lua ��֧�ַ���


        //    GameObject obj = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //    Instantiate(obj);

        //    //GameObject obj1 = ab.LoadAsset("Sphere", typeof(GameObject)) as GameObject;
        //    //Instantiate(obj1);


        //    // AB�������ظ����أ����򱨴�


        //    // �첽����AB�� -> Э��

        //    //StartCoroutine(LoadABRes("model","Cube"));


        //    // ж�����м��ص�AB�� false:ж��AB�������ã�  true��ж�أ��°���ͬʱ���ٴӰ�����ص���Դ
        //    //AssetBundle.UnloadAllAssetBundles(false);
        //    //// ж������
        //    //ab.Unload(false);
        //}
        //IEnumerator LoadABRes(string ABName, string resName) {

        //    // 1.����AB��
        //    AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);

        //    yield return abcr;
        //    // 2.����AB���е���Դ
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
