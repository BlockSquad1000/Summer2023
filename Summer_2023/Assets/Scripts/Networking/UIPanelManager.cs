using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Photon.Pun;
using Photon.Realtime;

using ExitGames.Client.Photon;
using JetBrains.Annotations;

public class UIPanelManager : MonoBehaviourPunCallbacks
{
    public static UIPanelManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject loginUIPanel;
    public GameObject connectingInfoPanel;
    public GameObject createRoomUIPanel;
    public GameObject creatingRoomInfoPanel;
    public GameObject gameOptionsUIPanel;
    public GameObject joinRandomUIPanel;
    public GameObject insideRoomUIPanel;
    //public GameObject lapUIPanel;

    [Header("Text Fields")]
    [SerializeField] private Text creatingGameUIMessagee;
    [SerializeField] private TMP_Text scoreText;

    [Header("Inside Room Panels")]
    [SerializeField] private Text gameTypeUIText;
    [SerializeField] private TMP_Text roomInfoUIText;
    [SerializeField] private GameObject playerListUIContainer;
    [SerializeField] private GameObject playerEntryPrefab;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject joinTeamOneButton;
    [SerializeField] private GameObject joinTeamTwoButton;
    [SerializeField] private Image roomPanelBackground;
    [SerializeField] private Sprite deathmatchBackground;
    [SerializeField] private Sprite ctfBackground;

    [Header("Mercenary Selection Panels")]
    [SerializeField] private GameObject[] characterSelectionUIObjects;
    [SerializeField] private CharactersSO[] characters;

    private Dictionary<int, GameObject> playerDictionary = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ActivatePanel(string panelName)
    {
        loginUIPanel.SetActive(loginUIPanel.name.Equals(panelName));
        connectingInfoPanel.SetActive(connectingInfoPanel.name.Equals(panelName));
        createRoomUIPanel.SetActive(createRoomUIPanel.name.Equals(panelName));
        creatingRoomInfoPanel.SetActive(creatingRoomInfoPanel.name.Equals(panelName));
        gameOptionsUIPanel.SetActive(gameOptionsUIPanel.name.Equals(panelName));
        joinRandomUIPanel.SetActive(joinRandomUIPanel.name.Equals(panelName));
        insideRoomUIPanel.SetActive(insideRoomUIPanel.name.Equals(panelName));
    }

    public void UpdateCreatingRoomUIMessage(string message)
    {
        creatingGameUIMessagee.text = message;
    }

    public void UpdateRoomInfoUIText(string text)
    {
        roomInfoUIText.text = text;
    }

    public void UpdateGameTypeInsideRoom(string mode)
    {
        if(mode == GameMode.DeathMatch.ToString())
        {
            roomPanelBackground.sprite = deathmatchBackground;
            gameTypeUIText.text = "Let's Battle!";
            scoreText.text = "How many kills?";

            for(int i = 0; i < characterSelectionUIObjects.Length; i++)
            {
                GameObject go = characterSelectionUIObjects[i];
                CharactersSO ships = characters[i];

               // SetCharacterPanelProperties(go, ships.characterSprite, ships.characterName, string.Empty);
            }
        }
        if (mode == GameMode.CaptureTheFlag.ToString())
        {
            roomPanelBackground.sprite = ctfBackground;
            gameTypeUIText.text = "Let's Capture!";
            scoreText.text = "How many captures?";

            for (int i = 0; i < characterSelectionUIObjects.Length; i++)
            {
                GameObject go = characterSelectionUIObjects[i];
                CharactersSO ships = characters[i];

               // SetCharacterPanelProperties(go, ships.characterSprite, ships.characterName, string.Empty);
            }
        }
    }

    private static void SetCharacterPanelProperties(GameObject go, Sprite sprite, string name, string property)
    {
        go.GetComponent<Image>().sprite = sprite;
        go.transform.Find("PlayerName").GetComponent<Text>().text = name;
        go.transform.Find("PlayerProperty").GetComponent<Text>().text = property;
    }

    public void SetStartButtonActiveState(bool state)
    {
        startGameButton.SetActive(state);
    }

    public void AddPlayerToLobbyList(Player newPlayer)
    {
        GameObject entry = Instantiate(playerEntryPrefab);
        entry.transform.SetParent(playerListUIContainer.transform);
        entry.transform.localScale = Vector3.one;
        entry.GetComponent<PlayerEntryInitializer>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);
        playerDictionary.Add(newPlayer.ActorNumber, entry);
    }

    public void RemovePlayerFromLobbyList (int actorNumber)
    {
        Destroy(playerDictionary[actorNumber]);
        playerDictionary.Remove(actorNumber);
    }
    
    public void ClearPlayerLobbyList()
    {
        foreach(GameObject gameObject in playerDictionary.Values)
        {
            Destroy(gameObject);
        }
        playerDictionary.Clear();
    }

    public void UpdatePlayerReadyStatus(Player targetPlayer, Hashtable changedProps)
    {
        if(playerDictionary.TryGetValue(targetPlayer.ActorNumber, out GameObject playerEntryObject))
        {
            if(changedProps.TryGetValue(GameConstants.PLAYER_READY_KEY, out object isPlayerReady))
            {
                playerEntryObject.GetComponent<PlayerEntryInitializer>().SetPlayerReady((bool)isPlayerReady);
            }
        }
    }
}
