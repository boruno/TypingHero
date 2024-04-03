using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Connection : MonoBehaviour
{
    public Tilemap lettersTilemap;
    public Tilemap grassTilemap;
    public Tilemap ItemsTilemap;
    public Tilemap KeyboardTilemap;
    public Tilemap Keyboard2Tilemap;
    private Vector3Int currentTilePosition;
    private List<Vector3Int> revealedItems = new List<Vector3Int>();

    private void GetNextTile()
    {
        var bounds = lettersTilemap.cellBounds;
        var xMin = bounds.xMin;
        var xMax = bounds.xMax;
        var yMin = bounds.yMin;
        var yMax = bounds.yMax;
        while (!lettersTilemap.HasTile(currentTilePosition))
        {
            currentTilePosition.x += 1;
            if (currentTilePosition.x > xMax)
            {
                currentTilePosition.x = xMin;
                currentTilePosition.y -= 1;
            }
            if (currentTilePosition.y < yMin)
            {
                currentTilePosition = new Vector3Int(-1, -1, -1);
            }
        }
    } 
    
    void Start()
    {
        currentTilePosition = new Vector3Int(lettersTilemap.cellBounds.xMin, lettersTilemap.cellBounds.yMax - 1, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CheckAndRemoveTile("p", "P");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            CheckAndRemoveTile("r", "R");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            CheckAndRemoveTile("i", "I");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            CheckAndRemoveTile("v", "V");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CheckAndRemoveTile("e", "E");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            CheckAndRemoveTile("t", "T");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            CheckAndRemoveTile("o", "O");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            CheckAndRemoveTile("l", "L");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            CheckAndRemoveTile("g", "G");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            CheckAndRemoveTile("k", "K");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            CheckAndRemoveTile("a", "A");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            CheckAndRemoveTile("d", "D");
        }
        else if (Input.GetMouseButtonDown(1)) // Right mouse button clicked
        {
            CheckAndRemoveItem();
        }
    }

    void CheckAndRemoveTile(string keyPressed, string tileName)
    {
        GetNextTile();
        var letterTile = lettersTilemap.GetTile(currentTilePosition);

        if (!letterTile) return;

        var rightKeyPressed = letterTile.name == tileName;
        if (rightKeyPressed)
        {
            lettersTilemap.SetTile(currentTilePosition, null);
            grassTilemap.SetTile(currentTilePosition, null);

            // If there is an item at the current position, add it to the list of revealed items
            if (ItemsTilemap.GetTile(currentTilePosition) != null)
            {
                revealedItems.Add(currentTilePosition);
            }
        }
    }

    void CheckAndRemoveItem()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = ItemsTilemap.WorldToCell(mouseWorldPos);

        // If the clicked position is in the list of revealed items, remove the item
        if (revealedItems.Contains(coordinate))
        {
            ItemsTilemap.SetTile(coordinate, null);
            revealedItems.Remove(coordinate);

            // Find the position of the 'f' tile in the KeyboardTilemap
            Vector3Int keyboardTilePosition = FindTilePositionInTilemap(KeyboardTilemap, "F");

            // If the 'f' tile exists, delete it
            if (keyboardTilePosition != new Vector3Int(-1, -1, -1))
            {
                KeyboardTilemap.SetTile(keyboardTilePosition, null);
            }
        }
    }

    Vector3Int FindTilePositionInTilemap(Tilemap tilemap, string tileName)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(pos);
            if (tile != null && tile.name == tileName)
            {
                return pos;
            }
        }
        return new Vector3Int(-1, -1, -1); // Return an invalid position if the tile is not found
    }
}