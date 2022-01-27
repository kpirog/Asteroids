using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }

    public List<Image> liveImages;
    
    public int lifes = 3;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        player.onPlayerDied.AddListener(PlayerDied);   
    }

    public void PlayerDied()
    {
        if (lifes <= 0)
        {
            GameOver();
            return;
        }
            
        lifes--;

        for (int i = liveImages.Count - 1; i >= 0; i--)
        {
            if (liveImages[i].gameObject.activeInHierarchy)
            {
                liveImages[i].gameObject.SetActive(false);
                break;
            }
        }

        ResetPlayerSettings();
    }
    private void GameOver()
    {
        foreach (Image heart in liveImages)
        {
            heart.gameObject.SetActive(true);
        }

        ResetPlayerSettings();
    }
    private void ResetPlayerSettings()
    {
        player.ResetPosition();
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        Invoke(nameof(TurnOnCollisions), 3f);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
