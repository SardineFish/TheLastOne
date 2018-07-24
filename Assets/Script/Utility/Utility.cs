using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
    public static void ForEach<T>(this IEnumerable<T> ts, Action<T> callback)
    {
        foreach (var item in ts)
            callback(item);
    }
    public static IEnumerable<T> WeightedRandomTake<T>(this IEnumerable<T> list, int count) where T : IWeightedObject
    {
        var source = list.ToArray();
        var idxMap = source.Select((item, idx) => idx).ToArray();
        var weightSum = source.Sum(item => item.Weight);

        for(var i = 0; i < count; i++)
        {
            var totalWeight = weightSum;
            for (var j = 0; j < source.Length - i; j++)
            {
                if (UnityEngine.Random.value < (float)source[idxMap[j]].Weight / (float)totalWeight)
                {
                    yield return source[idxMap[j]];

                    // Update the total weight of all rest items.
                    weightSum -= source[idxMap[j]].Weight;
                    // Put the last item to the position where the item was taken.
                    idxMap[j] = idxMap[source.Length - i - 1];
                    break;
                }
                else
                    totalWeight -= source[idxMap[j]].Weight;
            }
        }
    }

    public static IEnumerable<T> RandomTake<T>(this IEnumerable<T> list,int count)
    {
        var source = list.ToArray();

        for (var i = 0; i < count; i++)
        {
            var idx = UnityEngine.Random.Range(0, source.Length - i);
            yield return source[idx];
            source[idx] = source[count - i - 1];
        }
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject)
    {
        for(var i = 0; i < gameObject.transform.childCount; i++)
        {
            yield return gameObject.transform.GetChild(i).gameObject;
        }
    }

    public static IEnumerable<T> NotNull<T>(this IEnumerable<T> source)
    {
        foreach(var item in source)
        {
            if (item != null)
                yield return item;
        }
    }
    
    public static TResult Merge<TResult,Tkey,TElement>(this IGrouping<Tkey,TElement> group, TResult mergeTarget, Func<TElement,TResult,TResult> mergeFunc)
    {
        foreach(var element in group)
        {
            mergeTarget = mergeFunc(element, mergeTarget);
        }
        return mergeTarget;
    }

    public static GameObject Instantiate(this UnityEngine.Object self, GameObject original, Scene scene)
    {
        var obj = UnityEngine.Object.Instantiate(original);
        SceneManager.MoveGameObjectToScene(obj, scene);
        return obj;
    }

    public static GameObject Instantiate(GameObject original, Scene scene)
    {
        var obj = UnityEngine.Object.Instantiate(original);
        SceneManager.MoveGameObjectToScene(obj, scene);
        return obj;
    }
    public static GameObject Instantiate(GameObject original, GameObject parent)
    {
        var obj = Instantiate(original, parent.scene);
        obj.transform.parent = parent.transform;
        return obj;
    }

    public static GameObject Instantiate(GameObject original, GameObject parent, Vector3 relativePosition,Quaternion relativeRotation)
    {
        var obj = Instantiate(original, parent);
        obj.transform.localPosition = relativePosition;
        obj.transform.localRotation = relativeRotation;
        return obj;
    }

    public static void ClearChildren(this GameObject self)
    {
        self.GetChildren().ForEach((child)=>
        {
            child.ClearChildren();
            GameObject.Destroy(child);
        });
    }

    public static void ClearChildImmediate(this GameObject self)
    {
        while (self.transform.childCount > 0)
        {
            var obj = self.transform.GetChild(0).gameObject;
            obj.ClearChildImmediate();
            GameObject.DestroyImmediate(obj);
        }
    }
}