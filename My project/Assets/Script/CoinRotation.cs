using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 30f; // Скорость вращения

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Вращение вокруг оси Z
    }
}
