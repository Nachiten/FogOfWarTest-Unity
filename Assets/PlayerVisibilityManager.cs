using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVisibilityManager : MonoBehaviour
{
    private const float radius = 4.5f;
    
    private List<MeshRenderer> enemies;
    private List<Transform> players;
    
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(gameObj => gameObj.GetComponent<MeshRenderer>()).ToList();
        players = GameObject.FindGameObjectsWithTag("Player").Select(gameObj => gameObj.transform).ToList();
    }
    
    private void Update()
    {
        foreach (MeshRenderer enemyMeshRenderer in enemies)
        {
            enemyMeshRenderer.enabled = false;

            foreach (Transform player in players.Where(player =>
                         Vector3.Distance(enemyMeshRenderer.transform.position, player.position) < radius))
                enemyMeshRenderer.enabled = true;
        }
    }
}
