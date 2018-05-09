using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
public class UnityStoreKitMgr : MonoBehaviour
{

    private static UnityStoreKitMgr _instance;
    public static UnityStoreKitMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("UnityStoreKitMgr");
                _instance = go.AddComponent<UnityStoreKitMgr>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [DllImport("__Internal")]
    private static extern void _goComment();
    public void GoToCommnet()
    {
#if UNITY_IPHONE
        _goComment();
#endif
    }

}
