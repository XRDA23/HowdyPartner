using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pawnPrefab; 
    public LayerMask tileLayer; 

    private GameObject[] pawns = new GameObject[16]; 
    private int currentPawnIndex = 0;

    void Start()
    {
        SpawnPawns();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                MoveCurrentPawn(hit.point);
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

