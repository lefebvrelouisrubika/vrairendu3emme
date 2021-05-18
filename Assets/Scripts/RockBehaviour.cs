using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{


    public float TLerpMove = 0.075f;
    private Vector3 Destination;
    private bool isMoving = false;
    private bool canMove = false;
    private Vector3 Direction;
    private GameObject[] allObjects;
    //private List<GameObject>

    private void Start()
    {
        UpdateAllObjects();
        foreach (GameObject obj in allObjects)
        {
            Debug.Log(obj.name);
        }
    }
    private void Update()
    {

        if (canMove) Move(Direction);
    }

    private void UpdateAllObjects()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    }
    public void MoveRock(Vector3 direction)
    {
        Debug.Log("AuthorizeMoveRock");
        Direction = direction;
        canMove = true;
    }
    public void Move(Vector3 direction)
    {
        UpdateAllObjects();

        if (!isMoving)
        {
            Debug.Log("MoveRock");
            Vector3 origin = this.transform.position;
            GameObject destination = null;
            foreach (GameObject obj in allObjects)
            {
                if (Vector3.Distance(obj.transform.position, origin + direction) < 0.1f)
                {
                    Debug.Log(obj.name);
                    destination = obj;
                }
            }
            //Debug.Log(Hit.collider.gameObject.tag);
            if (destination == null || destination.tag == "Untagged" || destination.tag == "Key")
            {
                isMoving = true;
                Destination = this.transform.position + direction;
                StartCoroutine(MoveCoroutine());
            }
            else if (destination.tag == "Wall")
            {
                canMove = false;
            }
            else if (destination.tag == "Rock")
            {

                canMove = false;

            }
            else if (destination.tag == "Ennemy")
            {

                canMove = false;

            }
            else if (destination.tag == "Door")
            {

                canMove = false;

            }

        }


    }
    IEnumerator MoveCoroutine()
    {
        Debug.Log("RockCorout");
        while (Vector3.Distance(this.transform.position, Destination) > 0.001f)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, Destination, TLerpMove);
            yield return null;
        }
        this.transform.position = Destination;
        isMoving = false;
        canMove = false;

    }


}
