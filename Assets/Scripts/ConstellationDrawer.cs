using UnityEngine;

public class ConstellationDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;  // 星座を描画するLineRenderer

    public void DrawConstellation(Vector3[] positions)
    {
        if (positions == null || positions.Length < 2)
        {
            Debug.LogWarning("Not enough points to draw a constellation.");
            return;
        }

        // LineRendererの頂点数を設定
        lineRenderer.positionCount = positions.Length;

        // 座標を設定
        lineRenderer.SetPositions(positions);

        Debug.Log($"Constellation drawn with {positions.Length} points.");
    }
}
