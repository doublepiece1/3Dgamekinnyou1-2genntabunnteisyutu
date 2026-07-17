using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Collider playerCollider;
    [SerializeField] Enemy[] enemies;

    void OnEnable()
    {
        foreach (var enemy in enemies)
        {
            enemy.playerCollider = playerCollider;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
