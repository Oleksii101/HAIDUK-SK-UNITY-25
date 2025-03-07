using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinUI : MonoBehaviour
{
    public TMP_Text coinText; // UI-счётчик монет
    public GameObject winPanel; // Панель победы
      public AudioSource audioSource; // 🎵 Аудиоплеер
    public AudioClip winSound; // 🎶 Мелодия победы

    void Start()
    {
        UpdateCoinUI(0); // Обновляем UI на старте
        winPanel.SetActive(false); // Скрываем панель победы
    }

    public void UpdateCoinUI(int collectedCoins)
    {
        coinText.text = "Coins: " + collectedCoins + "/20";
    }

    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время

         Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

         // 🔊 Проигрываем мелодию победы
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }
    }

    public void RestartGame()
{
    
    Time.timeScale = 1f; // Возвращаем скорость игры
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

     Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
}

}
