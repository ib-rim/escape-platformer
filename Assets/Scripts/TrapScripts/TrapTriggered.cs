using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggered : MonoBehaviour
{
    /*
    public static TrapTriggered instance;

    public Transform FirePoint;
    public GameObject Arrow;

    public void shootArrows()
    {
        Instantiate(Arrow, FirePoint.position, FirePoint.rotation);
    }*/

    public Transform ArrowPoint;
    public GameObject Arrow;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            Shoot();
        }
    }


    public void Shoot()
    {
        Instantiate(Arrow, ArrowPoint.position, ArrowPoint.rotation);
    }
}
