public class Stage : Base
{
    /// <summary>
    /// 스테이지를 구성하는 요소에 대한 데이터를 관리. 
    /// </summary>

    public Dictionary<string, List<Knot>> knots = new Dictionary<string, List<Knot>>();

    public List<Line> lines = new List<Line>();
    public List<Tempo> tempos = new List<Tempo>();
    public List<Knot> sortedknots = new List<Knot>();

    public string name
    {
        get
        {
            Info info = (Info)GetKnot(1, 1, 1, 1, KnotType.Info);
            if (null != info)
                return info.name;
            return null;
        }
    }

    public DifficultType difficultType
    {
        get
        {
            Info info = (Info)GetKnot(1, 1, 1, 1, KnotType.Info);
            if (null != info)
                return info.difficultType;
            return DifficultType.None;
        }
    }

    public GridType gridType
    {
        get
        {
            Info info = (Info)GetKnot(1, 1, 1, 1, KnotType.Info);
            if (null != info)
                return info.gridType;
            return GridType.None;
        }
    }

    public UInt32 totalBars
    {
        get
        {
            Info info = (Info)GetKnot(1, 1, 1, 1, KnotType.Info);
            if (null != info)
                return info.totalBars;
            return 1;
        }
    }

    public float velocity
    {
        get
        {
            Speed speed = (Speed)GetKnot(1, 1, 4, 4, KnotType.Speed);
            if (null != speed)
                return speed.velocity;
            return 1f;
        }
    }

    public void Clear()
    {
        knots.Clear();
        lines.Clear();
        tempos.Clear();
        sortedknots.Clear();

        /* 추가 데이터들 초기화. */
    }

    public void Restart()
    {
        /* 데이터들 초기화. */
    }

    public void Ready(StageMode mode)
    {
        /* 게임 로딩씬에서 미리 로드된 데이터 가져와 셋팅. */

        ResetByType(mode);
    }

    public void ResetByType(StageMode mode)
    {
        this.mode = mode;

        /* 게임 모드에 따른 변경되어야 할 데이터 변경. */
    }

    private void RefreshTempos()
    {
        tempos.Clear();

        /* BPM, Meter, Grid, Width 데이터들 Refresh. */
    }


    public void FindBySpan(int beginIdx, float fromTime, float toTime, ref List<Knot> knots)
    {
        knots.Clear();

        /* 이진탐색(바이너리 서치)으로 플레이할 knots을 얻음. */
    }

    public string AddKnot(Knot knot)
    {
        List<Knot> branch = null;
        string key = knot.key;
        if (!knots.ContainsKey(key))
        {
            branch = new List<Knot>();
            knots.Add(key, branch);
        }
        else
        {
            branch = knots[key];
        }

        if (branch.Count < Constants.MAX_KNOTS_IN_BRANCH)
        {
            branch.Add(knot);
            return key;
        }
        return null;
    }

    public Knot GetKnot(UInt32 bar, UInt32 bit, UInt32 startChannel, UInt32 endChannel, KnotType knotType)
    {
        string key = Position.MakeKey(bar, bit, startChannel, endChannel);
        if (knots.ContainsKey(key))
        {
            List<Knot> branch = knots[key];
            foreach (Knot knot in branch)
                if (knotType == knot.knotType)
                    return knot;
        }
        return null;
    }

    public Knot GetKnot(string key, UInt32 uniq)
    {
        if (knots.ContainsKey(key))
        {
            List<Knot> branch = knots[key];
            foreach (Knot knot in branch)
                if (knot.uniq == uniq)
                    return knot;
        }
        return null;
    }

    public Tempo GetTempo(UInt32 bar)
    {
        if (0 < bar && bar <= totalBars)
            return tempos[(int)bar - 1];
        return null;
    }

    public bool AddLine(Line line)
    {
        if (null == line.fromNote.forwardLine && null == line.toNote.backwardLine)
        {
            line.fromNote.forwardLine = line;
            line.toNote.backwardLine = line;
            lines.Add(line);
            return true;
        }
        else
            return false;
    }

    public bool LoadFromResources(string filePath)
    {
        /* Json 파일을 로드 후 파싱 로직. */

        return false;
    }

    public override void LoadFromJObject(JObject obj)
    {
        knots.Clear();
        lines.Clear();

        /* knots, lines 데이터 셋팅 로직. */
    }
}