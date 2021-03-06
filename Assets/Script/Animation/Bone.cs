﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Bone : MonoBehaviour
{
    public Vector3 InitialVector = new Vector3(1, 0, 0);
    public float Length = 1;
    public float Width = 0.1f;
    public float MaxRotationSpeed = 180;
    public bool Edit = true;
    public Bone[] SubBones = new Bone[0];
    public bool _showAsActive = false;
    public Quaternion InitialRotation;

    public bool AngularLimit = false;
    public bool SpeedLimit = false;
    public AngularRange AngularLimitX = new AngularRange(-180, 180);
    public AngularRange AngularLimitY = new AngularRange(-180, 180);
    public AngularRange AngularLimitZ = new AngularRange(-180, 180);

    Quaternion lastRotation;
    
    public Vector3 DirectionVector
    {
        get
        {
            return transform.rotation * InitialVector;
        }
    }

    public Matrix4x4 BoneToWorldMatrix
    {
        get
        {
            return transform.localToWorldMatrix * Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.right, InitialVector));
        }
    }

    // Use this for initialization
    void Start()
    {
        lastRotation = transform.localRotation;
        mesh = new Mesh();
    }

    [ExecuteInEditMode]
    // Update is called once per frame
    void Update()
    {
        if (AngularLimit)
            ApplyAngularLimit();
        //Debug.DrawLine(transform.position, transform.position + DirectionVector * 10);
        if (SubBones != null && SubBones.Length > 0 && Edit)
        {
            if(!SubBones[0])
            {
                var list = SubBones.ToList();
                list.RemoveAt(0);
                SubBones = list.ToArray();
                return;
            }
            InitialVector = SubBones[0].transform.localPosition;
            //this.InitialVector = Quaternion.Inverse(transform.rotation) * (SubBones[0].transform.position - transform.position);
            this.Length = InitialVector.magnitude;
        }
    }

    [ExecuteInEditMode]
    private void FixedUpdate()
    {
    }

    public void AddSubBone(Bone bone)
    {
        Array.Resize(ref SubBones, SubBones.Length + 1);
        SubBones[SubBones.Length - 1] = bone;
    }

    public void ApplyAngularLimit()
    {
        if(SpeedLimit)
        {
            if (Quaternion.Angle(lastRotation, transform.localRotation) / Time.deltaTime > MaxRotationSpeed)
            {
                transform.localRotation = Quaternion.Lerp(lastRotation, transform.localRotation, MaxRotationSpeed / (Quaternion.Angle(lastRotation, transform.localRotation) / Time.deltaTime));
                lastRotation = transform.localRotation;
            }
        }
        if (AngularLimit)
        {
            var angle = transform.localRotation.eulerAngles;
            angle.x = AngularLimitX.Limit(MathUtility.MapAngle(angle.x));
            angle.y = AngularLimitY.Limit(MathUtility.MapAngle(angle.y));
            angle.z = AngularLimitZ.Limit(angle.z);
            transform.localRotation = Quaternion.Euler(angle);

        }
    }

    public Quaternion ApplyAngularLimit(Quaternion rotation, Space space)
    {
        if (AngularLimit)
        {
            if (space == Space.World)
            {
                if (transform.parent)
                {
                    rotation = Quaternion.Inverse(transform.parent.rotation) * rotation;
                }
            }
            var angle = rotation.eulerAngles;
            angle.x = AngularLimitX.Limit(MathUtility.MapAngle(angle.x));
            angle.y = AngularLimitY.Limit(MathUtility.MapAngle(angle.y));
            angle.z = AngularLimitZ.Limit(MathUtility.MapAngle(angle.z));
            rotation = Quaternion.Euler(angle);
            if (space == Space.World)
            {
                if (transform.parent)
                {
                    rotation = transform.parent.rotation * rotation;
                }
            }
        }
        return rotation;
    }

    Mesh mesh;//= new Mesh();
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        mesh.Clear();
        var V = new Vector3[]
        {
                new Vector3(0, 0, 0),
                new Vector3(Width, Width, 0),
                new Vector3(Width, 0, Width),
                new Vector3(Width, -Width, 0),
                new Vector3(Width, 0, -Width),
                new Vector3(1,0,0),
        };
        for (var i = 0; i < V.Length; i++)
        {
            V[i] *= Length;
        }
        mesh.vertices = V;
        //mesh.triangles = new int[] { 0, 1, 5, 0, 2, 5, 0, 3, 5, 0, 4, 5 };
        mesh.triangles = new int[]
        {
            2,1,0,
            3,2,0,
            4,3,0,
            1,4,0,
            1,2,5,
            2,3,5,
            3,4,5,
            4,1,5,
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        Color color;
        if (_showAsActive)
            ColorUtility.TryParseHtmlString("#F44336", out color);
        else
            ColorUtility.TryParseHtmlString("#66CCFF", out color);
        _showAsActive = false;
        Gizmos.color = Color.cyan;
        Gizmos.DrawMesh(mesh, transform.position,
            Quaternion.FromToRotation(Vector3.right, transform.rotation * InitialVector));
    }


}
