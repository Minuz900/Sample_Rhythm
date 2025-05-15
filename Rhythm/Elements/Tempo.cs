public class Tempo
{
    /// <summary>
    /// ����, BPM, ���� �� �����ϴ� ���. 
    /// </summary>

    public Meter meter;
    public BPM bpm;
    public Width width;
    public float beginTime;

    public float duration { get { return null != meter && null != bpm ? meter.GetDurationByBPM(bpm.bpm) : 0.0f; } }

    public Tempo()
    {
        meter = null;
        bpm = null;
        width = null;
        beginTime = 0.0f;
    }
}


