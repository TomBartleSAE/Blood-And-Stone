using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
{
    public static T Instance;

    public virtual void Awake()
    {
        Instance = (T)this;
    }
}
