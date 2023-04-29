using System;
using UnityEngine;

namespace Grid
{
    public class GridTile : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        private Color _nonHighlightColor;
        private Color _highlightColor;
        private bool _isSelectable;

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
            if(!_isSelectable) return;
            Debug.Log($"I was clicked! {transform.position}");
        }

        public void OnMouseEnter()
        {
            if(!_isSelectable) return;
            spriteRenderer.color = _highlightColor;
        }

        public void OnMouseExit()
        {
            if(!_isSelectable) return;
            spriteRenderer.color = _nonHighlightColor;
        }
    }
}