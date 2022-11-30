using System;
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
    
    private void Awake()
    {
        players = new List<Transform>();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").Select(gameObj => gameObj.transform).ToList();
        Initialize();
    }

    private void Update()
    {
        UpdateFog();
    }
    
    private void UpdateFog()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i].a = 1;
            
            Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);

            float minDistance = Mathf.Min(players.Select(player => Vector3.SqrMagnitude(v - player.position)).ToArray());
            
            // Distance is too far away
            if (minDistance >= radiusSqr) 
                continue;
            
            // Distance is close enough
            float alpha = Mathf.Min(colors[i].a, minDistance / radiusSqr);
            colors[i].a = alpha;
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
