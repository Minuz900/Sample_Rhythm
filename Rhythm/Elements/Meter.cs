public class Meter : Knot
{
    public byte numerator;
    public byte denominator;

    public Meter() : base(KnotType.Meter)
    {
        numerator = 4;
        denominator = 4;
    }

    public float GetDurationByBPM(float bpm)
    {
        return (float)numerator * 60.0f / bpm;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        numerator = (byte)obj["numerator"];
        denominator = (byte)obj["denominator"];
    }
}
