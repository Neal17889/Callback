using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RayCaster : Singleton<RayCaster>
{
    

    public bool CanTeleport(Vector2 point, Tilemap tilemap)
    {        
        Collider2D collider = Physics2D.OverlapPoint(point);
        bool hasCollider = collider != null;

        Vector3Int cellPosition = tilemap.WorldToCell(point);

        // 检查该位置是否有瓦片
        TileBase tile = tilemap.GetTile(cellPosition);
        bool hasTile = tile != null;

        if (hasCollider || hasTile)
            return false;
        else
            return true;
    }
}
