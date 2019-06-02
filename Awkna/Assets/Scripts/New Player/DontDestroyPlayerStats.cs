using UnityEngine;

public class DontDestroyPlayerStats : MonoBehaviour
{

    private static DontDestroyPlayerStats _instance = null;

    public static DontDestroyPlayerStats Instance { get { return _instance; } }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }


        //GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);

        //if (objs.Length > 1)
        //{
        //    Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);
    }
}
