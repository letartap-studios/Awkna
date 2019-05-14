using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //public GameObject crate;
    public static bool destroyobject(GameObject obj)
    {
        Destroy(obj);

        /*if (obj == crate)
        {
            return true;
        }
        else
        {
            return false;
        }*/
        return true;
    }
}
