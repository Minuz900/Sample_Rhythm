public class Tempo
{
    /// <summary>
    /// 박자, BPM, 길이 등 구성하는 요소. 
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


