using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelInteractable : Interactable
{
    private Room thisRoom;
    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += CheckIfEndRoom;
    }
    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= CheckIfEndRoom;
    }

    private void CheckIfEndRoom()
    {
        if (transform.parent.TryGetComponent(out thisRoom) && !thisRoom.IsEndRoom)
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        GameManager.singleton.EndLevelInteracted();
    }
}
