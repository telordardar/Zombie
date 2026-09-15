using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;
    public int skor = 0;
    public GameManager gameManager;
    private Vector2 arahGerak;
    private Rigidbody2D rb;

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        rb.MovePosition(rb.position + arahGerak * kecepatan * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            skor += 1;
            Debug.Log("Skor: " + skor);
            gameManager.AmbilKoin();
        }
    }
}