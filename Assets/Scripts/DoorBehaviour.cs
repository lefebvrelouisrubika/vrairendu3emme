using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public void DestroyDoor()
    {
        Debug.Log("DestroyDoor");
        Destroy(this.gameObject);
    }
}
