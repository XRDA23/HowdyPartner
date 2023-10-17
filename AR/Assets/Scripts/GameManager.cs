using System.Collections.Generic;
using System.Linq;
using Board;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask tileLayer;

    [SerializeField] private GameObject redPawnPrefab;
    [SerializeField] private GameObject bluePawnPrefab;
    [SerializeField] private GameObject yellowPawnPrefab;
    [SerializeField] private GameObject greenPawnPrefab;
    [SerializeField] private GameObject boardPrefab;
    // List to store paths
    private List<GameObject[]> paths = new List<GameObject[]>(); 
    [SerializeField] private Renderer boardRenderer;
    private Renderer pawnRenderer;

    private int currentPathIndex = 0; // Variable to keep track of current path

    private int currentStep = 0; // Variable to keep track of current step on path


    private GameObject[] pawns = new GameObject[16];
    private int currentPawnIndex = 0;
    
    private bool canMove = true;
    
    private bool isSwitching = false;
    private GameObject selectedPawn;
    private int[] currentSteps = new int[56];

    public void StartGame()
    {
        // Spawn the board prefab 
        SpawnBoardPrefab();

        // Get pawn renderer
        pawnRenderer = GetComponent<Renderer>();

        // Spawn the pawns
        SpawnPawns();
        GameObject[] redPath = GameObject.FindGameObjectsWithTag("RedPathTile"); 
        GameObject[] yellowPath = GameObject.FindGameObjectsWithTag("YellowPathTile"); 
        GameObject[] greenPath = GameObject.FindGameObjectsWithTag("GreenPathTile"); 
        GameObject[] bluePath = GameObject.FindGameObjectsWithTag("BluePathTile"); 

        paths.Add(redPath);
        paths.Add(yellowPath);
        paths.Add(greenPath);
        paths.Add(bluePath);
    }
    void SpawnBoardPrefab()
    {
        // Setting the position of board
        Vector3 boardPosition = new Vector3(0, -7f, 9f); 

        // Instantiate the board prefab
        GameObject boardInstance = Instantiate(boardPrefab, boardPosition, Quaternion.identity);
        
        // Adjust the rotation of the board
           boardInstance.transform.rotation = Quaternion.Euler(0, -180, 0);
           
           // Get board renderer
           boardRenderer = boardInstance.GetComponent<Renderer>();

    }
    
    public void ToggleBoardVisibility()
    {
        if (boardRenderer != null)
        {
            boardRenderer.enabled = !boardRenderer.enabled;
        }
    }

    public void TogglePawnVisibility(bool isVisible)
    {
        for (int i = 0; i < pawns.Length; i++)
        {
            if (pawns[i] != null)
            {
                Renderer[] renderers = pawns[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = isVisible;
                }
            }
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                if (pawns[currentPawnIndex] != null)
                {
                 
                    GameObject selectedPawn = pawns[currentPawnIndex]; 

        
                    int nrOfSteps = 3; 

                    MoveCurrentPawn(nrOfSteps);
                }
            }
        }
    }
    
 void SpawnPawns()
{
   GameObject[] redTiles = GameObject.FindGameObjectsWithTag("BaseRedTile");
   GameObject[] blueTiles = GameObject.FindGameObjectsWithTag("BaseBlueTile");
   GameObject[] yellowTiles = GameObject.FindGameObjectsWithTag("BaseYellowTile");
   GameObject[] greenTiles = GameObject.FindGameObjectsWithTag("BaseGreenTile");

   SpawnPawnsForColor(redTiles, Team.RedOrHeart, 4);
   SpawnPawnsForColor(blueTiles, Team.BlueOrWater, 4);
   SpawnPawnsForColor(yellowTiles, Team.YellowOrStar, 4);
   SpawnPawnsForColor(greenTiles, Team.GreenOrEmerald, 4);
}

void SpawnPawnsForColor(GameObject[] baseTiles, Team color, int count)
{
   int pawnCount = Mathf.Min(baseTiles.Length, count);

   for (int i = 0; i < pawnCount; i++)
   {
       GameObject pawnPrefab = GetPawnPrefab(color);

       pawns[currentPawnIndex] = Instantiate(pawnPrefab, baseTiles[i].transform.position, Quaternion.identity);
       PawnLogic pawnLogic = pawns[currentPawnIndex].GetComponent<PawnLogic>();
       pawnLogic.gameManager = this;
       pawnLogic.team = color;

       currentPawnIndex++;
   }
}
GameObject GetPawnPrefab(Team color)
{
    switch (color)
    {
        case Team.RedOrHeart:
            return redPawnPrefab;
        case Team.BlueOrWater:
            return bluePawnPrefab;
        case Team.YellowOrStar:
            return yellowPawnPrefab;
        case Team.GreenOrEmerald:
            return greenPawnPrefab;
        default:
            return null;
    }
}

public void MoveCurrentPawn(int nrOfSteps)
{
    if (pawns[currentPawnIndex] != null)
    {
        Team currentTeam = GetCurrentPawnTeam();

        GameObject[] currentPath = GetPathForTeam(currentTeam);

        if (currentStep + nrOfSteps <= currentPath.Length) // Adjusted condition
        {
            currentStep += nrOfSteps;

            Vector3 targetPosition = currentPath[currentStep - 1].transform.position; // Adjusted index
            pawns[currentPawnIndex].transform.position = targetPosition;
        }
        else
        {
            // Handle case where pawn completes the path
            SwitchToNextPath();
        }
    }
}



private Team GetCurrentPawnTeam()
{
    PawnLogic pawnLogic = pawns[currentPawnIndex].GetComponent<PawnLogic>();
    Debug.Log("Current Pawn Team: " + pawnLogic.team); // Debug
    return pawnLogic.team;
}

private GameObject[] GetPathForTeam(Team team)
{
    switch (team)
    {
        case Team.RedOrHeart:
            return GameObject.Find("RedPath").GetComponentsInChildren<Transform>()
                .Where(child => child.gameObject.tag == "RedPathTile").Select(child => child.gameObject).ToArray();
        case Team.BlueOrWater:
            return GameObject.Find("BluePath").GetComponentsInChildren<Transform>()
                .Where(child => child.gameObject.tag == "BluePathTile").Select(child => child.gameObject).ToArray();
        case Team.YellowOrStar:
            return GameObject.Find("YellowPath").GetComponentsInChildren<Transform>()
                .Where(child => child.gameObject.tag == "YellowPathTile").Select(child => child.gameObject).ToArray();
        case Team.GreenOrEmerald:
            return GameObject.Find("GreenPath").GetComponentsInChildren<Transform>()
                .Where(child => child.gameObject.tag == "GreenPathTile").Select(child => child.gameObject).ToArray();
        default:
            return null;
    }
}

void SwitchToNextPath()
{
    currentPathIndex = (currentPathIndex + 1) % paths.Count;
    currentStep = 0; // Reset current step
}

public void SelectPawn(GameObject pawn)
{
    for (int i = 0; i < pawns.Length; i++)
    {
        if (pawns[i] == pawn)
        {
            currentPawnIndex = i;
            Debug.Log("Current Pawn : " + pawn); // Debug
            return;
        }
    }
}


}