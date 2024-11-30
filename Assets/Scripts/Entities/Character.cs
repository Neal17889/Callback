using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    public Queue<Vector2> PositionInfo = new Queue<Vector2>();
}
