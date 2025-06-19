using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public Button menuButton;
    public GameObject panel;
    public Button resumeButton;
    public Button titleButton;

    bool isPaused = false;
    bool isToggling = false;

    void Awake()
    {
        panel.SetActive(false);  // 最初は非表示
        menuButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(ClosePauseMenu);  // ← 別関数に分けた！
        titleButton.onClick.AddListener(ReturnToTitle);
    }

    public void TogglePause()
    {
        if (isToggling) return;  // 2回押し防止
        isToggling = true;

        isPaused = !isPaused;
        Debug.Log("TogglePause → isPaused=" + isPaused);

        panel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        StartCoroutine(EnableToggleAfterDelay());
    }

    public void ClosePauseMenu()
    {
        isPaused = false;
        panel.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("PauseMenu closed.");
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1; // ポーズ解除
        SceneManager.LoadScene("TitleScene");
    }

    System.Collections.IEnumerator EnableToggleAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        isToggling = false;
    }
}
