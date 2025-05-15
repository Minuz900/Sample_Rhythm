public class NoteController : BaseController
{
    /// <summary>
    /// Note 를 컨트롤 하는 스크립트.
    /// </summary>
    
    #region SerializeField

    [SerializeField]
    private GameObject noteObject;

    [SerializeField]
    private SpriteRenderer sr;

    /* 생략 */

    #endregion


    #region Private Variables

    // StageController 에서 사용하는 오브젝트 풀 객체.
    private IObjectPool<NoteController> noteManagedPool;

    private Note note = null;
    private float velocity;
    private float scale;

    // 계산에 필요한 변수들.
    private Vector3 p;
    private Vector3 s;
    private float v;
    private float gap;
    private float tmpScale;
    private float offsetY;

    /* 생략 */

    #endregion


    #region Property

    public GameObject NoteObject
    {
        get
        {
            return this.noteObject;
        }
    }

    public Note Note
    {
        get
        {
            return this.note;
        }
    }

    public bool IsFlickBeginTouch
    {
        get
        {
            return this.isFlickBeginTouch;
        }
    }

    public Vector2 FlickBeginTouchPos
    {
        get
        {
            return this.flickBeginTouchPos;
        }
    }

    public float EffectPosX
    {
        get
        {
            return this.effectPosX;
        }
    }

    #endregion


    #region Public Methods

    public void SetManagedPool(IObjectPool<NoteController> pool)
    {
        noteManagedPool = pool;
    }


    public void SetNote(Note note, float velocity)
    {
        // 에디터 실행중 Hierarchy에서 구분하기 쉽게 게임오브젝트를 변경. 모바일 디바이스에서는 굳이 실행 할 필요없음.
#if UNITY_EDITOR
        this.gameObject.name = string.Format("N-{0}", note.uniq);
#endif

        this.note = note;
        this.velocity = velocity;
        this.sr.sprite = base.LoadNoteSprite(note.type);

        // 변수들 초기화.
        this.InitializeNote();

        // 넣을 위치를 잡고
        this.AllocPosition(this.stage.tickNumber, this.stage.newTick);

        // Alive On.
        this.SetIsAlive(true);

        // 튕기기 노트 설정.
        this.SetFlickNote();
    }


    public void BeginTouch()
    {
        if (isBeginTouch)
            return;

        this.isBeginTouch = true;
    }


    public Vector3 GetAllocPosition(UInt32 tickNumber, float newTick)
    {
        this.AllocPosition(tickNumber, newTick);

        return this.transform.localPosition;
    }


    public bool Explode(bool isChannelEffect)
    {
        /* 노트 폭발 처리 로직. */
        return true;
    }

    public void InitNotePostition()
    {
        this.gameObject.transform.localPosition = noteInitPos;
    }


    public void DestroyNote()
    {
        this.noteManagedPool.Release(this);
    }

    /* 생략 */

    #endregion


    #region Private Methods

    private void Update()
    {
        if (this.IsAlive)
        {
            if (this.IsDestroyed())
            {
                this.DestroyNote();
            }

            if (this.nowTickNumber == stage.tickNumber)
            {
                return;
            }

            /* 틱에 따른 노트 처리 로직. */

        }
    }

    private void InitializeNote()
    {
        /* 초기화가 필요한 변수들 값 초기화 */
    }

    private void AllocPosition(UInt32 tickNumber, float newTick)
    {
        /* 현재 tick 에 대한 상황에 맞는 Position, Scale 구하는 로직 */

        this.transform.localPosition = this.p;
    }


    private float linear(float start, float end, float val)
    {
        return Mathf.Lerp(start, end, val);
    }

    /* 생략 */

    #endregion

}

