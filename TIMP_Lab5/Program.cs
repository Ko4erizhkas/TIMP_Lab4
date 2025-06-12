
namespace TIMP_Lab5;
internal class Program
{
    static void Main(string[] args)
    {
        string expression = "()";
        POLIZ poliz = new POLIZ(expression);

        Console.WriteLine(poliz.Calc());
        Console.WriteLine(poliz.postfixExpr);
    }
}
