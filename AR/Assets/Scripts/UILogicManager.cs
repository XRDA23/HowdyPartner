using System;
using System.Collections.Generic;
using Board;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UILogicManager : MonoBehaviour
{
    public TextMeshProUGUI turnIndicatorText;
    public Button startButton;
    public GameObject startPanel;
    [SerializeField] private TextMeshProUGUI gameTitleText;
    [SerializeField] private TextMeshProUGUI gameRuleInfoText;
    [SerializeField] private Button showTurnButton;
    [SerializeField] private Button scanCardButton;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ImageTrackerScript imageTracker;
    [SerializeField] private TextMeshProUGUI detectedCardText;


    private List<Team> teams = new List<Team>();

    private void Start()
    {
        teams = EnumToList<Team>();
        // Initialize the teams

        // Ensure only the start button and title text are shown
        gameTitleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the inspector!");
        }

        if (scanCardButton != null)
        {
            scanCardButton.onClick.AddListener(OnScanButtonClicked);
        }
        else
        {
            Debug.LogError("Scan card Button is not assigned in the inspector!");
        }
        
        imageTracker.OnCardScanned += HandleCardScanned;
    }
    
    private void OnDestroy()
    {
        imageTracker.OnCardScanned -= HandleCardScanned;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space was pressed!");
        }
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Start game button was clicked!");

        // Hide the title and the start button
        startButton.gameObject.SetActive(false);
        gameTitleText.gameObject.SetActive(false);

        // Call the StartGame method from the GameManager
        gameManager.StartGame();
        
        // Display 'Show turn' button
        showTurnButton.gameObject.SetActive(true);

        if (showTurnButton != null)
        {
            showTurnButton.onClick.AddListener(OnShowTurnButtonClicked);
        }
        else
        {
            Debug.LogError("Show Turn Button is not assigned in the inspector!");
        }
    }

    private void OnShowTurnButtonClicked()
    {
        // Randomly decide whose turn it is and display the turn indicator
        Team startingTeam = teams[Random.Range(0, teams.Count)];
        UpdateTurnIndicator(startingTeam);

        turnIndicatorText.gameObject.SetActive(true); // Show the turn text
        scanCardButton.gameObject.SetActive(true);

        // Hide the "Show turn" button and game rule info after revealing the turn
        showTurnButton.gameObject.SetActive(false);
        gameRuleInfoText.gameObject.SetActive(false);
    }


    public void OnScanButtonClicked()
    {
        // For now, hide the elements.
        Debug.Log("Scan card Button was clicked!");
        scanCardButton.gameObject.SetActive(false);
        turnIndicatorText.gameObject.SetActive(false);
        
        // Enable the Image Tracker
        imageTracker.StartScanning();
        Debug.Log("Started scanning for a card...");
        
    }


    private void UpdateTurnIndicator(Team team)
    {
        turnIndicatorText.text = $"{team} team's turn!";
    }
    
    private void HandleCardScanned(CardTypeEnum cardType)
    {
        Debug.Log("Card scanned: " + cardType);
        imageTracker.StopScanning();
        
        // Display the detected card in the UI
        detectedCardText.text = "Detected Card: " + cardType;
        detectedCardText.gameObject.SetActive(true);
    }


    // Method to convert enum values to a list.
    private List<T> EnumToList<T>()
    {
        return new List<T>((T[])Enum.GetValues(typeof(T)));
    }
}