using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Package.View
{
    public class PackUIManager : MonoBehaviour
    {
        public Canvas PackageCanvas;
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                rayCast(Input.mousePosition);
            }             
        }

        private void rayCast(Vector3 scrennPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(scrennPos);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.tag == "Package")
                    PackageCanvas.planeDistance = 1;
            }
        }

        public void closePackUI()
        {
            PackageCanvas.planeDistance = 0;
        }
    }
}
