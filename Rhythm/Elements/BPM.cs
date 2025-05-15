public class BPM : Knot
{
    public float bpm;

    public BPM() : base(KnotType.BPM)
    {
        bpm = 110.0f;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        bpm = (float)obj["BPM"];
    }
}