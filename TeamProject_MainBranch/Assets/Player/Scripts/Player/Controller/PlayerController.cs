using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce = 5.0f;

    // 퀘스트 시, 입력을 막는 bool 타입 변수 생성
    [SerializeField]
    bool doingQuest = false;

    // Movement Inputs
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;

    // Skills Inputs
    bool dodge;
    bool heal;
    bool buf;
    bool rangeSkill;
    bool meleeSkill;

    // Skills Availability
    bool sa_dodge = false;
    bool sa_heal = false;
    bool sa_buf = false;
    bool sa_rangeSkill = false;
    bool sa_meleeSkill = false;


    // Skill Cool Time
    float ct_dodge = 0;
    float ct_heal = 0;
    float ct_buf = 0;
    float ct_rangeSkill = 0;
    float ct_meleeSkill = 0;

    // Skill Duration
    float sd_heal = 10.0f;
    float sd_buf = 5.0f;

    // Heal Cycle
    float healCycle = 0.0f;
    bool isHealing = false;

    bool isPower = false;

    // Skill Effects & Projectile
    [SerializeField]
    GameObject ef_heal;
    [SerializeField]
    GameObject ef_buf;
    [SerializeField]
    GameObject ef_meleeSkill;
    [SerializeField]
    GameObject ef_dodge;
    [SerializeField]
    GameObject pj_rangeSkill;

    [SerializeField]
    bool isJump;
    int attackCount = 0;
    bool isLastAttackEnd = true;
    float checkTime = 0;

    public Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;
    public Transform cameraTransform;

    [SerializeField]
    GameObject Sword;
    void Start()
    {

    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        Sword = GameObject.Find("Sword");
    }


    void Update()
    {

        if (!doingQuest)
        {
            GetInput();
            Move();
            Turn();
            //Jump();
            SkillInput();
            MouseInput();

            Healing();
        }
        UpdateCoolTime();
    }

    void PlayerMovementFix(bool bValue)
    {
        doingQuest = bValue;
        if (bValue)
        {
            speed = 0;
            anim.SetBool("isWalk", false);
        }
        else
            speed = 4;
    }

    void UpdateCoolTime()
    {
        // buf
        if (ct_buf > 10.0f) sa_buf = true;
        else ct_buf += Time.deltaTime;

        // heal
        if (ct_heal > 10.0f) sa_heal = true;
        else ct_heal += Time.deltaTime;

        // dodge
        if (ct_dodge > 10.0f) sa_dodge = true;
        else ct_dodge += Time.deltaTime;

        // melee Skill
        if (ct_meleeSkill > 10.0f) sa_meleeSkill = true;
        else ct_meleeSkill += Time.deltaTime;

        // range Skill
        if (ct_rangeSkill > 10.0f) sa_rangeSkill = true;
        else ct_rangeSkill += Time.deltaTime;
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        dodge = Input.GetKeyDown(KeyCode.E);
        heal = Input.GetKeyDown(KeyCode.F);
        buf = Input.GetKeyDown(KeyCode.C);
        rangeSkill = Input.GetKeyDown(KeyCode.R);
        meleeSkill = Input.GetKeyDown(KeyCode.Q);
    }

    void MouseInput()
    {
        if (wDown || jDown) return;
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Defence(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Defence(false);
        }
    }

    void Defence(bool isDefending)
    {
        anim.SetBool("defence", isDefending);
        attackEnd();
    }

    void Attack()
    {
        if (isLastAttackEnd)
        {
            attackCount = (attackCount + 1) % 4;

            anim.SetInteger("attCount", attackCount);

            anim.SetBool("isAttack", true);

            if (attackCount == 3)
                isLastAttackEnd = false;
        }
    }

    void attackEnd()
    {
        attackCount = 0;
        anim.SetInteger("attCount", 0);
        anim.SetBool("isAttack", false);
        isLastAttackEnd = true;
    }

    void Move()
    {
        if (anim.GetBool("defence"))
            return;
        Quaternion v3Rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);

        moveVec = v3Rotation * new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 1f : 0.3f) * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isRun", wDown);
        if (wDown)
            attackEnd();
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rigid.AddForce(moveVec * 10, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
            attackEnd();
        }
    }

    void SkillInput()
    {
        if (dodge && sa_dodge)
            Dodge();
        if (rangeSkill && sa_rangeSkill)
            RangeSkill();
        if (meleeSkill && sa_meleeSkill)
            MeleeSkill();
        if (heal && sa_heal)
            Heal();
        if (buf && sa_buf)
            Buf();
    }

    void Dodge()
    {
        ct_dodge = 0.0f;
        sa_dodge = false;

        // 순간이동 이펙트 1회 출력
        ef_dodge.SetActive(true);
        Invoke("EndDodge", 1.0f);
        rigid.AddForce(moveVec * 200, ForceMode.Impulse);

    }

    void Heal()
    {
        ct_heal = 0.0f;
        sa_heal = false;

        ef_heal.SetActive(true);

        isHealing = true;
        Invoke("EndHeal", sd_heal);
    }

    void Healing()
    {
        if (!isHealing) return;


        // HP 일정량 회복
        healCycle += Time.deltaTime;

        // 강화효과가 적용중이라면 추가적인 힐을 부여한다.
        int AdditionalHeal = 0;

        if (isPower)
            AdditionalHeal = 1;

        if (healCycle > 1.95f)
        {
            // 플레이어의 체력 일정량 회복
            this.GetComponent<PlayerStatus>().HealPlayer(2 + AdditionalHeal);
            Debug.Log("Heal");
            healCycle = 0.0f;
        }
    }

    void Buf()
    {
        ct_buf = 0.0f;
        sa_buf = false;

        ef_buf.SetActive(true);
        Invoke("EndBuf", sd_buf);

        isPower = true;

        // 모든 스탯 증가
        this.gameObject.GetComponent<PlayerStatus>().setPlayerDamage(15);
    }

    void RangeSkill()
    {
        ct_rangeSkill = 0.0f;
        sa_rangeSkill = false;

        // 플레이어가 바라보는 방향으로 날아가는 원거리 투사체 생성
        // 공격 1 애니메이션 1회 출력

        anim.SetTrigger("rangeSkill");

        Instantiate(pj_rangeSkill, transform.position, transform.rotation);
    }

    void MeleeSkill()
    {
        ct_meleeSkill = 0.0f;
        sa_meleeSkill = false;

        // 근거리 공격 이펙트 1회 출력
        // 공격 3 애니메이션 1회 출력
        ef_meleeSkill.SetActive(true);

        anim.SetTrigger("meleeSkill");

        Invoke("EndMeleeSkill", 1.0f);

    }

    void EndHeal()
    {
        ef_heal.SetActive(false);
        healCycle = 0.0f;
        isHealing = false;
    }
    void EndBuf()
    {
        ef_buf.SetActive(false);
        isPower = false;
        this.gameObject.GetComponent<PlayerStatus>().setPlayerDamage(5);
    }

    void EndDodge()
    {
        ef_dodge.SetActive(false);
    }

    void EndMeleeSkill()
    {
        ef_meleeSkill.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJump", false);
            isJump = false;
            attackEnd();
        }
    }

    void OnAttack()
    {
        Sword.GetComponent<Collider>().enabled = true;
    }

    void OffAttack()
    {
        Sword.GetComponent<Collider>().enabled = false;
    }

    public float getRangeCool()
    {
        return ct_rangeSkill;
    }

    public float getMeleeCool()
    {
        return ct_meleeSkill;
    }

    public float getBufCool()
    {
        return ct_buf;
    }

    public float getHealCool()
    {
        return ct_heal;
    }

    public float getDodgeCool()
    {
        return ct_dodge;
    }





}

