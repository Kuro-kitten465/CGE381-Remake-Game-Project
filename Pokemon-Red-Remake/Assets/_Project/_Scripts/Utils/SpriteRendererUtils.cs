using UnityEngine;

namespace Game.Utils
{
    public class SpriteRendererUtils : MonoBehaviour
    {
        [SerializeField] private Color _debugColor = Color.white;
        [SerializeField, Range(0f, 1f)] private float _adjustAlpha = 1f;
        [SerializeField] private bool _enableDebugger = true;

        private void OnValidate()
        {
            if (_enableDebugger)
            {
                foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>(true))
                {
                    if (spriteRenderer == null) return;

                    var color = _debugColor;
                    color.a = IsEnable();
                    spriteRenderer.color = color;
                }
            }
            else
            {
                foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>(true))
                {
                    if (spriteRenderer == null) return;

                    var color = _debugColor;
                    color.a = IsEnable();
                    spriteRenderer.color = color;
                }
            }
        }

        private float IsEnable() => _enableDebugger switch
        {
            true => _adjustAlpha,
            false => 0
        };
    }
}
