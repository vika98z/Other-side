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
        var flag = PlayerPrefs.HasKey("BestScore");
        if (bestScoreText)
        {
            if (flag)
            {
                var score = PlayerPrefs.GetInt("BestScore");
                bestScoreText.text = "Рекорд: " + score;
            }
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
