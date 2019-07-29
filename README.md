# VisualMVC
可视化MVC架构
![Image text](https://github.com/intelHEART/VisualMVC/blob/master/MVC.png)
搭建要求：
1.创建游戏对象 MVC，models，Commands并添加上同名标签

2.为三个对象添加脚本 NotDelete ，确保切换场景三者也不被销毁
MVC的作用，提供了MVC的基础方法（注册M，V，C，发送事件）
                                    可在其属性区中看到当前注册的所有 M，V，C,有利于梳理逻辑
·       models作用，奖Model对象作为其子物体，方便整理
·       Commands作用，当发送事件，有Controller对象相应后，会自动生成一个以事件为名称的对象，作为Commands的子物体，该物体将负责执行Controller的逻辑，执行后可销毁（Execute函数后加上 DestorySelf函数即可）
           
3.MVC的所有节点需要搭建在游戏的第一个场景中

4.每一个场景中需要一个RegisterManager节点，主要用于控制当前场景节点/MVC的控制与销毁
第一个场景中注册需要写在Start函数中
其他场景中注册需要写在Awaker函数中

5.在非第一个场景中进行开发需要注意以下几点：
可以搭建临时的MVC节点方便测试，测试跨场景流程时需要将临时节点设置为不可用，打包时需要将临时节点删除

6.在不再需要某些MVC资源时，为避免占用内存过大，可进行代码处理销毁/取消注册

![Image text](https://github.com/intelHEART/VisualMVC/blob/master/Picture1.png)



书写要点：
调用VisualMVC中函数或者属性，需要使用

VisualMVC.Instance.xxxx

核心原理：
1.使用单例模式开发MVC类，在awake时创建Visual.Instance
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

public class VisualMVC:SingletonMVC<VisualMVC>

2.使用反射与广播执行Controller中函数
    public void SendEvent(string eventName,object data=null){
        //优先执行控制器，再执行模型

        //控制器响应逻辑
        if(CommandMap.ContainsKey(eventName)){
            Type t=CommandMap[eventName];
            
            //实例化一个空对象，并为其绑定脚本
            GameObject commandObject = new GameObject(eventName);

            commandObject.AddComponent(t);
            commandObject.transform.parent=GameObject.FindGameObjectWithTag("Commands").transform;
            commandObject.SendMessage("Execute",data);
}

