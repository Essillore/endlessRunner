using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ThirdMobiusStripGenerator : MonoBehaviour
{
    public float stripWidth = 1.0f;
    public int radialSegments = 128;
    public int stripSegments = 16;

    private void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateMobiusStrip();
    }

    Mesh GenerateMobiusStrip()
    {
        Mesh mesh = new Mesh();
        mesh.name = "MobiusStrip";

        int vertexCount = (radialSegments + 1) * 2 * 2; // Extra vertices for the twist
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[radialSegments * 6 * 2]; // Double the triangles for the twist

        float radius = 5.0f; // Central radius of the strip
        float halfWidth = stripWidth / 2f;
        float dTheta = (2f * Mathf.PI) / radialSegments; // Angle step
        float dWidth = stripWidth / stripSegments; // Width step

        for (int segment = 0; segment <= radialSegments; segment++)
        {
            float theta = segment * dTheta;
            float cosTheta = Mathf.Cos(theta);
            float sinTheta = Mathf.Sin(theta);

            // Top edge of the strip
            vertices[segment * 2] = new Vector3(radius * cosTheta, halfWidth, radius * sinTheta);
            // Bottom edge of the strip
            vertices[segment * 2 + 1] = new Vector3(radius * cosTheta, -halfWidth, radius * sinTheta);

            // Apply twist by rotating the vertices along the strip's length
            if (segment % 2 == 0) // Even segments
            {
                vertices[segment * 2] = Quaternion.AngleAxis(180.0f * (segment / (float)radialSegments), new Vector3(cosTheta, 0, sinTheta)) * vertices[segment * 2];
                vertices[segment * 2 + 1] = Quaternion.AngleAxis(180.0f * (segment / (float)radialSegments), new Vector3(cosTheta, 0, sinTheta)) * vertices[segment * 2 + 1];
            }
        }

        // Creating triangles
        for (int segment = 0; segment < radialSegments; segment++)
        {
            int startIndex = segment * 6;
            triangles[startIndex] = segment * 2;
            triangles[startIndex + 1] = segment * 2 + 2;
            triangles[startIndex + 2] = segment * 2 + 1;

            triangles[startIndex + 3] = segment * 2 + 1;
            triangles[startIndex + 4] = segment * 2 + 2;
            triangles[startIndex + 5] = segment * 2 + 3;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}