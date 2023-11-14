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

        private List<TeamEnum> teams = new ();
        public Material highlightMaterial;
        private Material originalMaterial;
        private List<Material> originalMaterials = new (); 
        [SerializeField] private Button selectButton;
        private TeamEnum currentTeamTurn;
        private CardActionEnum selectedAction;
        private Dictionary<CardTypeEnum, CardActionEnum> cardAction;
        private List<Pawn> currentTeamPawns;
        private GameObject selectionCube;

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

            cardAction = new Dictionary<CardTypeEnum, CardActionEnum>()
            {
                {CardTypeEnum.Switch, CardActionEnum.Switch},
                {CardTypeEnum.Two, CardActionEnum.Two},
                {CardTypeEnum.Three, CardActionEnum.Three},
                {CardTypeEnum.FourBackwards, CardActionEnum.FourBackwards},
                {CardTypeEnum.Five, CardActionEnum.Five},
                {CardTypeEnum.Six, CardActionEnum.Six},
                {CardTypeEnum.Nine, CardActionEnum.Nine},
                {CardTypeEnum.Ten, CardActionEnum.Ten},
                {CardTypeEnum.Heart, CardActionEnum.Heart},
                {CardTypeEnum.Twelve, CardActionEnum.Twelve},
                {CardTypeEnum.SevenTimesOne, CardActionEnum.SevenTimesOne}
            };
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
            
            // Randomly decide whose turn it is and display the turn indicator
            currentTeamTurn = teams[Random.Range(0, teams.Count)];
            currentTeamPawns = gameLogic.pawns.Where(pawn => pawn.teamEnum == currentTeamTurn).ToList();
        }
        
        private void OnShowTurnButtonClicked()
        {
           // HandleOriginalMaterials();
            
            UpdateTurnIndicator();
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

        private void OnScanButtonClicked()
        {
            if (selectionCube != null)
            {
                Destroy(selectionCube);
            }

            imageTracker.OnCardScanned += HandleCardScanned;
            
            // Hide board
            gameLogic.ToggleBoardVisibility(false);
            // Hide pawns
            gameLogic.TogglePawnVisibility(false);

            scanCardButton.gameObject.SetActive(false);
            turnIndicatorText.gameObject.SetActive(false);
        }

        private void UpdateTurnIndicator()
        {
            turnIndicatorText.text = $"{currentTeamTurn} team's turn!";
            turnIndicatorText.gameObject.SetActive(true);
        }

        private void NextTurn()
        {
            if (selectionCube != null)
            {
                Destroy(selectionCube);
            }
            
            //Clearing stored variables for last player
            currentTeamTurn = currentTeamTurn.GetNextTeam();
            currentTeamPawns = gameLogic.pawns.Where(pawn => pawn.teamEnum == currentTeamTurn).ToList();
            selectedPawn = currentTeamPawns.First();

            //Showing whose turn it is & scan card button
            UpdateTurnIndicator();
            scanCardButton.gameObject.SetActive(true);
            ShowBoardAndPawns();
        }

        private void HidePawnSelectionUI()
        {
            detectedCardText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(false);
            selectButton.GameObject().SetActive(false);
        }

        private void HandleOptionSelected(CardActionEnum option)
        {
            Debug.Log("Option selected: " + option);
            optionPanel.SetActive(false);
            selectedAction = option;
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
                    option1Button.onClick.AddListener(() => HandleOptionSelected(CardActionEnum.One));

                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Fourteen";
                    option2Button.onClick.AddListener(() => HandleOptionSelected(CardActionEnum.Fourteen));
                    break;

                case CardTypeEnum.HeartOrEight:
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Heart";
                    option1Button.onClick.AddListener(() => HandleOptionSelected(CardActionEnum.Heart));

                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Eight";
                    option2Button.onClick.AddListener(() => HandleOptionSelected(CardActionEnum.Eight));
                    break;

                case CardTypeEnum.HeartOrThirteen:
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Heart";
                    option1Button.onClick.AddListener(() => HandleOptionSelected(CardActionEnum.Heart));

                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Thirteen";
                    option2Button.onClick.AddListener(() => HandleOptionSelected(CardActionEnum.Thirteen));
                    break;
            }

            optionPanel.SetActive(true);
        }

        private void HandleCardScanned(CardTypeEnum cardType)
        {
            Debug.Log("HandleCardScanned method entered.");
            Debug.Log("Card scanned: " + cardType);

            if (cardType is CardTypeEnum.OneOrFourteen or CardTypeEnum.HeartOrEight or CardTypeEnum.HeartOrThirteen)
            {
                SetupOptionPanelForCard(cardType);
            }
            else
            {
                selectedAction = cardAction[cardType];
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
            Invoke(nameof(ShowBoardAndPawns), 2.0f);
            
            EnableSelectionButtons();
        }

        private void EnableSelectionButtons()
        {
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
            selectButton.GameObject().SetActive(true);
        }

        private void ShowBoardAndPawns()
        {
            gameLogic.ToggleBoardVisibility(true);
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

        private void OnNextButtonClicked()
        {
            currentPawnIndex++;
            if (currentPawnIndex >= currentTeamPawns.Count)
            {
                currentPawnIndex = 0;
            }

            SelectPawnByIndex(currentTeamPawns[currentPawnIndex]);
        }

        private void OnPreviousButtonClicked()
        {
            currentPawnIndex--;
            if (currentPawnIndex < 0)
            {
                currentPawnIndex = currentTeamPawns.Count - 1;
            }

            SelectPawnByIndex(currentTeamPawns[currentPawnIndex]);
        }

        private void SelectPawnByIndex(Pawn pawn)
        {
            if (selectionCube != null)
            {
                Destroy(selectionCube);
            }
            selectedPawn = pawn;

            Debug.Log("Selected Pawn : " + selectedPawn.gameObject);

            var spawnPosition = selectedPawn.transform.position + Vector3.up * 2;
            selectionCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        }

        private void OnSelectButtonClicked()
        {
            try
            {
                if (selectedPawn != null)
                {
                    selectedPawn.transform.position = gameLogic.PlayCard(selectedPawn, selectedAction).vector3Position;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                NextTurn();
            }
        }
    }
}