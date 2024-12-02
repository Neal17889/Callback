using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public Queue<Vector3> PositionInfo = new Queue<Vector3>();
}
