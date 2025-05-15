public class Note : Knot
{
    public NoteType type;
    public Line forwardLine;
    public Line backwardLine;
    public JUDGMENT judgment;
    public bool isCount;
    public bool isLastNote;
    public bool isBoosterNote;

    public Note() : base(KnotType.Note)
    {
        type = NoteType.A;
        forwardLine = null;
        backwardLine = null;
        judgment = JUDGMENT.None;
        isCount = false;
        isLastNote = false;
        isBoosterNote = false;
    }


    public override void LoadFromJObject(JObject obj)
    {
        base.LoadFromJObject(obj);
        type = (NoteType)(int)obj["type"];
    }
}
