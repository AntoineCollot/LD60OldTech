using UnityEngine;

public class MiniGameCamGrid : MonoBehaviour
{
#if UNITY_EDITOR
    Camera cam;

    private void OnDrawGizmos()
    {
        if(cam == null)
            cam = GetComponent<Camera>();

        float camViewSize = cam.orthographicSize * 2;
        Vector2 squareSize = Vector2.one * camViewSize;
        Gizmos.color = Color.cyan;
        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                Vector2 pos = new Vector2(x * camViewSize, y * camViewSize);
                Gizmos.DrawWireCube(pos, squareSize);
            }
        }
    }
#endif
}
