using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    /// <summary>
    /// The Singleton instance and ensures that only one instance exists in the scene while making it persistent across scene transitions in Unity. If another instance is detected, it is destroyed, and if no instance exists, the current GameObject becomes the Singleton instance.
    /// </summary>
    protected virtual void Awake()
    {
        if(instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
