using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private float lifetime = 5f;      // Tiempo de vida de la bola de fuego
    [SerializeField] private LayerMask enemyLayer;     // Capa que representa a los enemigos
    [SerializeField] private int Fireball_damage = 0;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance < 1.5f)
            {
                IRecieveDamage enemy = other.GetComponent<IRecieveDamage>();
                if (enemy != null)
                {
                    enemy.RecieveDamage(Fireball_damage);
                }
                Destroy(gameObject);
            }
        }
    }


}



