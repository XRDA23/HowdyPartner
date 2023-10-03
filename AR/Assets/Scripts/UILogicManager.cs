﻿using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;  

public class UILogicManager: MonoBehaviour
{
    /*
    [SerializeField]  private TextMeshProUGUI turnText;
     private TextMeshProUGUI outcomeText;
     private TextMeshProUGUI winningTeamText;
    private GameObject cardOptionPanel; // A panel that pops up when card options are needed.
    private Camera mainCamera;
    public Team testTeam;  // This is for testing purposes.
    [SerializeField]  private PawnSymbol _pawnSymbol;
    */
   


    public TextMeshProUGUI turnIndicatorText; 
    public Button startButton; 
    public GameObject startPanel;  
    [SerializeField] private TextMeshProUGUI gameTitleText;
    [SerializeField] private TextMeshProUGUI scanCardText; // Reference to the text element for scan instructions.
    [SerializeField] private Button scanCardButton; // Reference to the scan button.

    private List<TeamLia> teams = new List<TeamLia>();

    private void Start()
    {
        // Initialize the teams
        teams.Add(new TeamLia(PawnSymbolEnum.Emerald));
        teams.Add(new TeamLia(PawnSymbolEnum.Heart));
        teams.Add(new TeamLia(PawnSymbolEnum.Star));
        teams.Add(new TeamLia(PawnSymbolEnum.WaterDrop));
        // Ensure only the start button and title text are shown
        gameTitleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        turnIndicatorText.gameObject.SetActive(false); // hide the turn indicator

        if(startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the inspector!");
        }
        
        if(scanCardButton != null)
        {
            scanCardButton.onClick.AddListener(OnScanButtonClicked);
        }
        else
        {
            Debug.LogError("Scan card Button is not assigned in the inspector!");
        }
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

       startButton.gameObject.SetActive(false);
       gameTitleText.gameObject.SetActive(false); // Hide the title text

       // Randomly decide whose turn it is and display the turn indicator
       TeamLia startingTeam = teams[Random.Range(0, teams.Count)];
       turnIndicatorText.gameObject.SetActive(true); // Show the turn text
       UpdateTurnIndicator(startingTeam);
       
       // Show scan instructions and the scan button
       scanCardText.gameObject.SetActive(true);
       scanCardButton.gameObject.SetActive(true);
   }

   public void OnScanButtonClicked()
   {

       // For now, hide the elements.
       Debug.Log("Scan card Button was clicked!");
       scanCardText.gameObject.SetActive(false);
       scanCardButton.gameObject.SetActive(false);
       turnIndicatorText.gameObject.SetActive(false);
       
   }
    
    private void UpdateTurnIndicator(TeamLia team)
    {
        turnIndicatorText.text = $"{team.Symbol}'s turn!";
    }
    
}