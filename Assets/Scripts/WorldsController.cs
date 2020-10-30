using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private LayerMask oldMask;
    private PlayerType gameOverType;

    void Awake()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        oldMask = camera.cullingMask;
        IsGameOver = false;
        CurrentWorldType = WorldType.Light;
        camera.cullingMask = LightMask;
        CurrentPlayerPower = PlayerPower.Shoot;
        CurrentPlayerType = PlayerType.LightPlayer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !IsGameOver)
            ChangeWorlds();
    }

    public void ChangeWorlds()
    {
        switch (CurrentWorldType)
        {
            case WorldType.Dark:
                camera.cullingMask = LightMask;
                CurrentWorldType = WorldType.Light;
                CurrentPlayerPower = PlayerPower.Shoot;
                CurrentPlayerType = PlayerType.LightPlayer;
                break;
            case WorldType.Light:
                camera.cullingMask = DarkMask;
                CurrentWorldType = WorldType.Dark;
                CurrentPlayerPower = PlayerPower.Jump;
                CurrentPlayerType = PlayerType.DarkPlayer;
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
}