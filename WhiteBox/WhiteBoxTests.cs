using TIMP_Lab4;

namespace WhiteBox;

[TestClass]
public class WhiteBoxTesting
{
    [TestClass]
    public class ShadowCalculatorTests
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
        public void EmptyList_ReturnsZero()
        {
            var calc = Make();
            long result = calc.CalcTotalLength();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SingleSegment_ReturnsLength()
        {
            var calc = Make((1, 5));
            long result = calc.CalcTotalLength();
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void DisjointSegments_ReturnsSumOfLengths()
        {
            var calc = Make((1, 3), (5, 7), (10, 12));
            long result = calc.CalcTotalLength();
            // (3-1) + (7-5) + (12-10) = 2 + 2 + 2 = 6
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void FullyNestedSegments_IgnoresInner()
        {
            var calc = Make((1, 10), (3, 5));
            long result = calc.CalcTotalLength();
            // Только внешний: 10 - 1 = 9
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void OverlappingSegments_ExpandsCorrectly()
        {
            var calc = Make((1, 5), (4, 8), (7, 10));
            long result = calc.CalcTotalLength();
            // Объединение [1,10] → 9
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void TouchingSegments_AreMerged()
        {
            var calc = Make((1, 5), (5, 8));
            long result = calc.CalcTotalLength();
            // [1,8] → 7
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void UnsortedInput_SortsBeforeMerging()
        {
            var calc = Make((5, 8), (1, 3), (2, 6), (10, 12));
            long result = calc.CalcTotalLength();
            // После сортировки и слияния: [1,8] + [10,12] = 7 + 2 = 9
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void ZeroLengthSegments_Ignored()
        {
            var calc = Make((1, 1), (1, 1), (2, 2));
            long result = calc.CalcTotalLength();
            // Все отрезки нулевой длины → 0
            Assert.AreEqual(0, result);
        }
    }
}
