using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Range(0f, 0.20f)]
    public float parallaxSpeed = 0.02f;

    public RawImage background, platform;

    public GameObject UiIdle;

    public enum GameState
    {
        Idle,
        Playing
    }

    public GameState gameState = GameState.Idle;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Idle:
                if (!Input.GetKeyDown("up") && !Input.GetMouseButtonDown(0))
                {
                    break;
                }

                gameState = GameState.Playing;
                UiIdle.SetActive(false);
                break;

            case GameState.Playing:
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    player.SendMessage("UpdateState", "PlayerRun");
                    Parallax();
                    break;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    ReverseParallax();
                    break;
                }

                player.SendMessage("UpdateState", "PlayerIdle");
                break;
        }
    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect =
            new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        platform.uvRect =
            new Rect(background.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    }

    void ReverseParallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;

        background.uvRect =
            new Rect(background.uvRect.x - finalSpeed, 0f, 1f, 1f);
        platform.uvRect =
            new Rect(background.uvRect.x - finalSpeed * 4, 0f, 1f, 1f);
    }

}
