using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    public int xsize, ysize;
    private Vector3[] vertices;
    private Mesh mesh;
    private MeshRenderer meshRender;
    private void Awake()
    {
        Generate();
        //StartCoroutine(Generate());
    }


    void Start()
    {
        //Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black ;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

    }
    private void Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
       
        GetComponent<MeshFilter>().mesh = mesh =new Mesh();
        meshRender = GetComponent<MeshRenderer>();
        mesh.name = "Procedural Grid";
        // vertices = new Vector3[10];
        vertices = new Vector3[xsize * ysize];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        int[] Triangles = new int[(xsize - 1) * (ysize - 1) * 2 * 3];
        Vector2[] UV = new Vector2[vertices.Length];
        for (int i = 0; i < ysize; i++)
        {
            for (int j = 0; j < xsize; j++)
            {
                vertices[i * xsize + j] = new Vector3(j, i, 0);
                UV[i] = new Vector2((float)j/xsize, (float)i/ysize);
            }
        }
        //设置顶点索引
        for (int i = 0; i < ysize - 1; i++)
        {
            for(int j = 0; j < xsize - 1; j++)
            {
                int index = i * 9 * 6 + j * 6;
                Triangles[index] = i * xsize + j + xsize;
                Triangles[index + 1] = i * xsize + j;
                Triangles[index + 2] = i * xsize + j + 1;
                Triangles[index + 3] = i * xsize + j + xsize + 1;
                Triangles[index + 4] = i * xsize + j + xsize;
                Triangles[index + 5] = i * xsize + j + 1;
                tangents[i] = tangent;
                int k = index + xsize + 1;
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = Triangles;
        mesh.RecalculateNormals();
        mesh.uv = UV;
        mesh.tangents = tangents;
        //绑定贴图

    }
}
