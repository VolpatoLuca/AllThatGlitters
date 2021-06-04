using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EndLevelInteractable : Interactable
{
    private Room thisRoom;
    [SerializeField] private GameObject vfx;
    private void OnEnable()
    {
        GameManager.singleton.RoomsGenerated += CheckIfEndRoom;
        vfx = GetComponentInChildren<VisualEffect>().gameObject;
        if (!vfx)
            print(transform.position);
        vfx.SetActive(false);
    }
    private void OnDisable()
    {
        GameManager.singleton.RoomsGenerated -= CheckIfEndRoom;
    }

    private void CheckIfEndRoom()
    {
        if (transform.parent.TryGetComponent(out thisRoom))
        {
            if (!thisRoom.IsEndRoom)
                Destroy(gameObject);
            else
                vfx.SetActive(true);
        }
    }
    public override void Interact()
    {
        GameManager.singleton.EndLevelInteracted();
    }
}
