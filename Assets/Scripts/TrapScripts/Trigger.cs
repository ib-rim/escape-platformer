using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Transform Shooter;
    public GameObject Arrow;
    public bool shootLeft;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            //Shoot in direction specified in editor
            if(shootLeft) {
                ShootLeft();
            }
            else {
                ShootRight();
            }
            AudioManager.instance.PlaySFX("ArrowTrap");
        }
    }

    public void ShootLeft()
    {
        GameObject arrow = Instantiate(Arrow, Shooter.position - new Vector3(1,0), Shooter.rotation);
        arrow.GetComponent<Arrow>().direction = -1;
    }

    public void ShootRight()
    {
        GameObject arrow = Instantiate(Arrow, Shooter.position + new Vector3(1,0), Shooter.rotation);
        arrow.GetComponent<Arrow>().direction = 1;
    }
}
