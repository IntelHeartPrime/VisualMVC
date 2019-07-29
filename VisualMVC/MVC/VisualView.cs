using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualView : MonoBehaviour
{   
    //视图标识
    public abstract string Name{get;}
    
    //事件列表
    public List<string> AttationEvents=new List<string>();
    
    //事件处理
    public abstract void HandleEvent(string eventName,object data);
    //获取模型
    protected VisualModel GetModel<T>()
        where T:VisualModel
    {
            return VisualMVC.Instance.GetModel<T>();
    }

    //发送消息
    protected void SendEvent(string eventName,object data=null){
        VisualMVC.Instance.SendEvent(eventName,data);
    }


}
