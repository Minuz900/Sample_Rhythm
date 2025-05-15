public class Info : Knot
{
    public string name;
    public DifficultType difficultType;
    public GridType gridType;
    public UInt32 totalBars;

    public Info() : base(KnotType.Info)
    {
        name = "Untitled";
        difficultType = DifficultType.Easy;
        gridType = GridType.X2;
        totalBars = 40;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        name = (string)obj["name"];
        difficultType = (DifficultType)(int)obj["difficultType"];
        gridType = (GridType)(int)obj["gridType"];
        totalBars = (UInt32)obj["totalBars"];
    }
}
