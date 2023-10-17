using System;
using System.Collections.Generic;
using Enums;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class UILogicManager : MonoBehaviour
    {
        public TextMeshProUGUI turnIndicatorText;
        public Button startButton;
        public GameObject startPanel;
        [SerializeField] private TextMeshProUGUI gameTitleText;
        [SerializeField] private TextMeshProUGUI gameRuleInfoText;
        [SerializeField] private Button showTurnButton;
        [SerializeField] private Button scanCardButton;
        [SerializeField] private GameLogic gameLogic;
        [SerializeField] private ImageTrackerScript imageTracker;
        [SerializeField] private TextMeshProUGUI detectedCardText;
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private Button option1Button;
        [SerializeField] private Button option2Button;
        public LayerMask tileLayer;


        private List<TeamEnum> teams = new List<TeamEnum>();

        private void Start()
        {
            teams = EnumToList<TeamEnum>();
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

            optionPanel.SetActive(false);

            if (option1Button != null)
            {
                option1Button.onClick.AddListener(OnOption1ButtonClicked);
            }
            else
            {
                Debug.LogError("Option 1 Button is not assigned in the inspector!");
            }

            if (option2Button != null)
            {
                option2Button.onClick.AddListener(OnOption2ButtonClicked);
            }
            else
            {
                Debug.LogError("Option 2 Button is not assigned in the inspector!");
            }
        }

        private void OnDestroy()
        {
            imageTracker.OnCardScanned -= HandleCardScanned;
        }

        private void OnStartButtonClicked()
        {
            Debug.Log("Start game button was clicked!");

            // Hide the title and the start button
            startButton.gameObject.SetActive(false);
            gameTitleText.gameObject.SetActive(false);

            // Call the StartGame method from the GameManager
            gameLogic.StartGame();

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
            TeamEnum startingTeamEnum = teams[Random.Range(0, teams.Count)];
            UpdateTurnIndicator(startingTeamEnum);

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
        
            if (gameLogic != null)
            {
                // Hide board
                gameLogic.ToggleBoardVisibility();
                // Hide pawns
                gameLogic.TogglePawnVisibility(false); 
            }

            scanCardButton.gameObject.SetActive(false);
            turnIndicatorText.gameObject.SetActive(false);

            if (imageTracker != null)
            {
                imageTracker.OnCardScanned += HandleCardScanned;
            }
            else
            {
                Debug.LogError("Image Tracker is not assigned in the inspector!");
            }

            Debug.Log("Started scanning for a card...");
        }

        private void UpdateTurnIndicator(TeamEnum teamEnum)
        {
            turnIndicatorText.text = $"{teamEnum} team's turn!";
        }

        private void HandleOptionSelected(string option)
        {
            Debug.Log("Option selected: " + option);
            optionPanel.SetActive(false);
        }

        private void SetupOptionPanelForCard(CardTypeEnum cardType)
        {
            // Reset the button text and listeners
            option1Button.onClick.RemoveAllListeners();
            option2Button.onClick.RemoveAllListeners();

            switch (cardType)
            {
                case CardTypeEnum.OneOrFourteen:
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "One";
                    option1Button.onClick.AddListener(() => HandleOptionSelected("One"));

                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Fourteen";
                    option2Button.onClick.AddListener(() => HandleOptionSelected("Fourteen"));
                    break;

                case CardTypeEnum.HeartOrEight:
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Heart";
                    option1Button.onClick.AddListener(() => HandleOptionSelected("Heart"));

                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Eight";
                    option2Button.onClick.AddListener(() => HandleOptionSelected("Eight"));
                    break;

                case CardTypeEnum.HeartOrThirteen:
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Heart";
                    option1Button.onClick.AddListener(() => HandleOptionSelected("Heart"));

                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Thirteen";
                    option2Button.onClick.AddListener(() => HandleOptionSelected("Thirteen"));
                    break;
            }

            optionPanel.SetActive(true);
        }

        private void HandleCardScanned(CardTypeEnum cardType)
        {
            Debug.Log("HandleCardScanned method entered.");
            Debug.Log("Card scanned: " + cardType);

            if (cardType == CardTypeEnum.OneOrFourteen || cardType == CardTypeEnum.HeartOrEight ||
                cardType == CardTypeEnum.HeartOrThirteen)
            {
                SetupOptionPanelForCard(cardType);
            }

            if (detectedCardText != null)
            {
                detectedCardText.text = "Detected Card: " + cardType;
                detectedCardText.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("detectedCardText is null.");
            } 

            // Delay by 3 seconds
            Invoke("ToggleBoardVisibilityAndPawnVisibility", 3.0f);
        }

        private void ToggleBoardVisibilityAndPawnVisibility()
        {
            // Show board
            gameLogic.ToggleBoardVisibility();
            // Show pawns
            gameLogic.TogglePawnVisibility(true);
        }
    
        private void OnOption1ButtonClicked()
        {
            Debug.Log("Option 1 selected!");
            optionPanel.SetActive(false);
        }

        private void OnOption2ButtonClicked()
        {
            Debug.Log("Option 2 selected!");
            optionPanel.SetActive(false);
        }

        // Method to convert enum values to a list.
        private List<T> EnumToList<T>()
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
                {
                    //TODO: implement SelectPawn first
                    // if (pawns[currentPawnIndex] != null)
                    // {
                    //
                    //     GameObject selectedPawn = pawns[currentPawnIndex]; 
                    //
                    //
                    //     int nrOfSteps = 3; 
                    //
                    //     MoveCurrentPawn(nrOfSteps);
                    // }
                }
            }
        }
        public void SelectPawn(GameObject pawn)
        {
            //TODO: Get selected pawn and store it for calling the game logic
            // for (int i = 0; i < pawns.Length; i++)
            // {
            //     if (pawns[i] == pawn)
            //     {
            //         currentPawnIndex = i;
            //         Debug.Log("Current Pawn : " + pawn); // Debug
            //         return;
            //     }
            // }
        }
    }
}