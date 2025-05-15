public class Speed : Knot
{
    public float velocity;

    public Speed() : base(KnotType.Speed)
    {
        this.velocity = 50.0f;
    }

    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        this.velocity = (float)obj["velocity"];
    }
}