using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Utility
{
    public static void ForEach<T>(this IEnumerable<T> ts, Action<T> callback)
    {
        foreach (var item in ts)
            callback(item);
    }
    public static IEnumerable<T> RandomTake<T>(this IEnumerable<T> list, int count) where T : IWeightedObject
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

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject)
    {
        for(var i = 0; i < gameObject.transform.childCount; i++)
        {
            yield return gameObject.transform.GetChild(i).gameObject;
        }
    }
}