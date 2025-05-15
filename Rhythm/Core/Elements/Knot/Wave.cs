public class Wave : Knot
{
    public string fileName;
    public AudioClip audioClip;

    public Wave() : base(KnotType.Wave)
    {
        fileName = "unknown";
        audioClip = null;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        fileName = (string)obj["fileName"];
    }
}