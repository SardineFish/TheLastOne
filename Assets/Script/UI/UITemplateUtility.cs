using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class UITemplateUtility
{
    public static object GetValueByPath(this GameObject obj, string path)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        var component = obj.GetComponent(paths[0]);
        object value = component;
        for (var i = 1; i < paths.Length; i++)
        {
            value = value.GetType().GetField(paths[i]).GetValue(value);
        }
        return value;
    }

    public static object SetValueByPath(this GameObject obj, string path, object value)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        var component = obj.GetComponent(paths[0]);
        object currentObj = component;
        FieldInfo current = component.GetType().GetField(paths[1]);
        for (var i = 2; i < paths.Length; i++)
        {
            currentObj = current.GetValue(currentObj);
            current = currentObj.GetType().GetField(paths[i]);
        }
        current.SetValue(currentObj, value);
        return value;
    }

    public static object GetValueByPath(object obj, string path)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        object value = obj;
        for(var i = 0; i < paths.Length; i++)
        {
            value = value.GetType().GetField(paths[i]).GetValue(value);
        }
        return value;
    }

    public static object SetValueByPath(object obj,string path, object value)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        object currentObj = obj;
        FieldInfo current = obj.GetType().GetField(paths[0]);
        for(var i=1;i<paths.Length;i++)
        {
            currentObj = current.GetValue(currentObj);
            current = currentObj.GetType().GetField(paths[i]);
        }
        current.SetValue(currentObj, value);
        return value;
    }
}