public class Grid : Knot
{
    public GridType type;

    public Grid() : base(KnotType.Grid)
    {
        type = GridType.X2;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        type = (GridType)(int)obj["type"];
    }
}
