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
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            collision.gameObject.AddComponent<Rigidbody>();
            groundSpawner.groundSpawn();
            StartCoroutine(groundDelete(collision.gameObject));
        }
    }
    IEnumerator groundDelete(GameObject DeleteGround)
    {
        yield return new WaitForSeconds(2f);
        Destroy(DeleteGround);
    }

    public void DeathState()
    {
        if (transform.position.y <= 0.5f)
        {
            fall = true;
        }
        if (fall == true)
        {
            GameManager.Instance.CurrentGameState = GameManager.GameState.Gameover;
            return;
        }
    }

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
            //AudioSource.PlayClipAtPoint(GameManager.gameManager.jumpSound, GameManager.gameManager.player.transform.position);
            //GameManager.gameManager.player.CollectCube();
            Destroy(other.gameObject);
        }
    }

}