public class Width : Knot
{
    public byte startChannel;
    public byte endChannel;

    public Width() : base(KnotType.Width)
    {
        startChannel = 1;
        endChannel = 12;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        startChannel = (byte)obj["startChannel"];
        endChannel = (byte)obj["endChannel"];
    }
}
