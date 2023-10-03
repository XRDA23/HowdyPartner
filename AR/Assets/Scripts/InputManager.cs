using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IInputManager
{
    [SerializeField] private ImageTrackerScript imageTracker;

    public event Action<CardTypeEnum> OnCardScanned;
    public event Action<Pawn> OnPawnSelected;
    public event Action<Option> OnOptionSelected;
    public event Action<Team> OnFoldAction;

    private void Awake()
    {
        //  bind to event.
        // imageTracker.OnCardDetected += card => OnCardScanned?.Invoke(card);
    }

    public void ScanCard()
    {
        // Initiate card scan
     //   imageTracker.InitiateScan();
    }

    public void SelectPawn(Pawn pawn)
    {
        if (pawn != null)
        {
            OnPawnSelected?.Invoke(pawn);
        }
    }

    public void SelectOption(Option option)
    {
        OnOptionSelected?.Invoke(option);
    }

    public void FoldCards(Team team)
    {
        OnFoldAction?.Invoke(team);
    }

    private void Update()
    {
        // using 3D objects for pawns and they're clickable:
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Pawn hitPawn = hit.transform.GetComponent<Pawn>();
                if (hitPawn != null)
                {
                    SelectPawn(hitPawn);
                }

            }
        }
    }

    // if not using events this will handle card scanning directly from the ImageTrackerScript
    public void HandleCardScanned(CardTypeEnum cardType)
    {
        OnCardScanned?.Invoke(cardType);
    }
    
}
