using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Script made by Ozer
// Feel free to use this script wherever you like with permission
// Supported colliders: BoxCollider2D, CircleCollider2D, PolygonCollider2D, EdgeCollider2D
// Currently unsupported: TilemapCollider2D, CapsuleCollider2D, CompositeCollider2D, any collider used by composites

[ExecuteInEditMode]
public class AreaVisualizer : MonoBehaviour
{
    [Tooltip("Warna dari visualisasi")]
    public Color areaColor = Color.yellow;

    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Destroy(this);
        }

        SceneView.duringSceneGui += OnScene;
    }

    private void OnScene (SceneView scene)
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            print("Hi");
            Collider2D col = Physics2D.OverlapPoint(SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(Input.mousePosition));
            if (col)
            {
                print(col.gameObject.name);
                Selection.activeGameObject = col.gameObject;
            }
        }
    }

    // Kode untuk mempermudah melihat Collider
    private void OnDrawGizmos()
    {
        /*
        // Cek kondisi warna sebelumnya
        Color col = Gizmos.color;
        // Ambil bentuk collider
        Bounds bounds = GetComponent<Collider2D>().bounds;
        // Mengeset warna menjadi kuning transparan
        Gizmos.color = new Color(1, 1, 0, 0.25f);
        // Menggambar persegi sesuai collider
        Gizmos.DrawCube(bounds.center, bounds.size);
        // Mengeset warna menjadi kuning pekat
        Gizmos.color = new Color(1, 1, 0, 1f);
        // Menggambar garis bentuk persegi sesuai collider
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        // Mengembalikan kondisi warna seperti sebelumnya
        Gizmos.color = col;*/
        Collider2D[] cols = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in cols)
        {
            if (col.GetType() == typeof(BoxCollider2D))
            {
                Color temp = Gizmos.color;
                Gizmos.color = areaColor;
                Gizmos.DrawWireCube(col.bounds.center, col.bounds.extents * 2);
                Gizmos.color = new Color(areaColor.r, areaColor.g, areaColor.b, 0.15f);
                Gizmos.DrawCube(col.bounds.center, col.bounds.extents * 2);
                Gizmos.color = temp;
            }
            else if (col.GetType() == typeof(CircleCollider2D))
            {
                Color temp = Gizmos.color;
                Gizmos.color = areaColor;
                Gizmos.DrawWireSphere(col.bounds.center, ((CircleCollider2D)col).radius);
                Gizmos.color = new Color(areaColor.r, areaColor.g, areaColor.b, 0.15f);
                Gizmos.DrawSphere(col.bounds.center, ((CircleCollider2D)col).radius);
                Gizmos.color = temp;
            }
            else if (col.GetType() == typeof(PolygonCollider2D))
            {
                Vector2[] points;
                Color color = areaColor;
                points = ((PolygonCollider2D)col).points;
                color.a = 0.05f;
                Vector2 pos = transform.position;

                for (int i = 0; i < points.Length; i++)
                {
                    points[i] += col.offset;
                }

                for (int i = 1; i < points.Length - 1; i++)
                {
                    Vector3[] verts = new Vector3[]
                    {
                        points[0] + pos,
                        points[0] + pos,
                        points[i] + pos,
                        points[i+1] + pos
                    };

                    Handles.DrawSolidRectangleWithOutline(verts, color, areaColor);
                }
            }
            else if (col.GetType() == typeof(EdgeCollider2D))
            {
                Color temp = Gizmos.color;
                Gizmos.color = areaColor;
                Vector2[] points;
                points = ((EdgeCollider2D)col).points;
                Vector2 pos = transform.position;

                for (int i = 0; i < points.Length; i++)
                {
                    points[i] += col.offset;
                }
                
                for (int i = 0; i < points.Length - 1; i++)
                {
                    Gizmos.DrawLine(points[i] + pos, points[i + 1] + pos);
                }

                Gizmos.color = temp;
            }
        }
    }
}
