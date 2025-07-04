using UnityEngine;

public class SimpleBallController : MonoBehaviour
{
    [Header("기본 설정")]
    public float power = 10f;
    public Sprite arrowSprite;

    private Rigidbody rb;
    private GameObject arrow;
    private bool isDragging = false;
    private Vector3 startPos;

    static bool isAnyBallPlaying = false;
    static bool isAnyBallMoving = false;

    void Start()
    {
        SetupBall();
    }


    void Update()
    {
        HandleInput();
        UpdateArrow();
    }

    void SetupBall()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.mass = 1;
        rb.drag = 1;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.2;
    }

    void HandleInput()
    {
        if (!SimpleTurnManager.canPlay)
        {
            return;
        }

        if (SimpleTurnManager.anyBallMoving)
        {
            return;
        }

        if (IsMoving())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Shoot();
        }
    }

void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                startPos = Input.mousePosition;
                CreateArrow();
                Debug.Log("드래그 시작");
            }
        }
    }
void Shoot()
    {
        // 공 발사 하기

        Vector3 mouseDelta = Input.mousePosition - startPos; // 마우스 이동 거리로 힘 계산
        float force = mouseDelta.magnitude * 0.01f + power;

        if (force < 5) force = 5; // 최소 힘 보정

        Vector3 direction = new Vector3(-mouseDelta.x, 0, -mouseDelta.y).normalized; // 방향 계산

        rb.AddForce(direction * force, ForceMode.Impulse); // 공에 힘 적용

        // 턴 매니저에게 공을 쳤다고 알림 (추후 구현)
        SimpleTurnManager.OnBallHit();

        //정리
        isDragging = false;
        Destroy(arrow);
        arrow = null;

        Debug.Log("발사! 힘 : " + force);
    }

    void CreateArrow()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }

        arrow = new GameObject("Arrow");
        SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();

        sr.sprite = arrowSprite;
        sr.color = Color.green;
        sr.sortingOrder = 10;

        arrow.transform.position = transform.position + Vector3.up;
        arrow.transform.localScale= Vector3.one;

    }

    void UpdateArrow()
    {
        if (!isDragging||arrow==null) return;
        Vector3 mouseDelta=Input.mousePosition - startPos;
        float distance = mouseDelta.magnitude;
        float size = Mathf.Clamp(distance * .01f, .5f,2f);
        arrow.transform.localScale = Vector3.one * size;

        SpriteRenderer sr = arrow.GetComponent<SpriteRenderer>();
        float colorRatio = Mathf.Clamp01(distance * .005f);
        sr.color=Color.Lerp(Color.green,Color.red, colorRatio);

        sr.color=new Color(sr.color.r,sr.color.g,sr.color.b,.5f);

        if (distance > 10f)
        {
            Vector3 direction = new Vector3(-mouseDelta.x,0,-mouseDelta.y);
            float angle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;
            arrow.transform.rotation=Quaternion.Euler(90,angle,0);
        }
    }
}
