public class Position : Base
{
    /// <summary>
    /// 게임내 정의된 포지션을 구성하는 요소. 
    /// </summary>

    public UInt32 bar;
    public UInt32 bit;
    public UInt32 startChannel;
    public UInt32 endChannel;
    public float offset;

    public static string MakeKey(UInt32 bar, UInt32 bit, UInt32 startChannel, UInt32 endChannel)
    {
        return bar.ToString() + "-" + bit.ToString() + "-" + startChannel.ToString() + "-" + endChannel.ToString();
    }
    public string key { get { return Position.MakeKey(bar, bit, startChannel, endChannel); } }

    public Position()
    {
        bar = 0;
        bit = 0;
        startChannel = 0;
        endChannel = 0;
        offset = 0.0f;
    }

    public Position(UInt32 bar, UInt32 bit, UInt32 startChannel, UInt32 endChannel)
    {
        this.bar = bar;
        this.bit = bit;
        this.startChannel = startChannel;
        this.endChannel = endChannel;
        offset = 0.0f;
    }

    public bool IsEqual(Position pos)
    {
        return
            bar == pos.bar &&
            bit == pos.bit &&
            startChannel == pos.startChannel &&
            endChannel == pos.endChannel &&
            offset == pos.offset;
    }

    public bool IsEqualTime(Position pos)
    {
        return
            bar == pos.bar &&
            bit == pos.bit &&
            offset == pos.offset;
    }

    public override void LoadFromJObject(JObject obj)
    {
        bar = (UInt32)obj["bar"];
        bit = (UInt32)obj["bit"];
        startChannel = (UInt32)obj["startChannel"];
        endChannel = (UInt32)obj["endChannel"];
        offset = (float)obj["offset"];
    }

}
