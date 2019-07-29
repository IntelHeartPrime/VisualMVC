using System.Collections;
using System.Collections.Generic;

public abstract class VisualModel 
{
    //read-only
    public abstract string Name{get;}

    protected void SendEvent(string eveName,object data=null){
        VisualMVC.Instance.SendEvent(eveName,data);
    }
}
