using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { DarkPlayer, LightPlayer };

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject darkSkin;
    [SerializeField]
    private GameObject lightSkin;

    public PlayerType PlayerType;

    public void SetPlayerType(PlayerType type)
    {
        switch (type)
        {
            case PlayerType.DarkPlayer:
                darkSkin.SetActive(true);
                lightSkin.SetActive(false);
                break;
            case PlayerType.LightPlayer:
                lightSkin.SetActive(true);
                darkSkin.SetActive(false);
                break;
            default:
                break;
        }

        PlayerType = type;
    }
}
