using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    [SerializeField] private GameObject fogOfWarPlane;
    [SerializeField] private LayerMask fogLayer;
    [SerializeField] private float radius = 5f;
    private float radiusSqr => radius * radius;

    private List<Transform> players;
    
    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").Select(gameObj => gameObj.transform).ToList();
        Initialize();
        timer = updateInterval;
    }

    [SerializeField]
    private float updateInterval;

    private float timer;
    
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
            return;
        
        UpdateFog();
        timer = updateInterval;
    }
    
    private void UpdateFog()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i].a = 1;
            
            Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);

            List<float> alphas = new();

            foreach (Transform player in players)
            {
                float distance = Vector3.SqrMagnitude(v - player.position);
                
                if (distance < radiusSqr)
                    alphas.Add(Mathf.Min(colors[i].a, distance / radiusSqr));
            }

            if (alphas.Count > 0)
                colors[i].a = alphas.Min();
        }

        UpdateColor();
    }
    
    private void Initialize()
    {
        mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        
        for (int i = 0; i < colors.Length; i++)
            colors[i] = Color.black;
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        mesh.colors = colors;
    }
}
