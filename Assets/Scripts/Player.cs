using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { DarkPlayer, LightPlayer };

public class Player: MonoBehaviour
{
    [SerializeField]
    private GameObject darkSkin;
    [SerializeField]
    private GameObject lightSkin;
    [SerializeField]
    private WorldsController worldsController;

    public PlayerType PlayerType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
            worldsController.GameOver(PlayerType);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            worldsController.GameOver(PlayerType);
    }
}
