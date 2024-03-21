using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FifthMobiusStripGenerator : MonoBehaviour
{
    public int segments = 100;
    public float radius = 2f;
    public float width = 0.1f;

    private void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateMobiusStrip(segments, radius, width);
    }

    Mesh GenerateMobiusStrip(int segments, float radius, float width)
    {
        Mesh mesh = new Mesh();

        int verticesCount = segments * 2 * 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 6];

        float angleStep = 360f / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            float nextAngle = angleStep * (i + 1) * Mathf.Deg2Rad;
            float halfWidth = width / 2;

            Vector3 base1 = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 base2 = new Vector3(Mathf.Cos(nextAngle), 0, Mathf.Sin(nextAngle)) * radius;

            int baseIndex = i * 4;
            vertices[baseIndex] = base1 + (transform.up * halfWidth);
            vertices[baseIndex + 1] = base2 + (transform.up * halfWidth);
            vertices[baseIndex + 2] = base1 - (transform.up * halfWidth);
            vertices[baseIndex + 3] = base2 - (transform.up * halfWidth);

            // Adjust the up vector based on the angle to introduce the twist
            if (i % 2 == 0)
            {
                vertices[baseIndex] += transform.forward * halfWidth;
                vertices[baseIndex + 2] -= transform.forward * halfWidth;
            }
            else
            {
                vertices[baseIndex + 1] += transform.forward * halfWidth;
                vertices[baseIndex + 3] -= transform.forward * halfWidth;
            }

            int triIndex = i * 6;
            triangles[triIndex] = baseIndex;
            triangles[triIndex + 1] = baseIndex + 2;
            triangles[triIndex + 2] = baseIndex + 1;
            triangles[triIndex + 3] = baseIndex + 1;
            triangles[triIndex + 4] = baseIndex + 2;
            triangles[triIndex + 5] = baseIndex + 3;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        Debug.Log("Vertices count: " + mesh.vertices.Length);
        Debug.Log("Triangles count: " + mesh.triangles.Length);

        return mesh;
    }
}