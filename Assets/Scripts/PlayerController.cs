using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;

public enum PlayerPower { Shoot, Jump };

public class PlayerController : PhysicsObject
{
    [SerializeField]
    private float maxSpeed = 7;
    [SerializeField]
    private float jumpTakeOffSpeed = 7;

    [SerializeField]
    private WorldsController worldsController;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform otherPlayer;
    [SerializeField]
    private bool isChecking;

    Animator animator;
    private float shootTime = 0;
    private Player player;
    private  float offsetX = .2f;
    private AudioSource audioSource;

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    protected override void ComputeVelocity()
    {
        if (worldsController.IsGameOver)
        {
            animator.SetFloat("velocityX", 0);
            return;
        }

        shootTime += Time.deltaTime;
        Vector2 move = Vector2.zero;
        
        if (isChecking)
        {
            if (Mathf.Abs(transform.position.x - otherPlayer.position.x) > offsetX)
                transform.position = otherPlayer.position;

            move.x = Input.GetAxis("Horizontal");
        }
        else
            move.x = Input.GetAxis("Horizontal");


        if (Input.GetButtonDown("Jump") && grounded && worldsController.CurrentPlayerPower == PlayerPower.Jump)
        {
            velocity.y = jumpTakeOffSpeed;
            audioSource.clip = worldsController.MusicController.Jump;
            audioSource.Play();
        }
        else if (Input.GetButtonUp("Jump") && worldsController.CurrentPlayerPower == PlayerPower.Jump
                    && player.PlayerType == worldsController.CurrentPlayerType)
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }


        if (Input.GetButtonDown("Jump") && worldsController.CurrentPlayerPower == PlayerPower.Shoot && shootTime > .6f
                && player.PlayerType == worldsController.CurrentPlayerType)
        {
            var sign = (spriteRenderer.flipX) ? -1 : 1;
            audioSource.clip = worldsController.MusicController.Shoot;
            audioSource.Play();
            GameObject bul = Instantiate(bullet, new Vector2(player.transform.position.x + sign * 0.2f, player.transform.position.y + 0.8f), Quaternion.identity);
            bul.GetComponent<BulletController>().Sign = sign;
            shootTime = 0;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (grounded)
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
        else
            animator.SetFloat("velocityX", 0);

        targetVelocity = move * maxSpeed;
    }
}