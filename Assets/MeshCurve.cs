using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshCurve : MonoBehaviour
{
    public AnimationCurve curveShape = AnimationCurve.Linear(0, 0, 1, 0);
    public float heightStrength = 2f;
    public float fadeDistance = 1f;

    private Mesh mesh;
    private Vector3[] originalVerts; // store the original mesh vertices
    private float minX, maxX, minZ, maxZ, length, centerZ;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        // Store original vertices so we can recalculate from a base shape each frame
        originalVerts = mesh.vertices;

        // Precompute mesh bounds info
        minX = Mathf.Infinity;
        maxX = Mathf.NegativeInfinity;
        minZ = Mathf.Infinity;
        maxZ = Mathf.NegativeInfinity;

        foreach (var v in originalVerts)
        {
            if (v.x < minX) minX = v.x;
            if (v.x > maxX) maxX = v.x;
            if (v.z < minZ) minZ = v.z;
            if (v.z > maxZ) maxZ = v.z;
        }

        length = maxX - minX;
        centerZ = (minZ + maxZ) / 2f;
    }

    void Update()
    {
        Vector3[] verts = new Vector3[originalVerts.Length];

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = originalVerts[i];

            // Position along road (0..1)
            float tLength = (verts[i].x - minX) / length;

            // Distance from center (0 = center, fadeDistance = max effect drop-off)
            float distFromCenter = Mathf.Abs(verts[i].z - centerZ);
            float fade = Mathf.Clamp01(1f - (distFromCenter / fadeDistance));

            // Vertical offset
            float heightOffset = curveShape.Evaluate(tLength) * heightStrength * fade;

            verts[i].y += heightOffset;
        }

        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
