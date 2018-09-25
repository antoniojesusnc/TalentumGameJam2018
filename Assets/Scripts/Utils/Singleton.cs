using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var temp = FindObjectsOfType<T>();
                if (temp.Length == 0)
                {
                    GameObject go = new GameObject(typeof(T).ToString());
                    _instance = go.AddComponent<T>();

                }
                else if (temp.Length >= 1)
                {
                    _instance = temp[0];
                    if (temp.Length > 1)
                    {
                        Debug.LogWarning("// Multiples instances of " + typeof(T));
                    }
                }
            }
            return _instance;
        } // getPtr
    }
}