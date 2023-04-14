using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base <T> where T:new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
    public Base()
    {
        Init();
    }
    public virtual void Init() { 

    }
}
public class BaseMonoBehaviour<T> : MonoBehaviour where T : BaseMonoBehaviour<T>
{
    public static T _Instance;
    public virtual void Awake()
    {
        if (_Instance == null)
            _Instance = this as T;
    }
}
     