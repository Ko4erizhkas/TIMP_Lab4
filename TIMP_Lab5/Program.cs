
namespace TIMP_Lab5;
internal class Program
{
    static void Main(string[] args)
    {
        string expression = "12+13+53*195/123^84*9-2";
        POLIZ poliz = new POLIZ(expression);

        Console.WriteLine(poliz.Calc());
        Console.WriteLine(poliz.postfixExpr);
    }
}
