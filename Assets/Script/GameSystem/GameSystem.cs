using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class GameSystem : Singleton<GameSystem>
{
    public Entity PlayerInControl;
    void Update()
    {
        PlayerInputManager.Instance.PlayerInControl = PlayerInControl;
    }
}