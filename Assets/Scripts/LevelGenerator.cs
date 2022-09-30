using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region SerializeFilds
    [Header("Cylinder atributies")]
    [SerializeField]
    private GameObject cylinder;

    [SerializeField]
    private Color enemycylinder;

    [SerializeField]
    private float minradius, maxRadius;
    #endregion

    private GameObject prevous_cylinder;
 
    #region Functions
    private float FindRadius(float minRadius, float maxRadius)
    {
        float radius = Random.Range(minRadius, maxRadius);
        if (prevous_cylinder != null)
        {
            while (Mathf.Abs(radius - prevous_cylinder.transform.localScale.x) < 0.6f)
            {
                radius = Random.Range(minRadius, maxRadius);

            }
        }
        return radius;
    }

    public void SpawnCylinder()
    {
        float radius = FindRadius(minradius, maxRadius);
        float height = Random.Range(2f, 6f);

        cylinder.transform.localScale = new Vector3(radius, height, radius);

        //fist slinder
        if (prevous_cylinder == null)
        {
            prevous_cylinder = Instantiate(cylinder, Vector3.zero, Quaternion.identity);
        }
        //all other cylinder
        else
        {
            float spawpoint = prevous_cylinder.transform.position.z + prevous_cylinder.transform.localScale.y + cylinder.transform.localScale.y;
            prevous_cylinder = Instantiate(cylinder, new Vector3(0, 0, spawpoint), Quaternion.identity);
            if (Random.value < 0.1f)
            {
                prevous_cylinder.GetComponent<Renderer>().material.color = enemycylinder;
                prevous_cylinder.tag = "Enemy";
            }
        }

        prevous_cylinder.transform.Rotate(90, 0, 0);

    }
    #endregion
}
