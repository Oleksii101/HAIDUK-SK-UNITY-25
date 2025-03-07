using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinsCollected = 0; // Счётчик монет
    public int totalCoinsToWin = 20; // Сколько нужно собрать для победы
    public AudioClip coinSound; // 🔊 Файл звука монеты
    private AudioSource audioSource;
    public GameObject coinEffectPrefab; // 🎇 Префаб эффекта сбора монеты
    private CoinUI coinUI; // Ссылка на UI-счётчик монет

    void Start()
    {
        // Проверяем, есть ли уже AudioSource, если нет – добавляем
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ищем объект с CoinUI в сцене
        coinUI = FindFirstObjectByType<CoinUI>(); // Новый метод

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin")) // Если объект - монета
        {
            coinsCollected++; // Увеличиваем счётчик
            

            // 🔊 Проигрываем звук
            if (coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            // 🎇 Создаём эффект частиц в месте монеты
            if (coinEffectPrefab != null)
            {
                Instantiate(coinEffectPrefab, other.transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject); // Удаляем монету

            // 🔹 Обновляем UI
            if (coinUI != null)
            {
                coinUI.UpdateCoinUI(coinsCollected);
            }

            // 🏆 Проверка на победу
            if (coinsCollected >= totalCoinsToWin)
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        

        if (coinUI != null)
        {
            coinUI.ShowWinScreen(); // Показываем экран победы
        }
    }
}
