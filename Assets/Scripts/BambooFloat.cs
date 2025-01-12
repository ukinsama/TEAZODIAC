using UnityEngine;

public class BambooFloat : MonoBehaviour
{
    public float floatSpeed = 1.0f;
    public float floatRange = 0.1f;
    [SerializeField] public Vector3 startPosition;
    public Transform sphere; // ← 球体オブジェクトのTransform

    private Vector3 normal; // 球体表面の法線ベクトル

    void Start()
    {
        startPosition = transform.position;
        if (sphere == null)
        {
            Debug.LogWarning("Sphere is not assigned to BambooFloat");
            return;
        }

        // 初期位置の法線ベクトルを計算
        normal = (startPosition - sphere.transform.position).normalized;

        // 接線ベクトルを計算
        Vector3 tangent = Vector3.Cross(normal, Vector3.up).normalized;
        if (tangent == Vector3.zero)
        {
            tangent = Vector3.Cross(normal, Vector3.forward).normalized;
        }

        // 竹の向きを初期化
        transform.rotation = Quaternion.LookRotation(tangent, normal);
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}