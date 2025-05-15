public partial class StageController : BaseController
{
    /// <summary>
    /// 게임내 Touch 와 관련된 부분을 관리하고 제어.
    /// </summary>


    #region SerializeField

    [SerializeField]
    private RectTransform touchArea;

    [SerializeField]
    private List<GameObject> channelGameObj;

    /* 생략 */

    #endregion

    const int MaxTouchCount = 10;

    #region Private Variables

    private Dictionary<int, TouchInfo> touchInfos = new Dictionary<int, TouchInfo>();

    private List<Vector2> channelPos;

    /* 생략 */

    #endregion


    #region Class

    // 롱노트를 제어하기 위해 만들어진 터치관련 정보를 가지고 있는 클래스.
    public class TouchInfo
    {
        public bool IsTouch;                     // 터치가 되었는지.
        public bool IsSetController;           // FromNote, ToNote 컨트롤러가 셋팅 되어있는지.

        public bool IsTouchEffect;             // 터치효과가 시작했는지.

        public float TouchBeginTime;        // 터치 Begin 처음 시간.

        public NoteController FromNote;   // 롱노트의 처음 (FromNote) NoteController.
        public NoteController ToNote;       // 롱노트의 뒤쪽 (ToNote) NoteController.
        public LineController Line;            // 롱노트의 라인 LineController.

        public TouchEffectController LongTouchEffect; // 터치에 뿌려줄 터치이펙트.

        public void Clear()
        {
            IsTouch = false;
            IsSetController = false;

            IsTouchEffect = false;
            TouchBeginTime = 0;

            FromNote = default(NoteController);
            ToNote = default(NoteController);
            Line = default(LineController);

            if (LongTouchEffect != null)
            {
                LongTouchEffect.DestroyEffect();
            }
            LongTouchEffect = null;
        }

        // 해당 터치의 노트, 라인 정보를 셋팅해준다.
        public void SetTouchInfo(NoteController fromNote, NoteController toNote, LineController line)
        {
            this.FromNote = fromNote;
            this.ToNote = toNote;
            this.Line = line;
            this.IsTouch = true;
            this.IsSetController = true;
        }
    }

    #endregion


    #region Private Methods

    // 터치 관련 정보 초기화.
    private void InitTouchInfos()
    {
        this.touchInfos.Clear();

        for (int i = 0; i < MaxTouchCount; i++)
        {
            TouchInfo info = new TouchInfo();
            info.Clear();

            touchInfos.Add(i, info);
        }
    }


    // 터치 채널 설정. 판정 아래를 터치해도 해당 채널로 인식되게 설정.
    private void SetTouchChannel()
    {
        if (channelPos == null || channelPos.Count == 0)
        {
            channelPos = new List<Vector2>();
            for (int i = 0; i < channelGameObj.Count; i++)
            {
                Vector3 objPos = cam.WorldToScreenPoint(channelGameObj[i].transform.position);

                channelPos.Add(objPos);
            }
        }
    }


    private TouchInfo GetTouchInfos(int key)
    {
        return touchInfos[key];
    }

    private bool IsTouchingLong()
    {
        for (int i = 0; i < MaxTouchCount; i++)
        {
            var info = GetTouchInfos(i);

            if (info.IsTouchEffect)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsTouchingLongEndMiss()
    {
        for (int i = 0; i < MaxTouchCount; i++)
        {
            var info = GetTouchInfos(i);

            if (info.IsTouchEffect)
            {
                if (info.ToNote.Note.jUDGMENT != Core.JUDGMENT.Miss)
                {
                    return true;
                }
            }
        }

        return false;
    }


    private void PlayTouchEffect(int key)
    {
        var info = GetTouchInfos(key);

        if (info.LongTouchEffect == null)
        {
            info.LongTouchEffect = TouchObjectPool.Get();
        }

        if (info.FromNote != null)
        {
            if (!info.IsTouchEffect)
            {
                info.IsTouchEffect = true;
            }

            info.LongTouchEffect.PlayEffectPlarticle(info.FromNote.EffectPosX);
        }
    }

    public void StopTouchEffect(int key)
    {
        var info = GetTouchInfos(key);

        if (info.LongTouchEffect != null)
        {
            info.IsTouchEffect = false;
        }
    }


    private bool IsTouch(int key)
    {
        return touchInfos[key].IsTouch;
    }


    private void MouseTouchCheck()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        /* 화면 터치 처리 로직. */
#endif
    }

    private void ScreenTouchCheck()
    {
#if UNITY_IOS || UNITY_ANDROID
            if (0 < Input.touchCount)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    
                    /* 화면 터치 처리 로직. */
                }
            }
#endif
    }


    #region TouchPhase Methods

    void TouchPointBegin(Vector2 pos, ref bool isRefreshDashboard, TouchPhase touchPhase, int touchIndex)
    {
        /* TouchPhase.Began 에 대한 처리 로직.*/
    }


    private void TouchPointMoveStationary(Vector2 pos, ref bool isRefreshDashboard, TouchPhase touchPhase, int touchIndex)
    {
        /* TouchPhase.Moved, Stationary 에 대한 처리 로직.*/
    }


    private void TouchPointEnded(Vector2 pos, ref bool isRefreshDashboard, TouchPhase touchPhase, int fingerId)
    {
        /* TouchPhase.Ended 에 대한 처리 로직.*/
    }

    #endregion

    /* 생략 */

    #endregion

}

