using UnityEngine;

public class MoveWithBoundsAndJump : MonoBehaviour
{
    public float velocidad = 5f;      // Velocidad de movimiento horizontal
    public float fuerzaSalto = 10f;  // Fuerza del salto
    private bool puedeSaltar = false; // Indica si el objeto puede saltar

    private Rigidbody2D rb;          // Referencia al Rigidbody2D del objeto

    void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Si el Rigidbody2D no está presente, advertir
        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody2D en el objeto.");
        }
    }

    void Update()
    {
        // Movimiento horizontal
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidad;
        transform.Translate(movimientoHorizontal * Time.deltaTime, 0, 0);

        // Saltar si está permitido
        if (Input.GetKeyDown(KeyCode.W) && puedeSaltar)
        {
            rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
            puedeSaltar = false; // Evitar saltos múltiples
        }

        // Restringir movimiento dentro de los límites de la pantalla
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, -screenBounds.x, screenBounds.x);

        transform.position = currentPosition;
    }

    // Detectar si el objeto está tocando el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Permitir saltar si el objeto está tocando el suelo
        puedeSaltar = true;
    }
}