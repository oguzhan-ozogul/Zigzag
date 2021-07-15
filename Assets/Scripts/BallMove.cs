using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMove : MonoBehaviour
{
    Vector3 direction;
    public float speed;
    public GroundSpawner groundSpawner;
    public static bool fall;
    public float speedUpgrade;
    public AudioClip touchSound;
    public AudioClip diamondSound;
    
    


    private void Start()
    {
        direction = Vector3.forward;
        fall = false;
        
        
    }

    public void Update()
    {
        DeathState();

    }
    private void FixedUpdate()
    {
    }
    //Arkadaki zeminlerin top üstünden geçtikten sonra düşmesi
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            collision.gameObject.AddComponent<Rigidbody>();
            groundSpawner.GroundSpawn();
            StartCoroutine(groundDelete(collision.gameObject));
        }
    }
    //düşen zeminlerin 2 saniye sonra destroylanması
    IEnumerator groundDelete(GameObject DeleteGround)
    {
        yield return new WaitForSeconds(2f);
        Destroy(DeleteGround);
    }

    //top zeminin altına indiğinde direk ölmesi
    public void DeathState()
    {
        if (transform.position.y <= -0.2f)
        {
            fall = true;
            

        }
        if (fall == true)
        {
            GameManager.Instance.CurrentGameState = GameManager.GameState.Gameover;
            
            return;
        }
    }

    //Topun tıkladığında yönü ve hareketi
    public void PlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (direction.x == 0)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }
            speed += speedUpgrade * Time.deltaTime;
            GameManager.Instance.UpdateScore();
            AudioSource.PlayClipAtPoint(touchSound, transform.position);
        }
        Vector3 move = direction * Time.deltaTime * speed;
        transform.position += move;
    }
    private void OnTriggerEnter(Collider other)
    {
        CollectDiamond collectDiamond = other.GetComponent<CollectDiamond>();

        if (collectDiamond)
        {
            GameManager.Instance.CollectDiamond();
            AudioSource.PlayClipAtPoint(diamondSound, transform.position);
            Destroy(other.gameObject);
        }
    }
    

}