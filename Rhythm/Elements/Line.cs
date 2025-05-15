public class Line : Base
{
    /// <summary>
    /// ������ �����ϴ� ���. 
    /// </summary>

    public EaseType easeType;
    public LineType type;
    public Note fromNote;
    public Note toNote;

    public Line()
    {
        easeType = EaseType.Linear;
        type = LineType.A;
        fromNote = null;
        toNote = null;
    }

    public override void LoadFromJObject(JObject obj)
    {
        type = (LineType)(int)obj["type"];
        easeType = (EaseType)(int)obj["easing"];
    }
}
