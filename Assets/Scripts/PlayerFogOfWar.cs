using UnityEngine;

public class PlayerFogOfWar : MonoBehaviour
{
    // [SerializeField] private GameObject fogOfWarPlane;
    // [SerializeField] private LayerMask fogLayer;
    // [SerializeField] private float radius = 5f;
    // private float radiusSqr => radius * radius;
    //
    // private Mesh mesh;
    // private Vector3[] vertices;
    // private Color[] colors;
    //
    // private void Start()
    // {
    //     Initialize();
    // }
    //
    // private void Update()
    // {
    //     UpdateFog();
    // }
    //
    // public void UpdateFog()
    // {
    //     for (int i = 0; i < vertices.Length; i++)
    //     {
    //         Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
    //         float dist = Vector3.SqrMagnitude(v - transform.position);
    //
    //         // Distance is too far away
    //         if (dist >= radiusSqr) 
    //             continue;
    //         
    //         // Distance is close enough
    //         float alpha = Mathf.Min(colors[i].a, dist / radiusSqr);
    //         colors[i].a = alpha;
    //     }
    //
    //     UpdateColor();
    // }
    //
    // private void Initialize()
    // {
    //     mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
    //     vertices = mesh.vertices;
    //     colors = new Color[vertices.Length];
    //     
    //     for (int i = 0; i < colors.Length; i++)
    //         colors[i] = Color.black;
    //     
    //     UpdateColor();
    // }
    //
    // private void UpdateColor()
    // {
    //     mesh.colors = colors;
    // }
}