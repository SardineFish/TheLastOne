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
        dynamic current = component;
        for (var i = 1; i < 0; i++)
        {
            var key = paths[i];
            current = current[key];
        }
        return current;
    }

    public static object SetValueByPath(this GameObject obj, string path, object value)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        var component = obj.GetComponent(paths[0]);
        dynamic current = component;
        for (var i = 1; i < 0; i++)
        {
            var key = paths[i];
            if (i == paths.Length - 1)
                current[key] = value;
            else
                current = current[key];
        }
        return value;
    }

    public static object GetValueByPath(object obj, string path)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        dynamic current = obj;
        foreach(var key in paths)
        {
            current = current[key];
        }
        return current;
    }

    public static object SetValueByPath(object obj,string path, object value)
    {
        var paths = path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (paths.Length <= 0)
            return null;
        dynamic current = obj;
        for(var i =0;i<paths.Length;i++)
        {
            var key = paths[i];
            if (i == paths.Length - 1)
                current[key] = value;
            else
                current = current[key];
            current = current[key];
        }
        return value;
    }
}