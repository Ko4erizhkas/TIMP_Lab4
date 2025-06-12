using TIMP_Lab4;
namespace BlackBox_Shadow;

[TestClass]
public sealed class BlackBox_Shadows
{
    private ShadowCalc Make(params (int, int)[] segs)
    {
        var list = new List<Segment>();
        foreach (var (a, b) in segs)
        {
            list.Add(new Segment(a, b));
        }
        return new ShadowCalc(list);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NegativeArgs_Return_ArgumentException()
    {
        var segments = Make((-1, -3));

        segments.CalcTotalLength();

        // return ArgumentException
    }
    [TestMethod]
    public void EmptyCollection_ReturnsZero()
    {
        var calc = new ShadowCalc(new List<Segment>());

        calc.CalcTotalLength();

        Assert.AreEqual(0, calc.CalcTotalLength());
    }
    [TestMethod]
    public void TestCalculating_ReturnsSumOfLengths()
    {
        var calc = new ShadowCalc(new List<Segment> { new Segment(1, 3), new Segment(5, 8) });

        Assert.AreEqual(5, calc.CalcTotalLength());
    }
    [TestMethod]
    public void TestCalculating_ReturnPositiveLengthOneSegment_1()
    {
        var calc = new ShadowCalc(new List<Segment> { new Segment(1, 3) });

        Assert.AreEqual(2, calc.CalcTotalLength());
    }
    [TestMethod]
    public void TestCalculating_ReturnPositiveLengthOneSegment_2()
    {
        var calc = new ShadowCalc(new List<Segment> { new Segment(3,1)});

        Assert.AreEqual(2, calc.CalcTotalLength());
    }
    [TestMethod]
    public void TestCalculating_CorrectSegment_With_ZeroSegment()
    {
        var calc = new ShadowCalc(new List<Segment> { new Segment(1,2), new Segment(0,0)});

        Assert.AreEqual(1, calc.CalcTotalLength());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCalculating_CorrectSegment_With_NegativeSegment()
    {
        var calc = new ShadowCalc(new List<Segment> { new Segment(1,2), new Segment(-1,-3)});

        // return ArgumentException
    }
}
