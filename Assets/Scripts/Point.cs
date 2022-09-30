using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3(0, 0, 0);
    [SerializeField]
    private LayerMask player_layer;
    [SerializeField]
    private Color collectable_color, noncollectable_color;
    [SerializeField]
    private AudioClip pickup_sound;

    private PlayerManager pm;

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        transform.Rotate(axis * Time.deltaTime);

        //collext point 
        bool touchingtoplayer = Physics.CheckSphere(transform.position, 0.2f, player_layer);

        if (pm.canc_ollect)
        {
            //color and rotation speed
            axis.y = 250;
            GetComponent< MeshRenderer > ().material.color = collectable_color;

            if (touchingtoplayer && pm)
            {
                pm.IncreaseHealth(2.0f);

                Camera.main.GetComponent<AudioSource>().PlayOneShot(pickup_sound);

                Destroy(this.gameObject);
            }

        }
        else
        {
            //color and rotation speed
            axis.y = 45;
            GetComponent<MeshRenderer>().material.color = noncollectable_color;

        }
    }
}
