using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Logic;
using Models;
using TMPro;
using Unity.VisualScripting;
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
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        public GameObject cubePrefab;

        public LayerMask tileLayer;
        private Pawn selectedPawn; 
        private bool canMove = true;
        private int currentPawnIndex = -1;


        private List<TeamEnum> teams = new List<TeamEnum>();
        public Material highlightMaterial;
        private Material originalMaterial;
        private List<Material> originalMaterials = new List<Material>(); 
        [SerializeField] private Button selectButton;
        private TeamEnum currentTeamTurn;



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
            if (nextButton != null)
            {
                nextButton.onClick.AddListener(OnNextButtonClicked);
            }
            else
            {
                Debug.LogError("Next Button is not assigned in the inspector!");
            }

            if (previousButton != null)
            {
                previousButton.onClick.AddListener(OnPreviousButtonClicked);
            }
            else
            {
                Debug.LogError("Previous Button is not assigned in the inspector!");
            }
            if (selectButton != null)
            {
                selectButton.onClick.AddListener(OnSelectButtonClicked);
            }
            else
            {
                Debug.LogError("Select Button is not assigned in the inspector!");
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
            
           // HandleOriginalMaterials();
            
            turnIndicatorText.gameObject.SetActive(true); // Show the turn text
            scanCardButton.gameObject.SetActive(true);

            // Hide the "Show turn" button and game rule info after revealing the turn
            showTurnButton.gameObject.SetActive(false);
            gameRuleInfoText.gameObject.SetActive(false);
        }
        
        private void HandleOriginalMaterials()
        {
            originalMaterials.Clear(); 

            for (int i = 0; i < gameLogic.pawns.Count; i++)
            {
                originalMaterials.Add(gameLogic.pawns[i].GetComponent<Renderer>().material);
            }
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
            currentTeamTurn = teamEnum;
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
            // Enable next and previous buttons
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
            selectButton.GameObject().SetActive(true);
        
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
   

        
        public void OnNextButtonClicked()
        {
            List<Pawn> currentTeamPawns = gameLogic.pawns.Where(pawn => pawn.teamEnum == currentTeamTurn).ToList();

            currentPawnIndex++;
            if (currentPawnIndex >= currentTeamPawns.Count)
            {
                currentPawnIndex = 0;
            }

            SelectPawnByIndex(currentTeamPawns[currentPawnIndex]);
        }

        public void OnPreviousButtonClicked()
        {
            List<Pawn> currentTeamPawns = gameLogic.pawns.Where(pawn => pawn.teamEnum == currentTeamTurn).ToList();

            currentPawnIndex--;
            if (currentPawnIndex < 0)
            {
                currentPawnIndex = currentTeamPawns.Count - 1;
            }

            SelectPawnByIndex(currentTeamPawns[currentPawnIndex]);
        }




        private void SelectPawnByIndex(Pawn pawn)
        {
            selectedPawn = pawn;

            Debug.Log("Selected Pawn : " + selectedPawn.gameObject);

            Vector3 spawnPosition = selectedPawn.transform.position + Vector3.up * 2;
            GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

            Destroy(cube, 2.0f);
        }



        private void OnSelectButtonClicked()
        {
            if (selectedPawn != null)
            {
                
                // disable the Next and Previous buttons until the next turn.
                nextButton.interactable = false;
                previousButton.interactable = false;
            }
        }


    }
}