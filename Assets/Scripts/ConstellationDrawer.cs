using UnityEngine;

public class ConstellationDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;  // ������`�悷��LineRenderer

    public void DrawConstellation(Vector3[] positions)
    {
        if (positions == null || positions.Length < 2)
        {
            Debug.LogWarning("Not enough points to draw a constellation.");
            return;
        }

        // LineRenderer�̒��_����ݒ�
        lineRenderer.positionCount = positions.Length;

        // ���W��ݒ�
        lineRenderer.SetPositions(positions);

        Debug.Log($"Constellation drawn with {positions.Length} points.");
    }
}
