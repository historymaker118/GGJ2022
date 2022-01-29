using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeometry : MonoBehaviour
{
    public MeshRenderer _meshRenderer;
    public MeshFilter _meshFilter;

    public GameObject box;

    public void Generate(int sides, int rings, float ringDepth, float radius)
    {
        GenerateObstacles(sides, rings, ringDepth, radius);
        GenerateTubeMesh(sides, rings, ringDepth, radius);
    }

    public void GenerateObstacles(int sides, int rings, float ringDepth, float radius)
    {
        var txt =
@"
B_____ ______
_B____ ______
__B___ ______
___B__ ______
____B_ ______
_____B ______
B_____ ______
_B____ ______
__B___ ______
___B__ ______
____B_ ______
_____B ______
B_____ ______
_B____ ______
__B___ ______
___B__ ______
____B_ ______
_____B ______
B_____ ______
_B____ ______
__B___ ______
___B__ ______
____B_ ______
_____B ______
B_____ ______
_B____ ______
__B___ ______
___B__ ______
____B_ ______
_____B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
B__B__ ______
_B__B_ ______
__B__B ______
______ ______
";
        var ringText = txt.Trim().Split('\n');

        Debug.Log("ringText: " + ringText.ToString());
        
        var angle = 2 * Mathf.PI / sides;

        for (int line = 0; line < ringText.Length; line++)
        {
            var sections = ringText[line].Split(' ');
            Debug.Log("sections: " + sections[0]);

            var inside = sections[0];
            var outside = sections[1];

            for (int face = 0; face < sides; face++)
            {
                var faceAngle = angle * face + angle / 2.0f;
                switch (inside[face])
                {
                    case 'B':
                        var o = Instantiate(
                            box,
                            new Vector3(
                                Mathf.Cos(faceAngle) * radius,
                                Mathf.Sin(faceAngle) * radius,
                                line * ringDepth + (ringDepth / 2.0f)
                            ),
                            Quaternion.AngleAxis(Mathf.Rad2Deg * faceAngle, Vector3.forward)
                        );
                        break;
                    case '_':
                    default:
                        break;
                }
            }

        }
    }

    //public int sides = 6;
    //public int rings = 32;

    //public float ringDepth = 8.0f;
    //public float radius = 3.0f;

    // Start is called before the first frame update
    public void GenerateTubeMesh(int sides, int rings, float ringDepth, float radius)
    {
        var mesh = new Mesh();

        var angle = 2 * Mathf.PI / sides;

        var vertices = new List<Vector3>();
        var surfaceIndices = new List<int>();
        var wireframeIndices = new List<int>();
        var quadStart = 0;

        for (int ring = 0; ring < rings; ring++)
        {
            for (int side = 0; side < sides; side++)
            {
                var startAngle = angle * side;
                var endAngle = angle * (side + 1);
                vertices.Add(new Vector3(Mathf.Cos(startAngle) * radius, Mathf.Sin(startAngle) * radius, ring * ringDepth));
                vertices.Add(new Vector3(Mathf.Cos(startAngle) * radius, Mathf.Sin(startAngle) * radius, (ring + 1) * ringDepth));
                vertices.Add(new Vector3(Mathf.Cos(endAngle) * radius, Mathf.Sin(endAngle) * radius, (ring + 1) * ringDepth));
                vertices.Add(new Vector3(Mathf.Cos(endAngle) * radius, Mathf.Sin(endAngle) * radius, ring * ringDepth));


                surfaceIndices.Add(quadStart);
                surfaceIndices.Add(quadStart + 1);
                surfaceIndices.Add(quadStart + 2);
                surfaceIndices.Add(quadStart);
                surfaceIndices.Add(quadStart + 2);
                surfaceIndices.Add(quadStart + 3);

                surfaceIndices.Add(quadStart);
                surfaceIndices.Add(quadStart + 2);
                surfaceIndices.Add(quadStart + 1);
                surfaceIndices.Add(quadStart);
                surfaceIndices.Add(quadStart + 3);
                surfaceIndices.Add(quadStart + 2);

                // TODO WT: Add wireframe indices.
                wireframeIndices.Add(quadStart);
                wireframeIndices.Add(quadStart + 1);
                wireframeIndices.Add(quadStart + 2);
                wireframeIndices.Add(quadStart + 3);
                wireframeIndices.Add(quadStart);

                quadStart += 4;
            }
        }

        mesh.subMeshCount = 2;
        mesh.SetVertices(vertices);
        mesh.SetIndices(surfaceIndices, MeshTopology.Triangles, 0);
        mesh.SetIndices(wireframeIndices, MeshTopology.Lines, 1);

        _meshFilter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
