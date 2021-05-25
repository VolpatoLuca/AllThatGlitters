using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelInteractable : MonoBehaviour
{
    private Room thisRoom;
    private void Start()
    {
        if(transform.parent.TryGetComponent(out thisRoom) && thisRoom.IsEndRoom)
        {
            Instantiate(GameManager.singleton.endLevelInteractable, transform.position, Quaternion.identity);
        }
    }
}
