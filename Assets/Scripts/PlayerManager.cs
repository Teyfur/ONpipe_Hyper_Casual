using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region constants
    private const float size_scalel = 0.3f;
    private const float checker_radius = 0.18f;
    private const float offset = 0.2f;
    #endregion

    #region serializefields
    [SerializeField]
    private Vector3 default_size = new Vector3(1, 1, 1);

    [SerializeField]
    private LayerMask cylinder_layer;

    [SerializeField]
    private AudioClip click_sound, death_sound;

    #endregion

    [HideInInspector]
    public bool canc_ollect=false;



    public float healt=30.0f;

    #region Unity
    private void Update()
    {
        //Define cylinder and radius
            Transform cyl = Physics.OverlapSphere(transform.position, checker_radius, cylinder_layer)[0].transform;
            float cyl_radius = cyl.localScale.x*size_scalel;

        //chechk for player Deaths

        if (healt<=0)
        {
            Death();
        }
        if (cyl_radius>transform.localScale.y)
        {
            Death();
        }
        if (cyl.CompareTag("Enemy"))
        {
            if (cyl_radius+offset>transform.localScale.y)
            {
                Death();
            }
        }

        //check can collect
        if (cyl_radius + offset > transform.localScale.y)
        {
            canc_ollect = true;
        }
        else
        {
            canc_ollect = false;
        }
        ChangeRingRadius(cyl_radius);

        healtcounter();

    }
    #endregion

    #region Functions
    private void Death()
    {
        //stop camera controller
        if (Camera.main != null)
        {
            Camera.main.GetComponent<CameraController>().enabled = false;
        }

        //Game over UI
        UIManager.ui_m.OpenGameOverUI();

        //Player Alive boolean
        GameManager.gm.isPlayerAlive = false;

        //play death sound
        Camera.main.GetComponent<AudioSource>().PlayOneShot(death_sound, 0.5f);

        //save high score
        if (GameManager.gm.distance>PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", GameManager.gm.distance);
        }

        //set high score text
        UIManager.ui_m.SetHigScoreText();

        //destroy player game object 
        Destroy(this.gameObject);


    }

    private void ChangeRingRadius(float cyl_radius)
    {
        if (Input.touchCount>0)
        {

            Touch touch = Input.GetTouch(0);

            //play sound effect
        if (touch.phase==TouchPhase.Began) //Input.GetMouseButtonDown(0)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(click_sound, 0.1f);

        }
        //when touched screen
           if(touch.phase==TouchPhase.Stationary)  //Input.GetMouseButton(0)
        {
            //set size of ring
            Vector3 target_vector= new Vector3(default_size.x, cyl_radius, cyl_radius);
            transform.localScale = Vector3.Slerp(transform.localScale, target_vector, 0.125f);

            
        }


        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, default_size, 0.125f);

        }
    }

    private void healtcounter()
    {
      
             healt = Mathf.Clamp(healt, -1, 10.0f);       
        if (healt>=0)
        {

            healt -= Time.deltaTime;

            UIManager.ui_m.SetPlayerHealt(healt);
        }
    }

    public void IncreaseHealth(float value)
    {
        healt += value;
    }
    #endregion

}
