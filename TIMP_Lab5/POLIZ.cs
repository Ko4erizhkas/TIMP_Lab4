namespace TIMP_Lab5;
public class POLIZ
{
    public string infixExpr { get; private set; }
    public string postfixExpr { get; private set; }

    private readonly Dictionary<char, int> operPriority = new Dictionary<char, int>
    {
        { '(', 0},
        { '+', 1},
        { '-', 1},
        { '*', 2},
        { '/', 2},
        { '^', 3},
        { '~', 4}, // Унарный минус
    };

    public POLIZ (string expression)
    {
        if (expression.Count() != 0)
        {
            infixExpr = expression;
            postfixExpr = ToPostfix(expression + '\r');
        }
        else throw new ArgumentException("Void expression");
        
    }
    private string GetStringNumber(string expr, ref int pos)
    {
        string strNumber = "";
        for (; pos < expr.Length; pos++)
        {
            char num = expr[pos];
            if (Char.IsDigit(num))
            {
                strNumber += num;
            }
            else
            {
                pos--;
                break;
            }
        }
        return strNumber;
    }
    private string ToPostfix(string infixExpr)
    {
        string postfixExpr = "";
        Stack<char> stack = new();

        for (int i = 0; i < infixExpr.Length; i++)
        {
            char c = infixExpr[i];

            if (Char.IsDigit(c))
            {
                postfixExpr += GetStringNumber(infixExpr, ref i) + " ";
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                    postfixExpr += stack.Pop();
                stack.Pop();
            }
            else if (operPriority.ContainsKey(c))
            {
                char op = c;
                if (op == '-' && (i == 0 || (i > 1 && operPriority.ContainsKey(infixExpr[i - 1]))))
                    op = '~';

                while (stack.Count > 0 && (operPriority[stack.Peek()] >= operPriority[op]))
                    postfixExpr += stack.Pop();
                stack.Push(op);
            }
        }
        foreach (char op in stack)
            postfixExpr += op;

        return postfixExpr;
    }
    private double Execute(char op, double first, double second) => op switch
    {
        '+' => first + second,
        '-' => first - second,
        '*' => first * second,
        '/' => (first == 0) || (second == 0) ? throw new DivideByZeroException("Деление на ноль") : first / second,
        '^' => Math.Pow(first, second),
        _ => throw new InvalidOperationException("Неизвестный оператор")
    };

    public double Calc()
    {
        Stack<double> locals = new();
        int counter = 0;

        for (int i = 0; i < postfixExpr.Length; i++)
        {
            char c = postfixExpr[i];

            if (Char.IsDigit(c))
            {
                string number = GetStringNumber(postfixExpr, ref i);

                locals.Push(Convert.ToDouble(number));
            }
            else if (operPriority.ContainsKey(c))
            {

                counter += 1;
                
                if (c == '~')
                {
                    double last = locals.Count > 0 ? locals.Pop() : 0;

                    locals.Push(Execute('-', 0, last));

                    Console.WriteLine($"{counter}) {c}{last} = {locals.Peek()}");
                    continue;
                }

                double second = locals.Count > 0 ? locals.Pop() : 0,
                first = locals.Count > 0 ? locals.Pop() : 0;

                locals.Push(Execute(c, first, second));
                Console.WriteLine($"{counter}) {first} {c} {second} = {locals.Peek()}");
            }
        }
        return locals.Pop();
    }
}
