using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField]
    private float animAmplitude = 1f, animSpeed = 0.7f, rotationSpeed = 20;
    [SerializeField]
    private float lerpFactor = 0;
    private bool positive = true, follow = false;
    private Vector3 bottomPos, upPos;
    private Transform followPos;

    private void Start()
    {
        bottomPos = transform.position;
        upPos = new Vector3(bottomPos.x, bottomPos.y + animAmplitude, bottomPos.z);
    }

    void Update()
    {
        if (!follow)
        {
            //Floating anim
            lerpFactor += positive ? Time.deltaTime * animSpeed : -Time.deltaTime * animSpeed;
            if (lerpFactor >= 1) positive = false;
            if (lerpFactor <= 0) positive = true;
            float lerp = 0.5f * (1 - Mathf.Cos(lerpFactor * Mathf.PI));
            transform.position = Vector3.Lerp(bottomPos, upPos, lerp);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position ,followPos.position, Time.deltaTime * 2.5f);
        }
        //Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void Picked(GameObject player, int trackerIndex)
    {
        if (!follow)
        {
            transform.localScale = transform.localScale / 2;
            followPos = player.transform.GetChild(trackerIndex);
            transform.position = followPos.position;
            follow = true;
        }
    }
}
