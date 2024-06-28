using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class 
        TileRaycaster : Image
    {
        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector2 localPoint;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera,
                out localPoint);

            float radius = rectTransform.rect.width * 0.5f; // Assuming the image is square
            if (localPoint.magnitude <= radius)
            {
                return true; // The point is within the circular bounds
            }

            return false; // The point is outside the circular bounds
        }
    }
}