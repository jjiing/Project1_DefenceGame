using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float nowHp;
    public float maxHp;
    public int atk;
    public int exp;
    public int gold;
    public int diamond;
    private int lifedamage = 1;
    Image nowHpbar;
    public GameObject prefHpBar;
    public GameObject canvas;
    public GameObject dropText;
    public GameObject dropImage;
    public InGameManager inGameManager;   
    RectTransform hpBar;
    RectTransform textPosition;
    RectTransform imagePosition;
    private SpriteRenderer spriteRenderer;
    private Collider2D Collider2D;
    public IEnumerator onMove;
    public IEnumerator co_hit;
    public Transform attackTarget;
    public Animator animator;
    public bool isChase;
    public bool isMove;
    private int wayPointCount;
    private Transform[] wayPoints;
    public int currentIndex = 0;
    public float height = 0.02f;
    public Movement2D movement2D;
    private EnemySpawn enemySpawn;
    private MonsterRange monsterRange;
    public bool isSlow = false;
    bool isRightAttack = false;
    bool isDie = false;

    public int Lifedamage
    {
        set
        {
            lifedamage = value;
            Debug.Log("lifedamage : " + lifedamage);
        }
    }
    private void Awake()
    {
        isChase = false;
        isMove = true;
    }

    private void Start()
    {
        monsterRange = transform.GetChild(0).GetComponent<MonsterRange>();
        Collider2D = GetComponent<Collider2D>();
        movement2D = GetComponent<Movement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GameObject.Find("MonsterHpUI");
        inGameManager = FindObjectOfType<InGameManager>();
        hpBar = Instantiate(prefHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar=hpBar.transform.GetChild(0).GetComponent<Image>();
        
        onMove = OnMove();

        StartCoroutine(AttackCo());
    }

    private void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.55f, 0));
        if (hpBar != null)
            hpBar.position = _hpBarPos;

        nowHpbar.fillAmount = nowHp / maxHp;

        if (nowHp <= 0 && !isDie)
        {
            isDie = true;
            OnDie();
        }
        
    }

    public IEnumerator DieCo()
    {
        Destroy(hpBar.gameObject);
        Collider2D.enabled = false;

        movement2D.MoveSpeed = 0;
        animator.SetBool("Die", true);

        yield return new WaitForSeconds(0.9f);

        enemySpawn.DestoryEnemy(this);
    }

    IEnumerator FadeAway()
    {
        SpriteRenderer targetMonster = GetComponent<SpriteRenderer>();
        targetMonster.color = Color.white;

        yield return new WaitForSeconds(0.5f);

        while (spriteRenderer.color.a > 0)
        {           
            var color = spriteRenderer.color;

            color.a -= (5f * Time.deltaTime);

            spriteRenderer.color = color;

            yield return null;
        }

    }
    IEnumerator AttackCo()
    {
        while (true)
        {
            if (attackTarget != null)
            {

                if (Vector3.Distance(transform.position, attackTarget.position) < 0.6f)
                {

                    isChase = false;
                    isMove = false;

                    animator.SetBool("RightWalk", false);
                    animator.SetBool("LeftWalk", false);

                    isRightAttack = (attackTarget.position.x > transform.position.x);
                    animator.SetBool("LeftAttack", !isRightAttack);
                    animator.SetBool("RightAttack", isRightAttack);

                    if (attackTarget.GetComponent<Warrior>() != null)
                        GameManager.instance.HP -= atk;
                    else
                        attackTarget.GetComponent<knight>().KnightHP -= atk;

                    yield return new WaitForSeconds(1);
                }
                else
                {
                    isChase = true;
                    isMove = false;
                    animator.SetBool("LeftAttack", false);
                    animator.SetBool("RightAttack", false);

                    yield return null;
                }
            }
            yield return null;
        }
    }

    public IEnumerator Hit(int damage)
    {
        nowHp -= damage;

        SpriteRenderer targetMonster = GetComponent<SpriteRenderer>();
        targetMonster.color = new Color(1f, 0.4f, 0.4f, 1f);

        yield return new WaitForSeconds(0.3f);

        if(isSlow==true)      
            targetMonster.color = new Color(0.4f, 0.4f, 1f, 1f);   
        else
            targetMonster.color = Color.white;
    }

    public IEnumerator WizardHit(int damage)
    {
        SpriteRenderer targetMonster = GetComponent<SpriteRenderer>();
        targetMonster.color = new Color(0.4f, 0.4f, 1f, 255f);
        nowHp -= damage;
        isSlow = true;

        yield return new WaitForSeconds(2f);

        isSlow = false;
        targetMonster.color = Color.white;
    }

    public void Setup(EnemySpawn enemySpawn, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawn = enemySpawn;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    public IEnumerator OnMove()
    {
        if (isMove == true)
        {
            if (isChase == false)
            {

                NextMoveTo();

                while (true)
                {

                    if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.1f * movement2D.MoveSpeed)
                    {

                        NextMoveTo();
                    }
                    yield return null;
                }
            }
        }
    }
    private void NextMoveTo()
    {

        if (currentIndex < wayPointCount - 1)
        {

            currentIndex++;
            if (wayPoints[currentIndex].transform.position.x > transform.position.x)
            {

                animator.SetBool("RightWalk", true);
                animator.SetBool("LeftWalk", false);
            }
            else
            {

                animator.SetBool("RightWalk", false);
                animator.SetBool("LeftWalk", true);
            }

            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);

        }
        else
        {
            
            inGameManager.Life -= lifedamage;
            enemySpawn.DestoryEnemy(this);
        }
    }

    public void OnDie()
    {
        float range = Random.Range(1, 11);
        if (range == 1)
        {
            dropText.GetComponent<DropText>().dia = diamond;
            textPosition = Instantiate(dropText, canvas.transform).GetComponent<RectTransform>();
            imagePosition = Instantiate(dropImage, canvas.transform).GetComponent<RectTransform>();
            Vector3 _textPosition = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.55f, 0));
            Vector3 _imagePosition = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 0.55f, transform.position.y + 0.3f, 0));
            if (textPosition != null)
            {
                textPosition.position = _textPosition;
                imagePosition.position = _imagePosition;
            }

            GameManager.instance.Diamond += diamond;       // 수정     
        }
        else
        {
            inGameManager.Gold += gold;

        }

        GameManager.instance.Exp += exp;     // 수정
        StartCoroutine(FadeAway());
        StartCoroutine(DieCo());
    }
}