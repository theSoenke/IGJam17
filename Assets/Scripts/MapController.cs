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
        DestroyTile(new Vector3(3, 33));
    }

    void Update()
    {
    }

    public void DestroyTile(Vector3 worldPosition)
    {
        var gridPos = Vector3Int.FloorToInt(worldPosition);
        var tile = _foreground.GetTile(gridPos);

        if (tile == _indestructibleWallTile)
            return;

        ExplodeCell(gridPos);

        var directions = new[] {new Vector3Int(0,1,0),
                                new Vector3Int(0,-1,0),
                                new Vector3Int(1,0,0),
                                new Vector3Int(-1,0,0)};

        foreach (var dir in directions)
        {
            var candidatePos = gridPos + dir;
            gridPos += dir;
            var candidateTile = _foreground.GetTile(candidatePos);
            if (candidateTile != _indestructibleWallTile)
                ExplodeCell(candidatePos);

            //TODO: kill zombies in circle with radius = 1u
        }
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
        if (_foreground.GetTile(position) && _foreground.GetTile(position) == _indestructibleWallTile)
            return;

        _gridInfo.ErasePositionProperty(position, k_Key);
        _gridInfo.SetPositionProperty(position, k_Key, 1);
        if (_foreground.GetTile(position) != null)
        {
            _background.SetTile(position, _explodedFloorTile);
        }
        _foreground.SetTile(position, null);

        GameObject.Instantiate(_explosion, _grid.CellToLocalInterpolated(position + new Vector3(0.5f, 0.5f)), Quaternion.identity);
    }
}
