public class Memo : Knot
{
    public string content;

    public Memo() : base(KnotType.Memo)
    {
        content = string.empty;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        content = (string)obj["content"];
    }
}