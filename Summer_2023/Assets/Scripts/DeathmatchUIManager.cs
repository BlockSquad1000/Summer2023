using TMPro;
using UnityEngine;

using Photon.Pun;
using UnityEngine.SceneManagement;

public class DeathmatchUIManager : MonoBehaviourPunCallbacks
{
    public GameObject pauseMenu;
    bool gameIsPause = false;

    public TMP_Text countdownUIText;

    public GameObject[] scoreOrderUIObjects;
    public GameObject FinishUIBackground;
    public TMP_Text timeCountText;

    public bool finished;

    public static DeathmatchUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (GameObject go in scoreOrderUIObjects)
        {
            go.SetActive(false);
        }
        pauseMenu.SetActive(false);
        FinishUIBackground.SetActive(false);
    }

    public GameObject GetScoreOrderUIObject(int which)
    {
        return scoreOrderUIObjects[which];
    }

    public void SetCountdownUIText(string text)
    {
        countdownUIText.text = text;
    }

    public void OnQuitClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        SceneManager.LoadScene("MainMenu");
    }

    public void Paused()
    {
        gameIsPause = true;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        gameIsPause = false;
        pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
}
