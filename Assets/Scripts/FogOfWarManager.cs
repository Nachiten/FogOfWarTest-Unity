using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    [SerializeField] private GameObject fogOfWarPlane;
    [SerializeField] private LayerMask fogLayer;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float enemyCheckRadiusOffset = -0.5f;
    [SerializeField] private float updateInterval = 0.05f;
    
    private float radiusSqr => radius * radius;

    private List<MeshRenderer> enemies;
    private List<Transform> players;
    
    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").Select(gameObj => gameObj.transform).ToList();
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(gameObj => gameObj.GetComponent<MeshRenderer>()).ToList();
       
        timer = updateInterval;
        
        InitializeFog();
    }

    private float timer;
    
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
            return;
        
        UpdateFog();
        CalculateVisibleEnemies();
        
        timer = updateInterval;
    }
    
    private void CalculateVisibleEnemies()
    {
        foreach (MeshRenderer enemyMeshRenderer in enemies)
        {
            enemyMeshRenderer.enabled = false;

            foreach (Transform _ in players.Where(player =>
                         Vector3.Distance(enemyMeshRenderer.transform.position, player.position) < radius + enemyCheckRadiusOffset))
                enemyMeshRenderer.enabled = true;
        }
    }
    
    private void UpdateFog()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i].a = 1;
            
            Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);

            List<float> alphas = (from player in players 
                select Vector3.SqrMagnitude(v - player.position) 
                into distance 
                where distance < radiusSqr 
                select Mathf.Min(colors[i].a, distance / radiusSqr)).ToList();

            if (alphas.Count > 0)
                colors[i].a = alphas.Min();
        }

        UpdateColor();
    }
    
    private void InitializeFog()
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
