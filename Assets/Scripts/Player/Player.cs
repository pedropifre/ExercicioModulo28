using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public GameObject spawnPoint;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;

    //public Animator animator;

    private float _curentSpeed;
    private bool canJump = false;
    public bool canRun = false;

    private Animator _currentPlayer;



    private void Awake()
    {
        soPlayerSetup.life = 3;
        soPlayerSetup.enemiesKilled = 0;


        canRun = false;
        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

    }



    private void OnPlayerKill()
    {
        
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
        
    }



    public void Update()
    {
        HandleMoviment();
        HandleJump();
    }

    private void HandleMoviment()
    {
        //verificar corrida
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _curentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
        else
        {
            _curentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }
            

        //movimento
        if (canRun)
        {

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("andar");
                myRigidBody.velocity = new Vector2(-_curentSpeed, myRigidBody.velocity.y);
                if(myRigidBody.transform.localScale.x != -1)
                {
                    myRigidBody.transform.DOScaleX(-1, soPlayerSetup.playerSwypeDuration);
                }
                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigidBody.velocity = new Vector2(_curentSpeed, myRigidBody.velocity.y);
                if (myRigidBody.transform.localScale.x != 1)
                {
                    myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwypeDuration);
                }
                _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            }
            else
            {
                _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
            }

        }
        //eliminar fric��o
        if(myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction; 
        }

        if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
          
            canJump = false;

            myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            HandleScaleJump();
           
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
           
        }
        
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        //fazer a anima��o de queda com a fun��o do DoTween para esperar a anterior acabar
    }
    public void SpawnPlayer()
    {
        gameObject.transform.position = spawnPoint.transform.position;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerLive);

        
       
    }
    


}
