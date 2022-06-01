using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    public float speed;
    public float angleSpeed;
    public float jumpForce;
    public int attackPower;

    public GameObject sword;
    public GameObject viewPoint;

    public bool isAttackMode=false;
    public GameManager gamemanager;
    

    private bool isPlayerJump=false;
    private bool isPlayerAttack = false;
    private bool isPlayerGet = false;


    public Animator animator;

    
    private Rigidbody rigidbody = null;
    private bool isTalk = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Camera.main.GetComponent<CamController>().SetPlayer(transform);
    }
    void FixedUpdate()
    {

        
    }
    void Update()
    {
        if (!isTalk)
        {
            movePlayer();
        }
        //talk();
        if (!isTalk)
        {
            jumpPlayer();
            //AttackPlayer();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (isAttackMode)
                    isAttackMode = false;
                else
                    isAttackMode = true;
                sword.SetActive(isAttackMode);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)//충돌이 발생했을때
    {
        GameObject collisionObj=collision.gameObject;
        if (collisionObj.CompareTag("floor"))
        {
            
            isPlayerJump = false;
            jumpAnimationUpdated(isPlayerJump);
        }
    }
    
    public void movePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 pastPos = new Vector3(0, 0, 0);

        if (v > 0)
        {
            gameObject.transform.Translate(Vector3.forward  * speed *Time.deltaTime ,Space.World);
            // gameObject.transform.Translate(Vector3.forward * (0.2f * speed), Space.Self);
            pastPos += Vector3.forward  * speed * Time.deltaTime;

        }
        if (v < 0)
        {
            gameObject.transform.Translate(Vector3.back  * speed * Time.deltaTime, Space.World);
            //gameObject.transform.Translate(Vector3.back* (0.2f * speed), Space.Self);
            pastPos += Vector3.back * speed * Time.deltaTime;
        } 
        if (h < 0)
        {
            gameObject.transform.Translate(Vector3.left  * speed * Time.deltaTime, Space.World);
            //gameObject.transform.Translate(Vector3.left*(0.2f*speed), Space.Self);
            pastPos += Vector3.left * speed * Time.deltaTime;
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - angleSpeed, 0);
        }
        if (h > 0)
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            // gameObject.transform.Translate(Vector3.right * (0.2f * speed), Space.Self);
            pastPos += Vector3.right * speed * Time.deltaTime;
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + angleSpeed, 0);
        }

        transform.LookAt(transform.position + pastPos);

        walkAnimationUpdate(h, v);
    }

    public void jumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!isPlayerJump)
        {
            rigidbody.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            isPlayerJump = true;
            jumpAnimationUpdated(isPlayerJump);
        }
        
    }
    /*
    public void AttackPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isPlayerAttack)
        {
            isPlayerAttack = true;
 
            ObjectData objd = null ;
            if (checkFront()!=null)
                objd = checkFront().gameObject.GetComponent<ObjectData>();
            
            if (isAttackMode)
                swordAttackAnimation();                                  
            else
                normalAttackAnimation();

            if (objd != null)
                gamemanager.EnemyAttack(objd, attackPower);
        }
        
    }
    
    public void getItem()//줍기
    {
        if (Input.GetMouseButtonDown(1))//마우스 오른쪽
        {
            ObjectData objd = null;
            if (checkFront() != null)
                objd = checkFront().gameObject.GetComponent<ObjectData>();
            if (objd != null)
            {
                gamemanager.NpcTalk(objd);
            }
        }
    }
    public void talk()
    {
        if ((Input.GetMouseButtonDown(0))&& !EventSystem.current.IsPointerOverGameObject())  //마우스 왼쪽
        {//UI이 위가 아니면.
            ObjectData objd = null;
            if (checkFront() != null)
                objd = checkFront().gameObject.GetComponent<ObjectData>();
            if (objd != null)
            {         
                gamemanager.NpcTalk(objd);
            }
                

        }

    }
    */
    public void walkAnimationUpdate(float h, float v)
    {
        if (h != 0 || v != 0)
            animator.SetBool("iswalk", true);
        else
            animator.SetBool("iswalk", false);
    }
    public void jumpAnimationUpdated(bool j)
    {
        if (j)
        {
            animator.SetTrigger("jump");
            animator.SetBool("isjump", true);
        }
        else
            animator.SetBool("isjump", false);
    }

    public void swordAttackAnimation()
    {
        animator.SetTrigger("swordAttack");
       
    }
    public void normalAttackAnimation()
    {
        animator.SetTrigger("normalAttack");
    }
    public void ChangeIsPlayerAttack()
    {
        isPlayerAttack = false;
    }

    RaycastHit hit;
    public GameObject checkFront()
    {
        GameObject scanObject = null;
        if (Physics.Raycast(viewPoint.transform.position, viewPoint.transform.forward, out hit, 1.0f))
        {
            Debug.DrawRay(viewPoint.transform.position, viewPoint.transform.forward * 1.0f, Color.blue, 0.3f);
            scanObject = hit.collider.gameObject;
        }
        return scanObject;
    }
}
