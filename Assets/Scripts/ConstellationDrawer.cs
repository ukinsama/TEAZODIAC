using UnityEngine;

public class ConstellationDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;  // ¯À‚ğ•`‰æ‚·‚éLineRenderer

    public void DrawConstellation(Vector3[] positions)
    {
        if (positions == null || positions.Length < 2)
        {
            Debug.LogWarning("Not enough points to draw a constellation.");
            return;
        }

        // LineRenderer‚Ì’¸“_”‚ğİ’è
        lineRenderer.positionCount = positions.Length;

        // À•W‚ğİ’è
        lineRenderer.SetPositions(positions);

        Debug.Log($"Constellation drawn with {positions.Length} points.");
    }
}
