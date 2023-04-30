using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    public class GridTile : MonoBehaviour
    {
        public GridManager gridManager;
        public SpriteRenderer spriteRenderer;
        private Color _nonHighlightColor;
        private Color _highlightColor;
        private bool _isSelectable;
        public bool isOccupied;
        
        public GameObject towerPrefab;

        public void SetTileColor(Color tileColor)
        {
            _nonHighlightColor = tileColor;
            spriteRenderer.color = tileColor;
        }
        
        public void SetHighlightColor(Color highlightColor)
        {
            _highlightColor = highlightColor;
        }

        public void SetIsSelectable(bool isSelectable)
        {
            _isSelectable = isSelectable;
        }

        public void OnMouseDown()
        {
            if(!_isSelectable || isOccupied) return;
            
            gridManager.TileClicked(transform.position);
        }

        public void OnMouseEnter()
        {
            if(!_isSelectable || isOccupied) return;
            
            spriteRenderer.color = _highlightColor;
        }

        public void OnMouseExit()
        {
            if(!_isSelectable) return;
            
            spriteRenderer.color = _nonHighlightColor;
        }
    }
}