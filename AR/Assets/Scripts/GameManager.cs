using Board;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask tileLayer;

    [SerializeField] private GameObject redPawnPrefab;
    [SerializeField] private GameObject bluePawnPrefab;
    [SerializeField] private GameObject yellowPawnPrefab;
    [SerializeField] private GameObject greenPawnPrefab;

    private GameObject[] pawns = new GameObject[16];
    private int currentPawnIndex = 0;
    
    private bool canMove = true;
    
    private bool isSwitching = false;
    private GameObject selectedPawn;

    void Start()
    {
        SpawnPawns();
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
