using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caractercontroller : MonoBehaviour
{
    // Start is called before the first frame update

    public float TLerpMove = 0.1f;
    private Vector3 Destination;
    private bool isMoving = false;
    public bool hasKey = false;
    private GameObject[] allObjects;
    private bool isOnPics = false;
    private GameObject myPics = null;
    public int stepCounter;
    private int previousStepCounter;
    private bool hasNotYetCheckPics = true;
    public Text StepCounter;
    private bool facingRight = true;
    private Animator Animator;
    public GameObject WinScreen;


    //private List<GameObject>

    private void Start()
    {
        UpdateAllObjects();
        foreach (GameObject obj in allObjects)
        {
            //Debug.Log(obj.name);
        }
        Animator = this.gameObject.GetComponent<Animator>();

        
    }

    private void UpdateAllObjects()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    }

    void Update()
    {


        
        if (!isMoving)
        {
            if(isOnPics & hasNotYetCheckPics)
            {

                hasNotYetCheckPics = false;
            }
            if (Input.GetKeyDown("down"))
            {

                Vector3 direction = Vector3.down;
                Move(direction);
            }
            else if (Input.GetKeyDown("up"))
            {

                Vector3 direction = Vector3.up;
                Move(direction);
            }
            else if (Input.GetKeyDown("left"))
            {
                facingRight = false;
                Animator.SetBool("FacingRight",false);
                Vector3 direction = Vector3.left;
                Move(direction);
            }
            else if (Input.GetKeyDown("right"))
            {
                facingRight = true;
                Animator.SetBool("FacingRight", true);
                Vector3 direction = Vector3.right;
                Move(direction);
            }
        }

        TextUpdate();
        EmptyStepCounter();
    }

    public void EmptyStepCounter()
    {
        if (stepCounter <= 0)
        {
            BuildManager.Instance.ResetScene();
        }


    }



    public void TextUpdate()
    {
        StepCounter.text = stepCounter.ToString();
    }

    public void Move(Vector3 direction)
    {
        stepCounter--;
        UpdateAllObjects();
        Vector3 origin = this.transform.position;
        GameObject destination = null;
        foreach (GameObject obj in allObjects)
        {
            if (Vector3.Distance(obj.transform.position, origin + direction) < 0.1f & obj.tag != "Pics" & obj.tag != "Key")
            {
                
                destination = obj;
            }
        }
        
        if (destination == null || destination.tag == "Pics" || destination.tag == "Untagged" || destination.tag == "Key")
        {
            isMoving = true;
            Destination = this.transform.position + direction;
            StartCoroutine(MoveCoroutine());
        }
        else if (destination.tag == "Wall")
        {
            KickAnimation();
            isMoving = true;
            isMoving = false;
            hasNotYetCheckPics = true;
        }
        else if (destination.tag == "Rock")
        {
            KickAnimation();
            isMoving = true;
            destination.GetComponent<RockBehaviour>().MoveRock(direction);
            isMoving = false;
            hasNotYetCheckPics = true;
        }
        else if (destination.tag == "Ennemy")
        {
            KickAnimation();
            isMoving = true;
            
            destination.GetComponent<EnnemyBehaviour>().MoveEnnemy(direction);
            isMoving = false;
            hasNotYetCheckPics = true;
        }
        else if (destination.tag == "Door")
        {
            
            if (hasKey)
            {
                
                destination.GetComponent<DoorBehaviour>().DestroyDoor();
                
                isMoving = true;
                Destination = this.transform.position + direction;
                StartCoroutine(MoveCoroutine());
            }
            else
            {
                KickAnimation();
                
                hasNotYetCheckPics = true;
            }
        }
    }

    public void KickAnimation()
    {
        if (facingRight == true)
        {
            Animator.SetTrigger("KickTriggerRight");
        }
        else
        {
            Animator.SetTrigger("KickTriggerLeft");
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
        hasNotYetCheckPics = true;
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
        if (collision.tag == "Win")
        {
            Debug.Log("Win");
            WinScreen.SetActive(true);
        }
        if (collision.tag == "Pics")
        {
            isOnPics = true;
            myPics = collision.gameObject;
        }
    }

}
