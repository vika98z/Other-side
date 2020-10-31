using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField]
    private Text bestScoreText;

    private void Awake()
    {
        Time.timeScale = 1;
        var score = PlayerPrefs.GetInt("BestScore");
        if (bestScoreText)
        {
            if (score > 0)
                bestScoreText.text = "Рекорд: " + score;
            else
                bestScoreText.text = "";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
