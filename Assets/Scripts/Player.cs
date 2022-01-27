using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float turningSpeed = 1.0f;

    public Bullet bulletPrefab;

    private float turningDirection;
    private bool IsSpeeding => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    private bool IsTurningLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    private bool IsTurningRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

    private Rigidbody2D rb;

    [HideInInspector] public UnityEvent onPlayerDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (IsTurningLeft)
            turningDirection = 1.0f;
        else if(IsTurningRight)
            turningDirection = -1.0f;
        else
            turningDirection = 0.0f;

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }
    private void FixedUpdate()
    {
        if (IsSpeeding)
            rb.AddForce(transform.up * speed);

        if(turningDirection != 0f)
            rb.AddTorque(turningDirection * turningSpeed);
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Project(transform.up);
    }

    public void ResetPosition()
    {
        rb.transform.position = Vector3.zero;
        rb.transform.eulerAngles = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            onPlayerDied?.Invoke();
        }
    }
}
