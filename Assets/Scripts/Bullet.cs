using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 400f;
    [SerializeField] private float maxLifeTime = 3.0f;
    
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(gameObject, maxLifeTime);
    }
}
