using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class VisualController:MonoBehaviour
{   
    //一个控制器类型，与一个事件一一对应

    
    //获取模型
    protected VisualModel GetModel<T>()
        where T:VisualModel
    {
        return VisualMVC.Instance.GetModel<T>();
    }
    //获取视图
    protected VisualView GetView<T>()
        where T:VisualView
    {
        return VisualMVC.Instance.GetView<T>();
    }

    //注册模型
    protected void RegisterModel(VisualModel model){
        VisualMVC.Instance.RegisterModel(model);
    }
    //注册视图
    protected void RegisterView(VisualView view){
        VisualMVC.Instance.RegisterView(view);
    }
    //注册控制器类型
    protected void RegisterController(string eventName,Type controllerType)
    {
        VisualMVC.Instance.RegisterController(eventName,controllerType);
    }
    //执行系统消息
    public abstract void Execute(object data);
    
    protected void DestorySelf(){
        Destroy(this.gameObject);
    }
}

