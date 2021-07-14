using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject lastGround;
    public GameObject diamond;

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            groundSpawn();
            
        }
    }

    public void groundSpawn()
    {
        Vector3 _direction;
        if(Random.Range(0,2) == 0)
        {
            _direction = Vector3.left;
            diamond = Instantiate(diamond, lastGround.transform.position + new Vector3(0, 0.7f, 0), diamond.transform.rotation);
        }
        else
        {
            _direction = Vector3.forward;
        }
        lastGround = Instantiate(lastGround, lastGround.transform.position + _direction, lastGround.transform.rotation,transform.parent);
        
        
    }

}
