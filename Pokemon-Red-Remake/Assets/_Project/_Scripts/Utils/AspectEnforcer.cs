using UnityEngine;

namespace Game.Utils
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode] // Enables script execution in edit mode
    public class AspectEnforcer : MonoBehaviour
    {
        private void Update()
        {
            Camera cam = GetComponent<Camera>();
            float targetAspect = 3f / 2f; // 3:2
            float windowAspect = (float)Screen.width / Screen.height;
            float scaleHeight = windowAspect / targetAspect;

            // This code enforces aspect ratio by adding letterboxing/pillarboxing.
            // It does NOT crop the viewport; it scales the camera view to fit the rect.
            // For true cropping, consider rendering to a RenderTexture of 3:2 and displaying only that.

            if (scaleHeight < 1f)
            {
                // Letterbox: add black bars top/bottom
                Rect rect = cam.rect;
                rect.width = 1f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1f - scaleHeight) / 2f;
                cam.rect = rect;
            }
            else
            {
                // Pillarbox: add black bars left/right
                float scaleWidth = 1f / scaleHeight;
                Rect rect = cam.rect;
                rect.width = scaleWidth;
                rect.height = 1f;
                rect.x = (1f - scaleWidth) / 2f;
                rect.y = 0;
                cam.rect = rect;
            }
        }
    }
}
