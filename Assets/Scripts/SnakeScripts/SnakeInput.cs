using UnityEngine;

namespace SnakeScripts
{
    public class SnakeInput : MonoBehaviour
    {
        public Vector2 GetDirectionClick(Vector2 headPosition)
        {
            Vector2 clickPosition = Input.mousePosition;

            clickPosition = Camera.main!.ScreenToViewportPoint(clickPosition);
            clickPosition.y = 1f;
            clickPosition = Camera.main!.ViewportToWorldPoint(clickPosition);

            var direction = new Vector2(clickPosition.x - headPosition.x, clickPosition.y - headPosition.y);

            return direction;
        }
    }
}