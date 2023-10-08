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

    private GameObject[] pawns = new GameObject[16];
    private int currentPawnIndex = 0;
    
    private bool canMove = true;
    
    private bool isSwitching = false;
    private GameObject selectedPawn;

    public void StartGame()
    {
        // Spawn the board prefab 
        SpawnBoardPrefab();

        // Spawn the pawns
        SpawnPawns();
    }
    void SpawnBoardPrefab()
    {
        // Setting the position of board
        Vector3 boardPosition = new Vector3(0, -6.50f, 7f); 

        // Instantiate the board prefab
        GameObject boardInstance = Instantiate(boardPrefab, boardPosition, Quaternion.identity);
        
        // Adjust the rotation of the board
           boardInstance.transform.rotation = Quaternion.Euler(0, -180, 0); 
        
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
                    MoveCurrentPawn(hit.point);
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

void MoveCurrentPawn(Vector3 targetPosition)
{
    if (pawns[currentPawnIndex] != null)
    {
        pawns[currentPawnIndex].transform.position = targetPosition;
    }
}



public void SelectPawn(GameObject pawn)
{
    for (int i = 0; i < pawns.Length; i++)
    {
        if (pawns[i] == pawn)
        {
            currentPawnIndex = i;
            return;
        }
    }
}


}