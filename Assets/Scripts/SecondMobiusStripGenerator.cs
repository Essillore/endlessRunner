using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SecondMobiusStripGenerator : MonoBehaviour
{
    public float stripWidth = 1f;
    public int radialSegments = 128;
    public int stripSegments = 16;

    private void Start()
    {
        GetComponent<MeshFilter>().mesh = CreateMobiusStrip(stripWidth, radialSegments, stripSegments);
    }

    Mesh CreateMobiusStrip(float width, int radialSegments, int stripSegments)
    {
        Mesh mesh = new Mesh();

        int verticesCount = (radialSegments + 1) * (stripSegments + 1);
        Vector3[] vertices = new Vector3[verticesCount];
        Vector2[] uv = new Vector2[verticesCount];
        int[] triangles = new int[radialSegments * stripSegments * 6];

        float radius = 1f;
        float stripHalfWidth = width * 0.5f;
        float radialStep = 2f * Mathf.PI / radialSegments;
        float stripStep = 1f / stripSegments;

        for (int stripIdx = 0; stripIdx <= stripSegments; stripIdx++)
        {
            for (int radialIdx = 0; radialIdx <= radialSegments; radialIdx++)
            {
                int idx = stripIdx * (radialSegments + 1) + radialIdx;

                float t = stripIdx * stripStep;
                float angle = radialIdx * radialStep;
                float u = angle / (2f * Mathf.PI);
                float v = t;

                float halfTwistAngle = Mathf.PI * t; // Half twist over the length of the strip
                float x = Mathf.Cos(angle) * (radius + stripHalfWidth * Mathf.Cos(halfTwistAngle));
                float z = Mathf.Sin(angle) * (radius + stripHalfWidth * Mathf.Cos(halfTwistAngle));
                float y = stripHalfWidth * Mathf.Sin(halfTwistAngle);

                vertices[idx] = new Vector3(x, y, z);
                uv[idx] = new Vector2(u, v);

                if (stripIdx < stripSegments && radialIdx < radialSegments)
                {
                    int baseIdx = stripIdx * radialSegments + radialIdx;
                    int a = idx;
                    int b = idx + radialSegments + 1;
                    int c = idx + 1;
                    int d = idx + radialSegments + 2;

                    triangles[baseIdx * 6] = a;
                    triangles[baseIdx * 6 + 1] = b;
                    triangles[baseIdx * 6 + 2] = c;
                    triangles[baseIdx * 6 + 3] = c;
                    triangles[baseIdx * 6 + 4] = b;
                    triangles[baseIdx * 6 + 5] = d;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
