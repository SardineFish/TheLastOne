using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDocumented
{
    string Name { get; }
    string DisplayName { get; }
    string Description { get; }
    Sprite Icon { get; }
}