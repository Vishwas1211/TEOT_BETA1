using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ModuleEvent : UnityEvent<object>
{

}

public class ModuleEvent<T, P> : UnityEvent<T,P>
{

}


public class ModuleEvent<T> : UnityEvent<T>
{

}


public static class GlobalEvent 
{


    public static ModuleEvent<Item> useItemEvent = new ModuleEvent<Item>();
    public static ModuleEvent<Item> discardEvent = new ModuleEvent<Item>();
    public static ModuleEvent<float, float> updatePlayerHealth = new ModuleEvent<float, float>();



}
