using UnityEngine;
using System.Collections.Generic;

namespace Game.Utils
{
    [ExecuteAlways]
    public class LayerHighlighter2D : MonoBehaviour
    {
        [SerializeField] private List<LayerHighlight> _highlights = new();
        [SerializeField] private bool _enable = true;
        [Range(0, 1), SerializeField] private float _alpha;

        private void OnDrawGizmos()
        {
            if (!_enable) return;

            foreach (var highlight in _highlights)
            {
                if (!highlight.Enable) continue;

                DrawForLayers(highlight);
            }
        }

        private void DrawForLayers(LayerHighlight highlight)
        {
            int layerMask = highlight.Layers.value;
            var objects = FindObjectsByType<Transform>(FindObjectsSortMode.None);

            highlight.DebugColor.a = _alpha;
            Gizmos.color = highlight.DebugColor;

            foreach (var obj in objects)
            {
                if (((1 << obj.gameObject.layer) & layerMask) == 0)
                    continue;

                // Try SpriteRenderer bounds
                if (obj.TryGetComponent<SpriteRenderer>(out var sr))
                {
                    DrawBounds(sr.bounds, highlight.DebugColor);
                    continue;
                }

                // Try Collider2D bounds
                if (obj.TryGetComponent<Collider2D>(out var col))
                {
                    DrawBounds(col.bounds, highlight.DebugColor);
                }
            }
        }

        private void DrawBounds(Bounds b, Color c)
        {
            Gizmos.color = c;
            Gizmos.DrawCube(b.center, b.size);
        }
    }

    [System.Serializable]
    public class LayerHighlight
    {
        public LayerMask Layers;
        public Color DebugColor = Color.yellow;
        public bool Enable = true;
    }
}