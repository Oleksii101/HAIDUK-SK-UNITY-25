using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    public GameObject welcomePanel;

    void Start()
    {
        // ✅ Делаем курсор видимым при запуске игры
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        

        welcomePanel.SetActive(false); // Отключаем панель
        Time.timeScale = 1f; // Запускаем игру

        // ❌ Прячем курсор после начала игры
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
{
    if (welcomePanel.activeSelf) // Если панель активна, курсор включён
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

}
