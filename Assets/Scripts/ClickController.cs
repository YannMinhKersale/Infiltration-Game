using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickController : AgentController
{
    [SerializeField]
    private Camera myCam;
    private int indexTracker = 2;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private RawImage[] itemsImages;

    private Ray rayPickPos; //Déclaré ici pour pouvoir le visualiser, il doit rester accessible entre deux clics


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rayPickPos = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(rayPickPos, out rh))
            {
                FindPathTo(rh.point, moveSpeed);
            }
        }
        if (indexTracker - 2 == 4)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(rayPickPos.origin, rayPickPos.direction * 100);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            switch (other.gameObject.GetComponent<Item>().name)
            {
                case "chalice(Clone)":
                    itemsImages[0].color = new Color(255, 255, 255, 100);
                    break;
                case "crown(Clone)":
                    itemsImages[1].color = new Color(255, 255, 255, 100);
                    break;
                case "key(Clone)":
                    itemsImages[2].color = new Color(255, 255, 255, 100);
                    break;
                case "lock(Clone)":
                    itemsImages[3].color = new Color(255, 255, 255, 100);
                    break;
            }
            other.gameObject.GetComponent<Item>().tag = "Untagged";
            other.gameObject.GetComponent<Item>().Picked(gameObject, indexTracker);
            indexTracker++;
        }
    }
}