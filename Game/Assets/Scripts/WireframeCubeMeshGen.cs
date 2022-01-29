using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WireframeCubeMeshGen : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public float depth = 1.0f;
    public float height = 1.0f;
    public float baseWidth = 1.0f;
    public float topWidth = 0.6f;

    private int faceStartIndex;

    private void Awake()
    {
        Generate();
    }

    // Start is called before the first frame update
    void Generate()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        faceStartIndex = 0;

        var verts = new List<Vector3>();
        var faceIndices = new List<int>();
        var wireframeIndices = new List<int>();

        float baseTinyOffset = 0.1f;

        AddQuad(new Vector3[]{
            new Vector3(-baseWidth / 2, baseTinyOffset, -depth / 2),
            new Vector3(-topWidth / 2, height, -depth / 2),
            new Vector3(topWidth / 2, height, -depth / 2),
            new Vector3(baseWidth / 2, baseTinyOffset, -depth / 2)
        }, verts, faceIndices, wireframeIndices);

        AddQuad(new Vector3[]{
            new Vector3(-topWidth / 2, height, -depth / 2),
            new Vector3(-topWidth / 2, height, depth / 2),
            new Vector3(topWidth / 2, height, depth / 2),
            new Vector3(topWidth / 2, height, -depth / 2),
        }, verts, faceIndices, wireframeIndices);

        AddQuad(new Vector3[]{
            new Vector3(baseWidth / 2, baseTinyOffset, -depth / 2),
            new Vector3(topWidth / 2, height, -depth / 2),
            new Vector3(topWidth / 2, height, depth / 2),
            new Vector3(baseWidth / 2, baseTinyOffset, depth / 2)
        }, verts, faceIndices, wireframeIndices);

        AddQuad(new Vector3[]{
            new Vector3(-baseWidth / 2, baseTinyOffset, depth / 2),
            new Vector3(-topWidth / 2, height, depth / 2),
            new Vector3(-topWidth / 2, height, -depth / 2),
            new Vector3(-baseWidth / 2, baseTinyOffset, -depth / 2)
        }, verts, faceIndices, wireframeIndices);


        var mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.subMeshCount = 2;
        mesh.SetIndices(faceIndices, MeshTopology.Triangles, 0);
        mesh.SetIndices(wireframeIndices, MeshTopology.Lines, 1);

        var colliderMesh = new Mesh();
        colliderMesh.SetVertices(verts);
        colliderMesh.SetIndices(faceIndices, MeshTopology.Triangles, 0);
        colliderMesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = colliderMesh;
    }

    //private void OnValidate()
    //{
    //    Generate();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddQuad(Vector3[] quadVerts, List<Vector3> verts, List<int> faceIndices, List<int> wireframeIndices)
    {
        if (quadVerts.Length != 4)
        {
            throw new UnityException("Quad must have 4 verts");
        }

        verts.AddRange(quadVerts);
        
        faceIndices.Add(faceStartIndex + 0);
        faceIndices.Add(faceStartIndex + 1);
        faceIndices.Add(faceStartIndex + 2);

        faceIndices.Add(faceStartIndex + 0);
        faceIndices.Add(faceStartIndex + 2);
        faceIndices.Add(faceStartIndex + 3);

        wireframeIndices.Add(faceStartIndex + 0);
        wireframeIndices.Add(faceStartIndex + 1);

        wireframeIndices.Add(faceStartIndex + 1);
        wireframeIndices.Add(faceStartIndex + 2);

        wireframeIndices.Add(faceStartIndex + 2);
        wireframeIndices.Add(faceStartIndex + 3);

        wireframeIndices.Add(faceStartIndex + 3);
        wireframeIndices.Add(faceStartIndex + 0);

        faceStartIndex += 4;
    }
}
