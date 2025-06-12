using TIMP_Lab5;
namespace WhiteBox_POLIZ;

[TestClass]
public class WhiteBox_POLIZ_PublicMethod_Calc
{
    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void Calc_DivideByZero_ReturnDivideByZeroException()
    {
        var target = new POLIZ("1/0");

        var method = target.Calc();

        //return DivideByZeroException
    }
    [TestMethod]
    public void Calc_MultiplyByZero_ReturnsZeroResult()
    {
        var target = new POLIZ("1*0");

        var method = target.Calc();

        Assert.AreEqual(0, method);
    }
    [TestMethod]
    public void Calc_EnterNormalExpression_ReturnCorrectCalculationResultToInt()
    {
        var target = new POLIZ("(13+3/(13*4)) * 10");

        var method = (int)target.Calc();

        Assert.AreEqual(130, method);
    }
}
