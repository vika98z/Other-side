using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioClip mainBackround;
    [SerializeField]
    private AudioClip gameOverBackround;

    public AudioClip Jump;
    public AudioClip Shoot;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.clip = mainBackround; 
        audioSource.Play();
    }

    public void SetGameOverMusic()
    {
        audioSource.clip = gameOverBackround;
        audioSource.Play();
    }
}
