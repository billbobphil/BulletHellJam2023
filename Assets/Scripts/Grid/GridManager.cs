using System.Collections.Generic;
using General;
using UnityEngine;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        public int width;
        public int height;
        public GridTile tilePrefab;
        public Camera mainCamera;
        [SerializeField] private bool showIndividualTiles;
        [SerializeField] private bool allowTileSelection;
        public Color normalColor;
        public Color showTilesColorOne;
        public Color showTilesColorTwo;
        public Color tileHighlightColor;
        private bool _hasGridBeenGenerated;
        public BuildPhaseManager buildPhaseManager;

        private Dictionary<Vector2, GridTile> tiles = new();
        
        [ContextMenu("Set Tiles to Alternating")]
        public void SetTilesToAlternating()
        {
            showIndividualTiles = true;
            foreach (KeyValuePair<Vector2, GridTile> tile in tiles)
            {
                SetTileColor(tile.Value);
            }
        }
        
        [ContextMenu("Set Tiles to Normal")]
        public void SetTilesToNormal()
        {
            showIndividualTiles = false;
            foreach (KeyValuePair<Vector2, GridTile> tile in tiles)
            {
                SetTileColor(tile.Value);
            }
        }

        [ContextMenu("Allow Tile Selection")]
        public void AllowTileSelection()
        {
            allowTileSelection = true;
            foreach (KeyValuePair<Vector2, GridTile> tile in tiles)
            {
                ApplyTileSelectionPolicy(tile.Value);
            }
        }

        [ContextMenu("Disallow Tile Selection")]
        public void DisallowTileSelection()
        {
            allowTileSelection = false;
            foreach (KeyValuePair<Vector2, GridTile> tile in tiles)
            {
                ApplyTileSelectionPolicy(tile.Value);
            }
        }
        
        public void GenerateGrid()
        {
            if (_hasGridBeenGenerated)
            {
                return;
            }
            
            _hasGridBeenGenerated = true;

            GameObject gridFolder = new();
            gridFolder.name = "Grid Tiles";
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GridTile tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    tile.name = $"Tile ({x}, {y})";
                    tiles.Add(new Vector2(x, y), tile);
                    tile.SetHighlightColor(tileHighlightColor);
                    tile.gridManager = this;
                    tile.transform.SetParent(gridFolder.transform);

                    SetTileColor(tile);
                    ApplyTileSelectionPolicy(tile);
                }
            }
            
            // Set camera to center of grid
            Vector3 centerOfGrid = GetCenterOfGrid();
            mainCamera.transform.position = new Vector3(centerOfGrid.x, centerOfGrid.y, mainCamera.transform.position.z);
        }
        
        public Vector3 GetCenterOfGrid()
        {
            return new Vector3(width / 2f - .5f, height / 2f - .5f, 0);
        }

        public Vector3 GetBottomLeftOfGrid()
        {
            return new Vector3(0, 0, 0);
        }
        
        public Vector3 GetBottomRightOfGrid()
        {
            return new Vector3(width - 1, 0, 0);
        }

        public Vector3 GetTopRightOfGrid()
        {
            return new Vector3(width - 1, height - 1, 0);
        }
        
        public Vector3 GetTopLeftOfGrid()
        {
            return new Vector3(0, height - 1, 0);
        }

        public void TileClicked(Vector2 tileCoordinates)
        {
            buildPhaseManager.CreateTower(tileCoordinates);
        }

        public void MarkTileAsOccupied(Vector2 tileCoordinates)
        {
            tiles[tileCoordinates].isOccupied = true;
        }

        private void SetTileColor(GridTile tile)
        {
            if (showIndividualTiles)
            {
                float x = tile.transform.position.x;
                float y = tile.transform.position.y;
                
                bool isOffset = x % 2 == 0 && y % 2 != 0 || x % 2 != 0 && y % 2 == 0;
                Color colorToUse = isOffset ? showTilesColorOne : showTilesColorTwo;
                tile.SetTileColor(colorToUse);
            }
            else
            {
                tile.SetTileColor(normalColor);
            }
        }

        private void ApplyTileSelectionPolicy(GridTile tile)
        {
           tile.SetIsSelectable(allowTileSelection);
        }
    }
}