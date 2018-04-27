using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace Assets.Script.Package.Manager
{
    public class GetEmptyGrid : MonoBehaviour
    {
        private Transform[] Grids;

        void Start()
        {
            Grids = new Transform[transform.childCount];
            for(int i=0;i<transform.childCount;i++)
            {
                Grids[i] = transform.GetChild(i).transform;
            }
        }

        //get the nearest grid transform
        public Transform GetEmpty()
        {
            for (int i = 0; i < Grids.Length; i++)
                if (Grids[i].childCount == 0)
                    return Grids[i];
            return null;
        }
    }
}
