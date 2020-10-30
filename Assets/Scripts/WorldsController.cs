using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum WorldType { Dark, Light };

public class WorldsController : MonoBehaviour
{
    public WorldType CurrentWorldType { get; private set; }

    [SerializeField]
    private GameObject darkWorld;
    [SerializeField]
    private GameObject lightWorld;
    [SerializeField]
    private Player player;

    void Awake()
    {
        player.SetPlayerType(PlayerType.LightPlayer);
        darkWorld.SetActive(false);
        lightWorld.SetActive(true);
        CurrentWorldType = WorldType.Light;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWorlds();
        }
    }

    private void ChangeWorlds()
    {
        switch (CurrentWorldType)
        {
            case WorldType.Dark:
                darkWorld.SetActive(false);
                lightWorld.SetActive(true);
                player.SetPlayerType(PlayerType.LightPlayer);
                CurrentWorldType = WorldType.Light;
                break;
            case WorldType.Light:
                darkWorld.SetActive(true);
                lightWorld.SetActive(false);
                player.SetPlayerType(PlayerType.DarkPlayer);
                CurrentWorldType = WorldType.Dark;
                break;
            default:
                break;
        }
    }
}
