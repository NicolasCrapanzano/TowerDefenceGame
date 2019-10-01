using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool m_SuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if(m_SuttingDown)
            {
                Debug.LogWarning("[singleton] instance'" + typeof(T) + "' already destroyed. returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if(m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if(m_Instance == null)
                    {
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + "(Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return m_Instance;
            }
        }

    } //end of instance creation

    private void OnApplicationQuit()
    {
        m_SuttingDown = true;
    }

    private void OnDestroy()
    {
        m_SuttingDown = true;
    }
}
