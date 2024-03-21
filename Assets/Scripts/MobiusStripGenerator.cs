using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MobiusStripGenerator : MonoBehaviour
{
    public float radius = 5f;
    public float width = 1f;
    public int segments = 100;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = GenerateMobiusStrip();
    }

    Mesh GenerateMobiusStrip()
    {
        Mesh mesh = new Mesh();

        int verticesCount = segments * 2 * 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 6];

        float uStep = (2 * Mathf.PI) / segments;
        float vStep = width;

        for (int i = 0; i < segments; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                float u = i * uStep;
                float v = (j - 0.5f) * vStep;

                float x = (radius + v * Mathf.Cos(u / 2)) * Mathf.Cos(u);
                float y = (radius + v * Mathf.Cos(u / 2)) * Mathf.Sin(u);
                float z = v * Mathf.Sin(u / 2);

                vertices[i * 4 + j * 2] = new Vector3(x, y, z);
                vertices[i * 4 + j * 2 + 1] = new Vector3(-x, -y, -z); // for the twist

                if (i < segments - 1)
                {
                    triangles[i * 6 + j * 3] = i * 4 + j * 2;
                    triangles[i * 6 + j * 3 + 1] = i * 4 + j * 2 + 2;
                    triangles[i * 6 + j * 3 + 2] = i * 4 + j * 2 + 1;

                    triangles[i * 6 + j * 3 + 3] = i * 4 + j * 2 + 1;
                    triangles[i * 6 + j * 3 + 4] = i * 4 + j * 2 + 2;
                    triangles[i * 6 + j * 3 + 5] = i * 4 + j * 2 + 3;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // to make the lighting work properly

        return mesh;
    }
}
