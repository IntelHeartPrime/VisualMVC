using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    //prefab
    GameObject m_prefab;

    //set
    List<GameObject> m_objects=new List<GameObject>();

    //name 
    public string Name{
        get{return m_prefab.name;}
    }
    //construct method
    public SubPool(GameObject prefab){
        this.m_prefab=prefab;
    }

    //取得对象
    public GameObject Spawn(){
        GameObject go=null;

        foreach (GameObject obj in m_objects)
        {
            if(!obj.activeSelf){
                go=obj;
                break;
            }
        }
        //if length(m_objects)=0 || All objects are active
        if(go==null){
            go=GameObject.Instantiate<GameObject>(m_prefab);
            m_objects.Add(go);
        }

        go.SetActive(true);

        //发送消息
        go.SendMessage("OnSpawn",SendMessageOptions.DontRequireReceiver);
        return go;
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        if(Contains(go)){
            go.SendMessage("OnUnSpawn",SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //回收该池所有对象
    public void UnSpawnAll(){
        foreach (GameObject item in m_objects)
        {
            if(item.activeSelf){
                Unspawn(item);
            }
        }
    }
    //是否包含对象
    public bool Contains(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
