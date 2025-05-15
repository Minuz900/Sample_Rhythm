public partial class StageController : BaseController
{
    /// <summary>
    /// 스테이지의 가장기본이 되는 노트나 데이터를 제어하는 컨트롤러.
    /// </summary>

    public delegate void DelegateController();

    public DelegateController delegatePlayOrPause = null;
    public DelegateController delegateCompleted = null;
    public DelegateController delegateNoteCheck = null;

    #region ObjectPool

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/Pool.ObjectPool_1.html 
    /// 유니티에서 공식 제공하는 오브젝트 풀링 사용.
    /// </summary>

    // NoteController 형식의 오브젝트풀 객체.
    private IObjectPool<NoteController> noteObjectPool;

    // LineController 형식의 오브젝트풀 객체.
    private IObjectPool<LineController> lineObjectPool;

    // TouchEffectController 형식의 오브젝트풀 객체.
    private IObjectPool<TouchEffectController> touchObjectPool;

    // 외부에서 접근이 필요해 생성한 Property.
    public IObjectPool<TouchEffectController> TouchObjectPool
    {
        get
        {
            return touchObjectPool;
        }
    }


    // 오브젝트 풀링에 생성 될 노트 프리팹.
    [SerializeField]
    private GameObject noteObjectPrefab;

    // 오브젝트 풀링에 생성 될 라인 프리팹.
    [SerializeField]
    private GameObject lineObjectPrefab;

    // 오브젝트 풀링에 생성 될 터치효과 프리팹.
    [SerializeField]
    private GameObject touchObjectPrefab;

    // 오브젝트 풀링으로 생성을 위한 부모 Transform.
    [SerializeField]
    private Transform objectPoolParent;

    // 오브젝트 풀링으로 생성을 위한 부모 Transform.
    [SerializeField]
    private Transform touchObjectPoolParent;

    // 초기 노트 프리팹 생성 개수.
    private int noteCreateCount = 50;

    // 초기 라인 프리팹 생성 개수.
    private int lineCreateCount = 20;

    // 초기 터치 효과 프리팹 생성 개수.
    private int TouchEffectCreateCount = 8;

    #endregion



    #region GamePlay Variables

    [HideInInspector]
    public UInt32 tickNumber = 0;

    [HideInInspector]
    public float oldTick = 0f;
    [HideInInspector]
    public float newTick = 0f;
    [HideInInspector]
    public float noteNewTick = 0f;
    [HideInInspector]
    public float velocity = 0f;

    private float oldTrackCurrentTime;
    private float trackCurrentTime;
    private double trackStartTime;
    private double trackPauseTime;
    private float trackTotalPauseTime;

    private StageState state = StageState.Init;
    [HideInInspector]
    public StageState State
    {
        get
        {
            return this.state;
        }
        set
        {
            if (this.state != value)
            {
                this.state = value;
                if (StageState.Play == value || StageState.Pause == value)
                {
                    if (null != delegatePlayOrPause)
                    {
                        delegatePlayOrPause();
                    }
                }
            }
        }
    }

    // 현재 생성되어있는 Knot들.
    private List<Knot> knots = new List<Knot>(50);

    // 현재 활성화 되어있는 Notes.
    private List<NoteController> activeNotes = new List<NoteController>(50);

    // 현재 활성화 되어있는 Lines.
    private List<LineController> activeLines = new List<LineController>(20);

    #endregion

    #region MonoBehaviour Methods

    void Awake()
    {
        /* 게임 데이터 초기화 */
        InitSoundController();
        InitValue();
        InitTouchInfos();
    }

    void Start()
    {

    }


    void Update()
    {
        if (StageState.Play == state)
        {
            /* 게임내의 tick/time 관리, 노트/라인 생성 및 관리, 화면 터치 체크 처리 로직. */

            UpdateKnots();

            ProcessKnots();

            SetTrackCurrentTime();

            /* 생략 */

            #region TouchCheck
#if UNITY_EDITOR || UNITY_STANDALONE
            MouseTouchCheck();
#elif UNITY_IOS || UNITY_ANDROID
            ScreenTouchCheck();
#endif
            #endregion

            CheckStageComplete();
        }
    }


#if !UNITY_EDITOR
    void OnApplicationPause(bool isPaused)
    {
        if (StageState.Play == state)
        {
            if (isPaused)
            {
                /* Paused 처리 로직. */
            }
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            /* Focus 처리 로직. */
        }
    }
#endif


    #endregion


    #region Public Methods

    // 스테이지 정보를 셋팅하고 데이터에 따라 맞춤작업 수행.
    public void LoadStage(StageMode mode)
    {
        /* 스테이지에 정보에 따라 필요한 오브젝트 생성, 배경, UI 등등 로드하고 셋팅 후 게임 시작 로직. */

        InitStageVariables();

        Init();

        if (noteObjectPool == null)
        {
            noteObjectPool = new ObjectPool<NoteController>(CreateNote, OnGetNote, OnReleaseNote, OnDestroyNote, maxSize: noteCreateCount);

            for (int i = 0; i < noteCreateCount; i++)
            {
                var note = CreateNote();
                noteObjectPool.Release(note);
            }
        }

        if (lineObjectPool == null)
        {
            lineObjectPool = new ObjectPool<LineController>(CreateLine, OnGetLine, OnReleaseLine, OnDestroyLine, maxSize: lineCreateCount);

            for (int i = 0; i < lineCreateCount; i++)
            {
                var line = CreateLine();
                lineObjectPool.Release(line);
            }
        }

        if (touchObjectPool == null)
        {
            touchObjectPool = new ObjectPool<TouchEffectController>(CreateTouchEffect, OnGetTouchEffect, OnReleaseTouchEffect, OnDestroyTouchEffect, maxSize: touchEffectCreateCount);

            for (int i = 0; i < touchEffectCreateCount; i++)
            {
                var effect = CreateTouchEffect();
                touchObjectPool.Release(effect);
            }
        }

        StartCoroutine(GameStart());

    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);

        UpdateStageState(StageState.Play);
    }


    // activeLines 리스트에서 해당 라인을 찾음.
    public LineController FindLine(Line line)
    {
        for (int i = 0; i < activeLines.Count; i++)
        {
            if (activeLines[i].Line == line)
            {
                return activeLines[i];
            }
        }

        return null;
    }


    // activeNotes 리스트에서 note 기준 해당 노트를 찾음.
    public NoteController FindNoteController(Note note)
    {
        for (int i = 0; i < activeNotes.Count; i++)
        {
            if (activeNotes[i].Note == note)
            {
                return activeNotes[i];
            }
        }
        return null;
    }


    // activeNotes 리스트에서 uniqId 기준 해당 노트를 찾음.
    public NoteController FindNoteController(uint uniqId)
    {
        for (int i = 0; i < activeNotes.Count; i++)
        {
            if (activeNotes[i].Note.uniq == uniqId)
            {
                return activeNotes[i];
            }
        }
        return null;
    }

    #endregion



    #region Private Methods

    private void InitStageVariables()
    {
        // 변수 초기화.
        musicAudioWave = null;
        tickNumber = 0;
        oldTick = 0.0f;
        newTick = 0.0f;
        noteNewTick = 0.0f;
        velocity = stage.velocity;

        continueTryCount = 0;
        boosterNoteClearCount = 0;
    }


    private void InitValue()
    {
        /* 게임내 계산에 사용되는 변수, 에셋에 정의되어있는 데이터로 셋팅 */
    }

    void SetTrackCurrentTime()
    {
        // dspTime이 변화했으면 보간된 시간 변수에도 적용한다.
        if (AudioSettings.dspTime != lastReportedPlayheadPosition)
        {
            lastReportedPlayheadPosition = AudioSettings.dspTime;
            interpolatedDspTime = AudioSettings.dspTime;
        }

        trackCurrentTime = (float)(interpolatedDspTime - gameTimeOffset - trackStartTime - trackTotalPauseTime);
    }


    private void UpdateStageState(StageState state)
    {
        if (this.state != state)
        {
            this.state = state;
        }
    }

    private void SetNewTick(float tick)
    {
        newTick = tick;
    }

    private void SetNoteNewTick(float tick)
    {
        noteNewTick = tick;
    }


    private void UpdateKnots()
    {
        stage.FindBySpan(0, noteNewTick, newTick + stage.topMarginTick, ref knots);
    }


    private void ProcessKnots()
    {
        for (int i = 0; i < knots.Count; i++)
        {
            switch (knots[i].knotType)
            {
                case KnotType.Note:
                    /* 단노트, 롱노트 생성 로직. */
                    break;
                case KnotType.Speed:
                    /* 스피드(velocity) 시작 및 변경 로직. */
                    break;
                case KnotType.Wave:
                    /* 음원 시작 및 변경 로직. */
                    break;
            }
        }
    }


    private void CheckStageComplete()
    {
        /* 스테이지 완료 로직. */

        if (null != delegateCompleted)
            delegateCompleted();
    }

    private NoteController FindNotesByPosition(Vector2 pos, TouchPhase touchPhase, out JUDGMENT check)
    {
        /* 터치한 위치와 시간에 따라 처리 되어야 할 노트를 찾는 로직. */
    }


    private JUDGMENT CheckGrade(NoteController note, float beginTime, bool isEnd = false)
    {
        /* 터치된 시간에 따라 판정 체크 로직. */
    }


    // 터치 된 노트에 대해 다음 스텝을 처리하는 부분.
    private void NoteResultProcessing(NoteController controller, JUDGMENT judgment)
    {
        /* 터치, 노터치에 따라 처리된 노트 결과에 따라 점수, 등급, 콤보, 데미지 등등 다음 처리 스텝 로직. */

        ComboAndDamageCheck(controller);
        CheckFever(controller);
        ScoreCalculator(controller);
    }


    private void ScoreCalculator(NoteController note)
    {
        /* 노트처리에 대한 스코어 계산 로직. */
    }

    #endregion

    #region Object Pooling Methods

    /// <summary>
    /// 노트 오브젝트 풀 관련 함수들.
    /// </summary>
    private NoteController CreateNote()
    {
        NoteController note = Instantiate(noteObjectPrefab, parent: objectPoolParent).GetComponent<NoteController>();
        note.SetManagedPool(noteObjectPool);
        return note;
    }

    private void OnGetNote(NoteController note)
    {
        note.gameObject.SetActive(true);
    }

    private void OnReleaseNote(NoteController note)
    {
        note.SetIsAlive(false);
        note.gameObject.SetActive(false);
        note.InitNotePostition();

        activeNotes.Remove(note);
    }

    private void OnDestroyNote(NoteController note)
    {
        Destroy(note.gameObject);
    }


    /// <summary>
    /// 라인 오브젝트 풀 관련 함수들.
    /// </summary>
    private LineController CreateLine()
    {
        LineController line = Instantiate(lineObjectPrefab, parent: objectPoolParent).GetComponent<LineController>();
        line.SetManagedPool(lineObjectPool);
        return line;
    }

    private void OnGetLine(LineController line)
    {
        line.gameObject.SetActive(true);
    }

    private void OnReleaseLine(LineController line)
    {
        if (line.FromNoteController != null)
        {
            RemoveLineTouchEffect(line);
            Dic.Remove(line.FromNoteController.Note.uniq);
        }

        line.SetIsAlive(false);
        line.gameObject.SetActive(false);
        line.InitLine();


        activeLines.Remove(line);
    }

    private void OnDestroyLine(LineController line)
    {
        Destroy(line.gameObject);
    }


    /// <summary>
    ///  이펙트 오브젝트 풀 관련 함수들.
    /// </summary>
    private TouchEffectController CreateTouchEffect()
    {
        TouchEffectController touchEffect = Instantiate(touchObjectPrefab, parent: touchObjectPoolParent).GetComponent<TouchEffectController>();
        touchEffect.SetManagedPool(touchObjectPool);
        return touchEffect;
    }

    private void OnGetTouchEffect(TouchEffectController touchEffect)
    {
        touchEffect.InitEffect();
        touchEffect.gameObject.SetActive(true);
    }

    private void OnReleaseTouchEffect(TouchEffectController touchEffect)
    {
        touchEffect.InitEffect();

        touchEffect.SetIsAlive(false);
        touchEffect.gameObject.SetActive(false);
    }

    private void OnDestroyTouchEffect(TouchEffectController touchEffect)
    {
        Destroy(touchEffect.gameObject);
    }

    #endregion
}

