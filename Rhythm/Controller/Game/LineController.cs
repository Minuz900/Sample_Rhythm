public class LineController : BaseController
{
    /// <summary>
    /// Line을 컨트롤 하는 스크립트.
    /// </summary>

    #region SerializeField

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material[] rendLineMaterials;

    /* 생략 */

    #endregion


    #region Private Variables

    private UInt32 tickNumber = 0;

    private Line line = null;
    private NoteController fromNoteController = null;
    private NoteController toNoteController = null;

    private IObjectPool<LineController> lineManagedPool;

    private NoteData noteData = new NoteData();

    private float newTick;

    private int[] triangles =
    {
        0, 2, 1,
        2, 3, 1
    };

    private float fromRadius;
    private float toRadius;

    private Vector3 xVector = new Vector3(0f, 0f, 10f);
    private Vector3 xxVector = new Vector3(0f, 0f, 10f);
    private Vector3 xxxVector = new Vector3(0f, 0f, 10f);
    private Vector3 xxxxVector = new Vector3(0f, 0f, 10f);
    private Vector3[] vertices = new Vector3[4];

    private Vector2[] uvs = new Vector2[]
    {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
    };

    private Vector3 initPos = new Vector3(50000f, 50000f, 0f);

    #endregion



    #region Property

    public Line Line
    {
        get
        {
            return this.line;
        }
    }

    public NoteController FromNoteController
    {
        get
        {
            return this.fromNoteController;
        }
    }

    public NoteController ToNoteController
    {
        get
        {
            return this.toNoteController;
        }
    }

    #endregion



    #region Class

    public class NoteData
    {
        public Vector3 fromPos;
        public Vector3 toPos;
        public Vector3 fromScale;
        public Vector3 toScale;
        public float fromSize;
        public float toSize;
        public float fromMid;
        public float toMid;
    }

    #endregion



    #region Public Methods

    public void SetManagedPool(IObjectPool<LineController> pool)
    {
        lineManagedPool = pool;
        rendLineMaterials = meshRenderer.materials;
    }

    public void SetLine(Line line)
    {
        /* 라인 transform, Material 설정 로직. */
    }


    public void SetDisabled()
    {
        /* 비활성 처리. */
    }


    public void InitLine()
    {
        this.gameObject.transform.localPosition = initPos;
        this.toNoteController = null;
        this.fromNoteController = null;
        this.line = null;
    }


    public void DestroyLine()
    {
        this.lineManagedPool.Release(this);
    }

    /* 생략 */

    #endregion



    #region Private Methods

    private void Locate(UInt32 tickNumber, float newTick)
    {
        this.SetFromNoteController();
        this.SetToNoteController();

        if (null != this.fromNoteController && null != this.toNoteController)
        {
            this.SetNoteData(newTick);
            this.SetStraightLineMesh();
        }

        this.tickNumber = tickNumber;
    }


    private void SetFromNoteController()
    {
        if (null == this.fromNoteController)
        {
            this.fromNoteController = this.stage.FindNoteController(line.fromNote);
        }
    }


    private void SetToNoteController()
    {
        if (null == this.toNoteController)
        {
            this.toNoteController = this.stage.FindNoteController(line.toNote);
        }
    }


    private void SetNoteData(float newTick)
    {
        this.noteData.fromPos = this.fromNoteController.GetAllocPosition(tickNumber, newTick);
        this.noteData.toPos = this.toNoteController.GetAllocPosition(tickNumber, newTick);

        this.noteData.fromScale = this.fromNoteController.NoteObject.transform.localScale;
        this.noteData.toScale = this.toNoteController.NoteObject.transform.localScale;

        this.noteData.fromSize = this.fromNoteController.GetSize(this.fromNoteController.Note.position);
        this.noteData.toSize = this.toNoteController.GetSize(this.toNoteController.Note.position);

        this.noteData.fromMid = this.fromNoteController.GetMidChannel(this.fromNoteController.Note.position);
        this.noteData.toMid = this.toNoteController.GetMidChannel(this.toNoteController.Note.position);
    }


    private void SetStraightLineMesh()
    {
        Mesh mesh = this.meshFilter.mesh;

        this.fromRadius = (((this.noteData.fromSize - 1) * (Constants.LINE_RADIUS * 0.5f)) * this.noteData.fromScale.x);
        this.toRadius = (((this.noteData.toSize - 1) * (Constants.LINE_RADIUS * 0.5f)) * this.noteData.toScale.x);

        this.xVector.x = (this.noteData.fromPos.x) - this.fromRadius;
        this.xVector.y = this.noteData.fromPos.y;
        this.vertices[0] = this.xVector;

        this.xxVector.x = (this.noteData.fromPos.x) + this.fromRadius;
        this.xxVector.y = this.noteData.fromPos.y;
        this.vertices[1] = this.xxVector;

        this.xxxxVector.x = this.noteData.toPos.x - this.toRadius;
        this.xxxxVector.y = this.noteData.toPos.y;
        this.vertices[2] = this.xxxxVector;

        this.xxxVector.x = this.noteData.toPos.x + this.toRadius;
        this.xxxVector.y = this.noteData.toPos.y;
        this.vertices[3] = this.xxxVector;

        mesh.vertices = this.vertices;
        mesh.triangles = this.triangles;
        mesh.uv = this.uvs;
    }

    /* 생략 */

    #endregion



    #region IEnumerator

    private void Update()
    {
        if (this.IsAlive)
        {
            this.newTick = stage.newTick;

            /* 틱에 따른 라인 처리 로직. */

        }
    }

    #endregion

}

