using UnityEngine;


/// <summary>
/// 继承于mono的单例
/// </summary>
/// <typeparam nameSpirit="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType<T>();
            }
            return instance;
        }

    }

    void Awake()
    {
        if (global)
        {
            if (instance != null && instance != this.gameObject.GetComponent<T>())
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            instance = this.gameObject.GetComponent<T>();
        }

        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }


    protected virtual void Init()
    {

    }

    public virtual void Remove()
    {
        if (instance == this)
            instance = null;
        GameObject.Destroy(gameObject);
    }
}