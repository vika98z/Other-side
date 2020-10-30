using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private LayerMask oldMask;
    void Awake()
    {
        Time.timeScale = 1;

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

        if (player != CurrentPlayerType)
        {
            ChangeWorlds();
        }

        camera.GetComponent<CameraController>().SetZoom();
    }

    public void SetResults()
    {
        Time.timeScale = 0;

    }
}