using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FourthMobiusStripGenerator : MonoBehaviour
{
    public float stripRadius = 5f;
    public float stripWidth = 1f;
    public int radialSegments = 128;
    public int stripSegments = 16;

    void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateMobiusStrip();
    }

    Mesh GenerateMobiusStrip()
    {
        Mesh mesh = new Mesh();
        mesh.name = "MobiusStrip";

        Vector3[] vertices = new Vector3[(radialSegments + 1) * 2];
        int[] triangles = new int[radialSegments * 6];
        Vector2[] uv = new Vector2[vertices.Length];

        float dTheta = (2f * Mathf.PI) / radialSegments;
        float halfWidth = stripWidth * 0.5f;
        int vertIndex = 0;
        int triIndex = 0;

        for (int i = 0; i <= radialSegments; i++)
        {
            float theta = i * dTheta;
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);
            float twistAngle = (theta * 180f) / Mathf.PI; // Convert to degrees for Quaternion.Euler

            // Positioning the two vertices of this segment
            Vector3 vertex1 = new Vector3(cosTheta * stripRadius, -halfWidth, sinTheta * stripRadius);
            Vector3 vertex2 = new Vector3(cosTheta * stripRadius, halfWidth, sinTheta * stripRadius);

            // Applying the twist by rotating around the forward axis (which is the direction of the strip)
            Quaternion twist = Quaternion.Euler(0f, twistAngle, 0f);
            vertices[vertIndex] = twist * vertex1;
            vertices[vertIndex + 1] = twist * vertex2;

            // Setting UVs
            uv[vertIndex] = new Vector2(i / (float)radialSegments, 0);
            uv[vertIndex + 1] = new Vector2(i / (float)radialSegments, 1);

            // Creating triangles
            if (i < radialSegments)
            {
                triangles[triIndex] = vertIndex;
                triangles[triIndex + 1] = vertIndex + 2;
                triangles[triIndex + 2] = vertIndex + 1;
                triangles[triIndex + 3] = vertIndex + 1;
                triangles[triIndex + 4] = vertIndex + 2;
                triangles[triIndex + 5] = vertIndex + 3;

                triIndex += 6;
            }

            vertIndex += 2;
        }
        Debug.Log("Vertices count: " + mesh.vertices.Length);
        Debug.Log("Triangles count: " + mesh.triangles.Length);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}