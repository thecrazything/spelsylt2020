using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapShadowCaster2D : MonoBehaviour
{
    public Vector2 tileScale = new Vector2(0.64f, 0.64f);
    public GameObject shadowPrefab;
    public void Start()
    {
        var cellSize = transform.parent.gameObject.GetComponent<Grid>().cellSize;

        var tilemap = GetComponent<Tilemap>();
        GameObject shadowCasterContainer = GameObject.Find("shadow_casters");
        int i = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(position);
            if (tile == null)
                continue;
            if (tile.name != "T_shadow_tile" && tile.name != "T_half_shadow_tile" && tile.name != "T_half_shadow_tile_lower")
            {
                throw new ArgumentException("Tile must be of type T_shadow_tile or T_half_shadow_tile"); 
            }
            GameObject shadowCaster = GameObject.Instantiate(shadowPrefab, shadowCasterContainer.transform);
            var corner = tilemap.CellToWorld(new Vector3Int(position.x, position.y, 1));
            if (tile.name == "T_shadow_tile")
            {
                shadowCaster.transform.position = new Vector3(corner.x + (cellSize.x / 2), corner.y + (cellSize.y / 2), corner.z);
                shadowCaster.transform.localScale = cellSize;
                shadowCaster.name = "shadow_caster_" + i;
            }
            else if (tile.name == "T_half_shadow_tile_lower")
            {
                Vector3 halfSize = new Vector3(cellSize.x, cellSize.y / 2, cellSize.z);
                shadowCaster.transform.position = new Vector3(corner.x + (halfSize.x / 2), corner.y + (halfSize.y / 2f), corner.z);
                shadowCaster.transform.localScale = halfSize;
                shadowCaster.name = "shadow_caster_half_" + i;
            }
            else
            {
                Vector3 halfSize = new Vector3(cellSize.x, cellSize.y / 2, cellSize.z);
                shadowCaster.transform.position = new Vector3(corner.x + (halfSize.x / 2), corner.y + (halfSize.y * 1.5f), corner.z);
                shadowCaster.transform.localScale = halfSize;
                shadowCaster.name = "shadow_caster_half_" + i;
            }
            i++;
        }
    }
}