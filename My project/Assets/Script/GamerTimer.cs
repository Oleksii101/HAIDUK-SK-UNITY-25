using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText; // Ссылка на текст таймера
    private float elapsedTime = 0f; // Прошедшее время

    void Update()
    {
        elapsedTime += Time.deltaTime; // Увеличиваем время каждый кадр
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = $"Time: {minutes:D2}:{seconds:D2}"; // Формат MM:SS
    }
}
