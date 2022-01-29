using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(MeshCollider))]
public class GeneratePlayerShip : MonoBehaviour
{
    public float length;
    public float width;
    public float height;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private int faceStartIndex;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    //private void OnValidate()
    //{
    //    Generate();
    //}

    private void Generate()
    {
        faceStartIndex = 0;

        var verts = new List<Vector3>();
        var faceIndices = new List<int>();
        var lineIndices = new List<int>();

        //int currentVert;

        AddTri(new Vector3[]
        {
            new Vector3(0.0f, 0.0f, length / 2),
            new Vector3(width / 2, height / 2, -length / 2),
            new Vector3(-width / 2, height / 2, -length / 2),
        }, verts, faceIndices, lineIndices);

        AddTri(new Vector3[]
        {
            new Vector3(0.0f, 0.0f, length / 2),
            new Vector3(-width / 2, -height / 2, -length / 2),
            new Vector3(width / 2, -height / 2, -length / 2),
        }, verts, faceIndices, lineIndices);

        AddTri(new Vector3[]
        {
            new Vector3(0.0f, 0.0f, length / 2),
            new Vector3(-width / 2, height / 2, -length / 2),
            new Vector3(-width / 2, -height / 2, -length / 2),
        }, verts, faceIndices, lineIndices);

        AddTri(new Vector3[]
        {
            new Vector3(0.0f, 0.0f, length / 2),
            new Vector3(width / 2, -height / 2, -length / 2),
            new Vector3(width / 2, height / 2, -length / 2),
        }, verts, faceIndices, lineIndices);

        AddQuad(new Vector3[]
        {
            new Vector3(-width / 2, -height / 2, -length / 2),
            new Vector3(-width / 2, height / 2, -length / 2),
            new Vector3(width / 2, height / 2, -length / 2),
            new Vector3(width / 2, -height / 2, -length / 2),
        }, verts, faceIndices, lineIndices);


        var mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.subMeshCount = 2;
        mesh.SetIndices(faceIndices, MeshTopology.Triangles, 0);
        mesh.SetIndices(lineIndices, MeshTopology.Lines, 1);


        var colliderMesh = new Mesh();
        colliderMesh.SetVertices(verts);
        colliderMesh.SetIndices(faceIndices, MeshTopology.Triangles, 0);
        colliderMesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = colliderMesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void AddTri(Vector3[] quadVerts, List<Vector3> verts, List<int> faceIndices, List<int> wireframeIndices)
    {
        if (quadVerts.Length != 3)
        {
            throw new UnityException("Tri must have 4 verts");
        }

        verts.AddRange(quadVerts);

        faceIndices.Add(faceStartIndex + 0);
        faceIndices.Add(faceStartIndex + 1);
        faceIndices.Add(faceStartIndex + 2);

        wireframeIndices.Add(faceStartIndex + 0);
        wireframeIndices.Add(faceStartIndex + 1);

        wireframeIndices.Add(faceStartIndex + 1);
        wireframeIndices.Add(faceStartIndex + 2);

        wireframeIndices.Add(faceStartIndex + 2);
        wireframeIndices.Add(faceStartIndex + 0);

        faceStartIndex += 3;
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
