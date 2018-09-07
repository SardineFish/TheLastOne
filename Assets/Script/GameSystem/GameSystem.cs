﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class GameSystem : Singleton<GameSystem>
{
    public Entity PlayerInControl;
    public Camera MainCamera => GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    void Start()
    {
        if (Application.isPlaying)
            this.NextFrame(() =>
            {
                //Map.Instance.Generate();
            });
    }
    void Update()
    {
        PlayerInputManager.Instance.PlayerInControl = PlayerInControl;

    }
}