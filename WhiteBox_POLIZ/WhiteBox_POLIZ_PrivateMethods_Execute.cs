using System.Reflection;
using TIMP_Lab5;

namespace WhiteBox_POLIZ;
[TestClass]
public class WhiteBox_POLIZ_PrivateMethods_Execute
{
    [TestMethod]
    public void ReturnException_Private_Execute()
    {
        string expr = "3+4";
        POLIZ poliz = new POLIZ(expr);

        MethodInfo methodInfo = typeof(POLIZ).GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Выдаст ошибку если метод не существует
        if (methodInfo == null)
        { 
            Assert.Fail("Private method 'Execute' not found.");
        }

        try
        {
            methodInfo.Invoke(poliz, new object[] { '&', 2, 3 });
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (TargetInvocationException ex)
        {
            Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException));
            Assert.AreEqual("Неизвестный оператор", ex.InnerException.Message);
        }
    }
    [TestMethod]
    public void Private_ReturnException_Execute_DivideByZero_LeftNumIsZero()
    {
        string expr = "3+4";
        POLIZ poliz = new POLIZ(expr);

        MethodInfo methodInfo = typeof(POLIZ).GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo == null)
        {
            Assert.Fail("Private method 'Execute not found.'");
        }

        try 
        {
            methodInfo.Invoke(poliz, new object[] { '/', 0, 3 });
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (TargetInvocationException ex) 
        {
            Assert.IsInstanceOfType(ex.InnerException, typeof(DivideByZeroException));
            Assert.AreEqual("Деление на ноль", ex.InnerException.Message);
        }
    }
    [TestMethod]
    public void Private_ReturnException_Execute_DivideByZero_RightNumIsZero()
    {
        string expr = "3+4";
        POLIZ poliz = new POLIZ(expr);

        MethodInfo methodInfo = typeof(POLIZ).GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Instance);

        if (methodInfo == null)
        {
            Assert.Fail("Private method 'Execute not found.'");
        }


        try
        {
            methodInfo.Invoke(poliz, new object[] { '/', 3, 0 });
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (TargetInvocationException ex)
        {
            Assert.IsInstanceOfType(ex.InnerException, typeof(DivideByZeroException));
            Assert.AreEqual("Деление на ноль", ex.InnerException.Message);
        }
    }
}
