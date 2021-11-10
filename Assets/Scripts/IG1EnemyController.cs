using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IG1EnemyController : AgentController
{
    private bool idleAnim = false;
    private float time = 0, idleTime;
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<AISenseSight>().AddSenseHandler(new AISense<SightStimulus>.SenseEventHandler(HandleSight));
        GetComponent<AISenseSight>().AddObjectToTrack(player);
        GetComponent<AISenseHearing>().AddSenseHandler(new AISense<HearingStimulus>.SenseEventHandler(HandleHearing));
        GetComponent<AISenseHearing>().AddObjectToTrack(player);
    }

    private void Update()
    {
        if (!idleAnim)
        {
            Vector3 dest = MoveAnim();
            time = Time.time;
            idleTime = Random.Range(15, 25);
            idleAnim = true;
        }
        else
        {
            Idle();
            if (Time.time > time + idleTime)
                idleAnim = false;
        }      
    }

    private Vector3 MoveAnim()
    {
        Vector3 actualPosition = transform.position;
        Vector3 dest = new Vector3(actualPosition.x + Random.Range(-15, 15), actualPosition.y, actualPosition.z + Random.Range(-15, 15));
        float speed = Random.Range(0.5f, 1.5f);
        FindPathTo(dest, speed);
        return dest;
    }

    private void Idle()
    {
        float rotSpeed = Random.Range(5, 15);
        if (Random.Range(0, 1) <= 0.5) rotSpeed *= -1;
        Quaternion newRot = new Quaternion();
        newRot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Time.deltaTime * rotSpeed, transform.rotation.eulerAngles.z);
        Rotate(newRot);
    }


    void HandleSight(SightStimulus sti, AISense<SightStimulus>.Status evt)
    {
        if (evt == AISense<SightStimulus>.Status.Enter)
            Debug.Log("Objet " + evt + " vue en " + sti.position);

        FindPathTo(sti.position);

        if ((sti.position - transform.position).sqrMagnitude < 2 * 2)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void HandleHearing(HearingStimulus sti, AISense<HearingStimulus>.Status evt)
    {
        if (evt == AISense<HearingStimulus>.Status.Enter)
            Debug.Log("Objet " + evt + " ouïe en " + sti.position);
        FindPathTo(sti.position);
    }
}