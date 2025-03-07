using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform cameraTransform; // Ссылка на камеру
    private Rigidbody rb;
    private bool isGrounded;

 // 🌪 Добавляем переменную для префаба пыли
    public GameObject dustEffectPrefab; 
    private GameObject dustEffectInstance; // Экземпляр эффекта пыли
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Блокируем вращение тела персонажа
        // 🌪 Создаём экземпляр эффекта пыли и привязываем к персонажу
        if (dustEffectPrefab != null)
        {
            dustEffectInstance = Instantiate(dustEffectPrefab, transform.position, Quaternion.identity);
            dustEffectInstance.transform.parent = transform; // Делаем дочерним объектом
            dustEffectInstance.SetActive(false); // Отключаем эффект на старте
        }
    }

    void FixedUpdate()
    {
        // Читаем ввод с клавиатуры
        float moveX = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float moveZ = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);

        // Преобразуем локальные координаты в глобальные относительно камеры
        Vector3 moveDirection = cameraTransform.right * moveX + cameraTransform.forward * moveZ;
        moveDirection.y = 0; // Убираем наклон вверх-вниз

        // Если есть движение, поворачиваем персонажа
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 4f);
        }

        // Движение
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

        // 🌪 Включаем или отключаем эффект пыли в зависимости от движения
        if (dustEffectInstance != null)
        {
            bool isMoving = moveDirection.magnitude > 0.1f && isGrounded;
            dustEffectInstance.SetActive(isMoving);
            // 🌪 Опускаем эффект пыли к ногам персонажа
        if (isMoving)
        {
            dustEffectInstance.transform.position = transform.position - new Vector3(0, 0.5f, 0); // Опускаем на 0.5 вниз
        }
            
        }
    }

    void Update()
    {
        // Прыжок
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;

            // 🌪 Отключаем эффект пыли при прыжке
            if (dustEffectInstance != null)
            {
                dustEffectInstance.SetActive(false);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
             
        }
    }
}
