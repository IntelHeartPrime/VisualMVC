using UnityEngine;
using System.Collections;

public interface IReusable
{
    //取出调用
    void OnSpawn();

    //回收调用
    void OnUnSpawn();
}