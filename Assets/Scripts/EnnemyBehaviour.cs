using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour
{


    public float TLerpMove= 0.075f;
    private Vector3 Destination;
    private bool isMoving = false;
    private bool canMove = false;
    private Vector3 Direction;
    private GameObject[] allObjects;
    private bool facingRight = true;
    private Animator Animator;
    
    private void Start()
    {
        UpdateAllObjects();
        foreach (GameObject obj in allObjects)
        {
            Debug.Log(obj.name);
        }
        Animator = this.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {

        if (canMove) Move(Direction);
    }

    private void UpdateAllObjects()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    }
    public void MoveEnnemy(Vector3 direction)
    {
        Debug.Log("AuthorizeMoveRock");
        Direction = direction;
        canMove = true;
    }
    public void Move(Vector3 direction)
    {
        UpdateAllObjects();
        AnimationsGestion();
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

            if (destination == null || destination.tag == "Untagged" || destination.tag == "Key")
            {
                isMoving = true;
                Destination = this.transform.position + direction;
                MoveAnimation();
                StartCoroutine(MoveCoroutine());
            }
            else if (destination.tag == "Wall")
            {
                
                canMove = false;
                Destroy(this.gameObject);
            }
            else if (destination.tag == "Rock")
            {
                
                canMove = false;
                Destroy(this.gameObject);

            }
            else if (destination.tag == "Ennemy")
            {
                
                canMove = false;

            }
            else if (destination.tag == "Door")
            {
               
                canMove = false;
                Destroy(this.gameObject);


            }
            else if (destination.tag == "Win")
            {

                canMove = false;
                Destroy(this.gameObject);


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
        canMove = false;

    }
    public void AnimationsGestion()
    {

        if (Direction == Vector3.right)
        {
            facingRight = false;
            Animator.SetBool("FacingRight", false);
        }
        if (Direction == Vector3.left)
        {
            facingRight = true;
            Animator.SetBool("FacingRight", true);
        }
    }

    public void MoveAnimation()
    {
        if (facingRight == true)
        {
            Animator.SetTrigger("PushedRight");
        }
        else
        {
            Animator.SetTrigger("PushedLeft");
        }

    }

}
