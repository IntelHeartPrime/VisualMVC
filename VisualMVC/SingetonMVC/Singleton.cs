﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMVC<T>:MonoBehaviour
    where T:MonoBehaviour
{
    private static T m_instance=null;

    public static T Instance{
        get{return m_instance;}
    }

    // private 无法被继承
    //virtual 可以被重写
    protected virtual void Awake() {
        m_instance=this as T;
    }
}
