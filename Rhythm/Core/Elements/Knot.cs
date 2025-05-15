public class Knot : Base
{
    /// <summary>
    /// 게임을 구성하는 노트의 구성하는 요소. 
    /// </summary>

    public KnotType knotType;
    public KnotAttr knotAttr;
    public Position position = null;
    public UInt32 uniq;
    public float beginTime;

    public String key { get { return position.key; } }

    public int NoteChannel { get { return (int)position.startChannel; } }

    public static Knot Factory(JObject obj)
    {
        KnotType knotType = (KnotType)(int)obj["knotType"];
        Knot knot = null;
        switch (knotType)
        {
            case KnotType.Note: knot = new Note(); break;
            case KnotType.BPM: knot = new BPM(); break;
            case KnotType.Meter: knot = new Meter(); break;
            case KnotType.Info: knot = new Info(); break;
            case KnotType.Speed: knot = new Speed(); break;
            case KnotType.Width: knot = new Width(); break;
            case KnotType.Wave: knot = new Wave(); break;
            case KnotType.Memo: knot = new Memo(); break;
            case KnotType.Grid: knot = new Grid(); break;
            default:
                throw new System.NotImplementedException();
        }
        if (null != knot)
            knot.LoadFromJObject(obj);
        return knot;
    }

    public Knot(KnotType knotType)
    {
        this.knotType = knotType;
        knotAttr = KnotAttr.Movable;
        position = new Position();
        uniq = 0;
        beginTime = 0.0f;
    }

    public override void LoadFromJObject(JObject obj)
    {
        knotType = (KnotType)(int)obj["knotType"];
        knotAttr = (KnotAttr)(int)obj["knotAttr"];
        position.LoadFromJObject(obj["position"] as JObject);
        uniq = (UInt32)obj["uniq"];
    }
}