using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caractercontroller : MonoBehaviour
{
    // Start is called before the first frame update

    public float TLerpMove = 0.075f;
    private Vector3 Destination;
    private bool isMoving = false;
    public bool hasKey = false;
    private GameObject[] allObjects;
    private bool isOnPics = false;
    private GameObject myPics = null;
    public int stepCounter;
    private int previousStepCounter;
    private bool hasNotYetCheckPïcs = true;

    //private List<GameObject>

    private void Start()
    {
        UpdateAllObjects();
        foreach (GameObject obj in allObjects)
        {
            Debug.Log(obj.name);
        }
    }

    private void UpdateAllObjects()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    }

    void Update()
    {
        if (!isMoving)
        {
            if(isOnPics & hasNotYetCheckPïcs)
            {
                Debug.Log("CheckPicsState");
                hasNotYetCheckPïcs = false;
            }
            if (Input.GetKeyDown("s"))
            {
                Debug.Log("bas");
                Vector3 direction = Vector3.down;
                Move(direction);
            }
            else if (Input.GetKeyDown("z"))
            {
                Debug.Log("haut");
                Vector3 direction = Vector3.up;
                Move(direction);
            }
            else if (Input.GetKeyDown("q"))
            {
                Debug.Log("goche");
                Vector3 direction = Vector3.left;
                Move(direction);
            }
            else if (Input.GetKeyDown("d"))
            {
                Debug.Log("drouate");
                Vector3 direction = Vector3.right;
                Move(direction);
            }
        }
        if (isOnPics) Debug.Log("There is pics behind me ! Heeeeeelllp !");
    }

    public void Move(Vector3 direction)
    {
        stepCounter++;
        UpdateAllObjects();
        Vector3 origin = this.transform.position;
        GameObject destination = null;
        foreach (GameObject obj in allObjects)
        {
            if (Vector3.Distance(obj.transform.position, origin + direction) < 0.1f & obj.tag != "Pics" & obj.tag != "Key")
            {
                Debug.Log(obj.name);
                destination = obj;
            }
        }
        //Debug.Log(Hit.collider.gameObject.tag);
        if (destination == null || destination.tag == "Pics" || destination.tag == "Untagged")
        {
            isMoving = true;
            Destination = this.transform.position + direction;
            StartCoroutine(MoveCoroutine());
        }
        else if (destination.tag == "Wall")
        {
            isMoving = true;
            Debug.Log("There is a Wall and your not yet a ghost...");
            isMoving = false;
            hasNotYetCheckPïcs = true;
        }
        else if (destination.tag == "Rock")
        {
            isMoving = true;
            Debug.Log("TryMoveRock");
            destination.GetComponent<RockBehaviour>().MoveRock(direction);
            isMoving = false;
            hasNotYetCheckPïcs = true;
        }
        else if (destination.tag == "Ennemy")
        {
            isMoving = true;
            Debug.Log("Pushennemy");
            destination.GetComponent<EnnemyBehaviour>().MoveEnnemy(direction);
            isMoving = false;
            hasNotYetCheckPïcs = true;
        }
        else if (destination.tag == "Door")
        {
            Debug.Log("TryDestroyDoor");
            if (hasKey)
            {
                Debug.Log("Can Destroy theDoor");
                destination.GetComponent<DoorBehaviour>().DestroyDoor();
                isMoving = true;
                Destination = this.transform.position + direction;
                StartCoroutine(MoveCoroutine());
            }
            else
            {
                Debug.Log("Cannot Destroy theDoor ! It's too strong for you !");
                hasNotYetCheckPïcs = true;
            }
        }
    }


    IEnumerator MoveCoroutine()
    {

        while (Vector3.Distance(this.transform.position, Destination) > 0.001f)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, Destination, TLerpMove);
            yield return null;
        }
        this.transform.position = Destination;
        isMoving = false;
        hasNotYetCheckPïcs = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Pics")
        {
            isOnPics = false;
            myPics = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            Destroy(collision.gameObject);
            hasKey = true;
        }
        if (collision.tag == "Pics")
        {
            isOnPics = true;
            myPics = collision.gameObject;
        }
    }

}
