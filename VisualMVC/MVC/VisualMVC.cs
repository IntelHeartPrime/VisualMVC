using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualMVC:SingletonMVC<VisualMVC>
{
    //存储MVC
    public List<string> models=new List<string>();
    public List<string> views=new List<string>();
    public List<string> commdMap=new List<string>();

    //名字-模型
    public Dictionary<string,VisualModel> Models=new Dictionary<string,VisualModel>();
    //名字-视图
    public Dictionary<string,VisualView> Views=new Dictionary<string,VisualView>();
    //事件名称-控制器"类型"
    public Dictionary<string,Type> CommandMap=new Dictionary<string,Type>();

    //注册
    public void RegisterModel(VisualModel model){
        Models[model.Name]=model;
        this.models.Add(model.Name);
    }

    public void RegisterView(VisualView view){
        Views[view.Name]=view;
        this.views.Add(view.name);
    }

    public void RegisterController(string eventName,Type controllerType){
        CommandMap[eventName]=controllerType;
        this.commdMap.Add(eventName);
    }
    
    
    //获取
    public VisualModel GetModel<T>()
        where T:VisualModel
    {
        foreach (VisualModel m in Models.Values)
        {
            if(m is T){
                return m;
            }
        }
        return null;
    }
    
    public VisualView GetView<T>()
        where T:VisualView
    {
        foreach (VisualView v in Views.Values)
        {
            if(v is T){
                return v;
            }
        }
        return null;
    }

    //发送事件
    public void SendEvent(string eventName,object data=null){
        //优先执行控制器，再执行模型

        //控制器响应逻辑
        if(CommandMap.ContainsKey(eventName)){
            Type t=CommandMap[eventName];
            
            //实例化一个空对象，并为其绑定脚本
            GameObject commandObject = new GameObject(eventName);
            /******************
            //反射
            //Controller c=Activator.CreateInstance(t) as Controller;
            *****************/

            commandObject.AddComponent(t);
            commandObject.transform.parent=GameObject.FindGameObjectWithTag("Commands").transform;
            commandObject.SendMessage("Execute",data);

            /******
            //控制器执行
            c.Execute(data);
            ******/
        }

        //视图响应逻辑
        foreach (VisualView v in Views.Values)
        {
            if(v.AttationEvents.Contains(eventName)){
                //视图响应事件
                v.HandleEvent(eventName,data);
            }
        }
    }
}

