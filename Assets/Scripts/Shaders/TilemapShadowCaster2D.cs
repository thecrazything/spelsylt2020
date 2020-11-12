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
            if (tilemap.GetTile(position) == null)
                continue;

            GameObject shadowCaster = GameObject.Instantiate(shadowPrefab, shadowCasterContainer.transform);
            var corner = tilemap.CellToWorld(new Vector3Int(position.x, position.y, 1));
            shadowCaster.transform.position = new Vector3(corner.x + (cellSize.x / 2), corner.y + (cellSize.y / 2), corner.z);
            shadowCaster.transform.localScale = cellSize;
            shadowCaster.name = "shadow_caster_" + i;
            i++;
        }
    }
}