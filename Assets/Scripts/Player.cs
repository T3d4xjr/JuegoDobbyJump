using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 100f; // O incluso 15f, según la velocidad que necesites

    private float movement;
    private Vector3 originalScale;
    private float screenHalfWidth;
    private float screenHalfHeight;

    // Variables para la detección de deslizamientos
    private Vector2 startTouchPosition;
    private bool isSwiping = false;
    public float swipeThreshold = 50f; // Distancia mínima para considerar un deslizamiento

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    private void Start()
    {
        rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);

        // Calcular los límites de la pantalla en unidades del mundo
        screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidth = screenHalfHeight * Screen.width / Screen.height;
    }

    private void Update()
    {
        // Detección de entrada del teclado
        movement = Input.GetAxis("Horizontal") * movementSpeed;

        // Detección de deslizamientos en dispositivos táctiles
        DetectSwipe();

        // Actualizar la escala del jugador según la dirección del movimiento
        if (movement > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (movement < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        CheckIfOutOfScreen();
    }

    private void FixedUpdate()
    {
        Vector2 vel = rb.linearVelocity;
        vel.x = movement;
        rb.linearVelocity = vel;
    }


    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        Vector2 currentTouchPosition = touch.position;
                        Vector2 swipeDelta = currentTouchPosition - startTouchPosition;

                        if (Mathf.Abs(swipeDelta.x) > swipeThreshold)
                        {
                            // Deslizamiento horizontal detectado
                            movement = Mathf.Sign(swipeDelta.x) * movementSpeed;
                            isSwiping = false; // Evitar múltiples detecciones en un solo deslizamiento
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isSwiping = false;
                    movement = 0; // Detener el movimiento cuando se termina el deslizamiento
                    break;
            }
        }
    }

    private void CheckIfOutOfScreen()
    {
        Vector3 playerPosition = transform.position;

        // Verificar si el jugador está fuera de los límites horizontales
        if (playerPosition.x > screenHalfWidth || playerPosition.x < -screenHalfWidth)
        {
            Debug.Log("El jugador ha salido de la pantalla horizontalmente.");
            Die();
        }

        // Verificar si el jugador está por debajo de la pantalla
        if (playerPosition.y < -screenHalfHeight)
        {
            Debug.Log("El jugador ha caído por debajo de la pantalla.");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Reiniciando escena...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
