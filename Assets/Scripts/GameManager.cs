using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerAlive=true;

    public static GameManager gm;

    private GameObject player;

    [SerializeField]
    private Transform playerStartPoint;

    [SerializeField]
    private CameraController cc;

    [SerializeField]
    private float diffuculty;

    public float distance;

    private void Awake()
    {
        gm = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        //load scene when we click
        if (!isPlayerAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        //Check player distance
        if (player!=null)
        {
        distance = Vector3.Distance(player.transform.position, playerStartPoint.position);
        UIManager.ui_m.SetDistanceValue(distance);
        }

        cc.speed += Time.timeSinceLevelLoad / 10000 * diffuculty;
        cc.speed = Mathf.Clamp(cc.speed, 1, 50);

    }
}
