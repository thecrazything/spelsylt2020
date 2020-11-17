using System;
using UnityEngine;

public class ComponentUtil
{
    public static T RequireComponent<T>(GameObject gameObject)
    {
        ArgumentNotNull(gameObject);
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            throw new ArgumentNullException(typeof(T).Name);
        }
        return component;
    }

    public static void ArgumentNotNull(object obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(obj.GetType().Name);
        }
    }
}
