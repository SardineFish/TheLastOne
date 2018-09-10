using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

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
                Map.Instance?.Generate();
            });
        var vm = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        vm.Follow = PlayerInControl.transform;
        vm.LookAt = PlayerInControl.transform;
    }
    void Update()
    {
        PlayerInputManager.Instance.PlayerInControl = PlayerInControl;

    }
}