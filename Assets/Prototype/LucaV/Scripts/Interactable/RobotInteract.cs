using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInteract : Interactable
{

    public override void Interact()
    {
        GetComponent<Robot>().Interact();
        Destroy(canvas);
    }

}
