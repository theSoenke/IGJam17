using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid), typeof(GridInformation))]
public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private TileBase _destructibleWallTile, _indestructibleWallTile, _explodedFloorTile;
    [SerializeField]
    private Tilemap _foreground, _background;

    private Grid _grid;
    private GridInformation _gridInfo;

    public const string k_Key = "exploded";

    public Tilemap BackgroundTilemap
    {
        get
        {
            return _background;
        }
    }

    public Tilemap ObstacleTilemap
    {
        get
        {
            return _foreground;
        }
    }

    void Start()
    {
        _grid = GetComponent<Grid>();
        _gridInfo = GetComponent<GridInformation>();
    }

    void Update()
    {
    }

    public void DestroyTile(Vector3 worldPosition)
    {
        //TODO
    }

    public void BuildWall(Vector3 worldPosition)
    {
        var tile = _foreground.GetTile(Vector3Int.FloorToInt(worldPosition));
        if (tile == null)
        {
            _foreground.SetTile(Vector3Int.RoundToInt(worldPosition), _destructibleWallTile);
        }
    }

    private void ExplodeCell(Vector3Int position)
    {
        Debug.Log(_indestructibleWallTile.name);
        if (_foreground.GetTile(position) && _foreground.GetTile(position).name == _indestructibleWallTile.name)
            return;

        _gridInfo.ErasePositionProperty(position, k_Key);
        _gridInfo.SetPositionProperty(position, k_Key, 1);
        foreach (var pos in new BoundsInt(position.x - 1, position.y - 1, position.z, 3, 3, 1).allPositionsWithin)
        {
            if (_foreground.GetTile(pos) != null)
            {
                _background.SetTile(pos, _explodedFloorTile);
            }
        }
        _foreground.SetTile(position, null);

        GameObject.Instantiate(_explosion, _grid.CellToLocalInterpolated(position + new Vector3(0.5f, 0.5f)), Quaternion.identity);
    }
}
