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

        // ����λ���Ƿ�����Ƭ
        TileBase tile = tilemap.GetTile(cellPosition);
        bool hasTile = tile != null;

        if (hasCollider || hasTile)
            return false;
        else
            return true;
    }
}
