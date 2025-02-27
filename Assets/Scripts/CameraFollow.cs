using UnityEngine;
using UnityEngine.UI; // Importar para usar Text

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Text scoreText; 

    private void Update()
    {
        if (scoreText != null) // Evita errores si el Text no está asignado
        {
            scoreText.text = ((int)(target.position.y * 10)).ToString();
        }
    }

    private void LateUpdate()
    {
        if (target.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
