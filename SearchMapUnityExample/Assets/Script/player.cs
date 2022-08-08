using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Singleton<Player>
{
    public PlayerUI playerUI;
   
    public Stat stat;
    public float angleSpeed;
    public float jumpForce;

    public GameObject viewPoint;

    public int weaponMode=0;
    

    private bool isPlayerJump=false;
    private bool isPlayerAttack = false;
    private bool isPlayerGet = false;


    public Animator animator;
    private CamController camController;
    
    private Rigidbody rigidbody = null;
    private bool isTalk = false;
    private ScanObject scanObject=null;

    private Weapon[] weapons;

    public delegate void MoveHandler(Vector2 currentPos);//center board가 맵설정되었을때 호출
    public event MoveHandler OnChangedPos;
    // Start is called before the first frame update
    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>();
        for(int i=0; i<weapons.Length; i++)
        {
            weapons[i].Init(stat.Damage);
            weapons[i].gameObject.SetActive(false);
        }
        SetWeapon(0);

        rigidbody = GetComponent<Rigidbody>();
        camController = Camera.main.GetComponent<CamController>();
        camController.SetPlayer(transform);

        stat.OnChanged += delegate { playerUI.UpdateHP(stat.CurrentHp); };
        stat.OnDie += delegate { GameManager.Instance.Reset(); }; 
        stat.Init();
       

    }
    void FixedUpdate()
    {

        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {//카메라 변경
            camController.ChangeMode((camController.GetCamMode() + 1) % 2);
        }
        if (!isPlayerAttack)
        {
            movePlayer();
            if (Input.GetKeyDown(KeyCode.Space) && !isPlayerJump)
            {
                jumpPlayer();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                if (weaponMode == 1)
                {
                    SetWeapon(0);
                }
                else
                {
                    SetWeapon(1);
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (weaponMode == 2)
                {
                    weaponMode = 0;
                }
                else
                {
                    weaponMode = 2;
                }
                SetWeapon(weaponMode);

            }
        }
        if (isPlayerJump)
        {
            DrawRayUnderPlayer();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AttackPlayer();
        }

        //앞 검사
        ScanObject newScanObject = checkFront();
        if (newScanObject != null)
        {
            scanObject = newScanObject;
            scanObject.OnOutLine();
        }
        else
        {
            if (scanObject != null)
                scanObject.Clear();
            scanObject = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            scanObject = checkFront();
            if (scanObject != null)
            {
                scanObject.Run();
                scanObject = null;
            }
        }
    }
    
    private void DrawRayUnderPlayer()
    {
        RaycastHit hit;
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절반 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics.BoxCast(transform.position+transform.up,new Vector3(0.5f,0.1f,0.5f), -transform.up, 
            out hit, transform.rotation, 0.9f, LayerMask.GetMask("platform"));

        if (isHit)
        {
            isPlayerJump = false;
            jumpAnimationUpdated(isPlayerJump);
            Board b;
            if(hit.collider.gameObject.TryGetComponent<Board>(out b))
            {
                OnChangedPos?.Invoke(b.Pos);
            }

        }
        else
        {
        }
    }
    void OnDrawGizmos()
    {   //디버그용 Ray체크
        //프레임 마다 호출됨.

         RaycastHit hit;
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절반 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics.BoxCast(transform.position+transform.up,new Vector3(0.5f,0.1f,0.5f), -transform.up, 
            out hit, transform.rotation, 0.9f,LayerMask.GetMask("platform"));

        Gizmos.color = Color.red;
        if (isHit)
        {
         
            Gizmos.DrawRay(transform.position + transform.up, -transform.up * hit.distance);
            Gizmos.DrawWireCube(transform.position +transform.up - transform.up * (hit.distance+0.05f), new Vector3(0.5f, 0.1f, 0.5f));
        }
        else
        {
            //Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.red);
            Gizmos.DrawRay(transform.position + transform.up, -transform.up * 0.9f);
        }
    
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position + transform.up, -transform.up * hit.distance);
        //Gizmos.DrawWireCube(transform.position + transform.up - transform.up * hit.distance, new Vector3(0.5f, 0.1f, 0.5f));
    }
    private void OnCollisionEnter(Collision collision)//충돌이 발생했을때
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
           
            //Debug.Log(e.name + " 의 체력 " + e.stat.CurrentHp);
        }
    }
    
    public void movePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 pastPos = new Vector3(0, 0, 0);

        Vector3 frontVec = camController.transform.forward;
        frontVec.y = 0;
        if (v > 0)
        { 
            gameObject.transform.Translate(frontVec * stat.Speed *Time.deltaTime,Space.World);
            // gameObject.transform.Translate(Vector3.forward * (0.2f * speed), Space.Self);
            pastPos += frontVec;

        }
        if (v < 0)
        {
            gameObject.transform.Translate(-frontVec * stat.Speed * Time.deltaTime, Space.World);
            //gameObject.transform.Translate(Vector3.back* (0.2f * speed), Space.Self);
            pastPos += -frontVec;
        } 
        if (h < 0)
        {
            
            gameObject.transform.Translate(Vector3.Cross(frontVec,transform.up)  * stat.Speed * Time.deltaTime, Space.World);
            //gameObject.transform.Translate(Vector3.left*(0.2f*speed), Space.Self);
            pastPos += Vector3.Cross(frontVec, transform.up);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - angleSpeed, 0);
        }
        if (h > 0)
        {
            gameObject.transform.Translate(-Vector3.Cross(frontVec, transform.up)* stat.Speed * Time.deltaTime, Space.World);
            // gameObject.transform.Translate(Vector3.right * (0.2f * speed), Space.Self);
            pastPos += -Vector3.Cross(frontVec, transform.up);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + angleSpeed, 0);
        }

        //transform.LookAt(transform.position + pastPos);
        if (camController.GetCamMode() == 0)
            transform.LookAt(transform.position + frontVec);
        else if(camController.GetCamMode() == 1)
        {
            transform.LookAt(transform.position + pastPos);
        }

        walkAnimationUpdate(h, v);
    }

    public void jumpPlayer()
    {
        //2단점프 방지
        transform.Translate(transform.up * 0.05f);

        rigidbody.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
        isPlayerJump = true;
        jumpAnimationUpdated(isPlayerJump);
    }
    private void SetWeapon(int i)
    {
        weapons[weaponMode].gameObject.SetActive(false);

        weaponMode = i;
        weapons[weaponMode].gameObject.SetActive(true);
        weapons[weaponMode].SetColliderOn(false);
        
    }
    public void AttackPlayer()
    {
        if (!isPlayerAttack)
        {
            isPlayerAttack = true;


            if (weaponMode==0)
            {
                weapons[0].SetColliderOn(true);
                animator.CrossFade("NormalAttack", 0.0f);
               
            }
            else if (weaponMode == 1)
            {
                weapons[1].SetColliderOn(true);
                animator.CrossFade("SwordAttack", 0.0f);
            }


        }
        
    }
    public void ChangeIsPlayerAttack()
    {
        weapons[weaponMode].SetColliderOn(false);
        isPlayerAttack = false;

    }
    /*
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



    RaycastHit hit;
    public ScanObject checkFront()
    {
        GameObject sc= null;
        if (Physics.Raycast(viewPoint.transform.position, viewPoint.transform.forward, out hit, 1.5f))
        {
            Debug.DrawRay(viewPoint.transform.position, viewPoint.transform.forward * 1.0f, Color.blue, 1.5f);
            sc = hit.collider.gameObject;
            ScanObject obj=null;
            sc.TryGetComponent<ScanObject>(out obj);
            
            return obj;

        }
        return null;
    }
}
