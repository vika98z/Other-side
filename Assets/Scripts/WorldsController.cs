using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum WorldType { Dark, Light };

public class WorldsController : MonoBehaviour
{
    public WorldType CurrentWorldType { get; private set; }
    public PlayerPower CurrentPlayerPower;
    public PlayerType CurrentPlayerType;
    public bool IsGameOver;

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Player darkPlayer;
    [SerializeField]
    private Player lightPlayer;
    [SerializeField]
    private LayerMask DarkMask;
    [SerializeField]
    private LayerMask LightMask;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Image HomeButton;

    private int Score;
    private int maxScore;
    private int curScore;
    private LayerMask oldMask;
    private PlayerType gameOverType;
    private CameraController cameraController;
    private int startX;
    void Awake()
    {
        Score = 0;
        startX = (int)lightPlayer.transform.position.x;
        maxScore = PlayerPrefs.GetInt("BestScore");
        cameraController = camera.GetComponent<CameraController>();
        cameraController.Player = lightPlayer.transform;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        oldMask = camera.cullingMask;
        IsGameOver = false;
        CurrentWorldType = WorldType.Light;
        camera.cullingMask = LightMask;
        CurrentPlayerPower = PlayerPower.Shoot;
        CurrentPlayerType = PlayerType.LightPlayer;

        ChangeColorUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !IsGameOver)
            ChangeWorlds();

        if (Input.GetKeyDown(KeyCode.Escape))
            Menu();

        curScore = (int)lightPlayer.transform.position.x - startX;
        Mathf.Clamp(curScore, 0, curScore);
        if (curScore > Score)
            Score = curScore;

        ScoreText.text = Score.ToString();
    }

    public void ChangeWorlds()
    {
        switch (CurrentWorldType)
        {
            case WorldType.Dark:
                cameraController.Player = lightPlayer.transform;
                camera.cullingMask = LightMask;
                CurrentWorldType = WorldType.Light;
                CurrentPlayerPower = PlayerPower.Shoot;
                CurrentPlayerType = PlayerType.LightPlayer;
                break;
            case WorldType.Light:
                cameraController.Player = darkPlayer.transform;
                camera.cullingMask = DarkMask;
                CurrentWorldType = WorldType.Dark;
                CurrentPlayerPower = PlayerPower.Jump;
                CurrentPlayerType = PlayerType.DarkPlayer;
                break;
            default:
                break;
        }

        ChangeColorUI();
    }

    private void ChangeColorUI()
    {
        switch (CurrentWorldType)
        {
            case WorldType.Dark:
                HomeButton.color = Color.white;
                ScoreText.color = Color.white;
                break;
            case WorldType.Light:
                HomeButton.color = Color.black;
                ScoreText.color = Color.black;
                break;
            default:
                break;
        }
    }

    public void GameOver(PlayerType player)
    {
        IsGameOver = true;
        gameOverType = player;
        if (player != CurrentPlayerType)
        {
            ChangeWorlds();
        }

        camera.GetComponent<CameraController>().SetZoom();
    }

    public void SetResults()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        if (Score > maxScore)
            PlayerPrefs.SetInt("BestScore", Score);

        switch (gameOverType)
        {
            case PlayerType.DarkPlayer:
                gameOverText.text = "Погиб игрок из тёмного мира";
                break;
            case PlayerType.LightPlayer:
                gameOverText.text = "Погиб игрок из светлого мира";
                break;
            default:
                break;
        }
    }

    public void Menu()
    {
        if (Score > maxScore)
            PlayerPrefs.SetInt("BestScore", Score);
        SceneManager.LoadScene("Menu");
    }
}