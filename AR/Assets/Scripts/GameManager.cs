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

       SpawnPawnsForColor(redTiles, Pawn.Team.Red, 4);
       SpawnPawnsForColor(blueTiles, Pawn.Team.Blue, 4);
       SpawnPawnsForColor(yellowTiles, Pawn.Team.Yellow, 4);
       SpawnPawnsForColor(greenTiles, Pawn.Team.Green, 4);
   }

   void SpawnPawnsForColor(GameObject[] baseTiles, Pawn.Team color, int count)
   {
       int pawnCount = Mathf.Min(baseTiles.Length, count);

       for (int i = 0; i < pawnCount; i++)
       {
           GameObject pawnPrefab = GetPawnPrefab(color);

           pawns[currentPawnIndex] = Instantiate(pawnPrefab, baseTiles[i].transform.position, Quaternion.identity);
           Pawn pawnLogic = pawns[currentPawnIndex].GetComponent<Pawn>();
           pawnLogic.gameManager = this;
           pawnLogic.team = color;

           currentPawnIndex++;
       }
   }
   
    GameObject GetPawnPrefab(Pawn.Team color)
    {
        switch (color)
        {
            case Pawn.Team.Red:
                return redPawnPrefab;
            case Pawn.Team.Blue:
                return bluePawnPrefab;
            case Pawn.Team.Yellow:
                return yellowPawnPrefab;
            case Pawn.Team.Green:
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



/*
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pawnPrefab; 
    public LayerMask tileLayer; 

    private GameObject[] pawns = new GameObject[2]; 
    private int currentPawnIndex = 0;

    void Start()
    {
        SpawnPawns();
    }

    void Update()
    {
        // Check if there is at least one touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch phase is began (finger touched the screen)
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
                {
                    MoveCurrentPawn(hit.point);
                }
            }
        }
    }

    void SpawnPawns()
    {
        GameObject[] baseTiles = GameObject.FindGameObjectsWithTag("BaseTile");

        for (int i = 0; i < 16 && i < baseTiles.Length; i++)
        {
            pawns[i] = Instantiate(pawnPrefab, baseTiles[i].transform.position, Quaternion.identity);
            pawns[i].GetComponent<Pawn>().gameManager = this;
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

*/

