
using Antlr4.Runtime.Misc;
using OnionLang.Content;

namespace OnionLang
{
    public class OnionVisitor: OnionBaseVisitor<object?>
    {

        public override object? VisitAssignment([NotNull] OnionParser.AssignmentContext context)
        {
            string type = context.TYPE().GetText();
            string identifier = context.ID().GetText();
            var value = Visit(context.expression());

            switch (type)
            {
                case "int":
                    if (!(value is int))
                    {
                        throw new Exception("Type mismatch");
                    }
                    OnionContent.Variables.Add(identifier, new Variable(identifier, (int)value, type));
                    break;
                case "string":
                    if (!(value is string))
                    {
                        throw new Exception("Type mismatch");
                    }
                    OnionContent.Variables.Add(identifier, new Variable(identifier, (string)value, type));
                    break;
                case "boolean":
                    if (!(value is bool))
                    {
                        throw new Exception("Type mismatch");
                    }
                    OnionContent.Variables.Add(identifier, new Variable(identifier, (bool)value, type));
                    break;
                case "float":
                    if (!(value is float))
                    {
                        throw new Exception("Type mismatch");
                    }
                    OnionContent.Variables.Add(identifier, new Variable(identifier, (float)value, type));
                    break;
            }
            return null;
        }

        public override object VisitReAssignment([NotNull] OnionParser.ReAssignmentContext context)
        {
            string identifier = context.ID().GetText();
            var value = Visit(context.expression());

            if (OnionContent.GetVariable(identifier) is { } variable)
            {
                if (variable.value is null)
                {
                    throw new Exception("Variable does not have a value");
                }
                if (variable.value.GetType() != value.GetType())
                {
                    throw new Exception("Type mismatch");
                }
                OnionContent.Variables[identifier] = new Variable(identifier, value, variable.type);
            }
            else
            {
                throw new Exception("Variable does not exist");
            }
            return null;
        }

        public override object? VisitConstant([NotNull] OnionParser.ConstantContext context)
        {
            if (context.INTEGER() is { } i)      
                return int.Parse(i.GetText());
            if (context.FLOAT() is { } f)
                return float.Parse(f.GetText());
            if (context.STRING() is { } s)
                return s.GetText().Trim('\"');
            if (context.BOOLEAN() is { } b)
                return bool.Parse(b.GetText());
            if (context.GetText() == "null")
                return null;

            throw new NotImplementedException();
        }

        public override object? VisitIdExpression([NotNull] OnionParser.IdExpressionContext context)
        {
            string identifier = context.ID().GetText();
            if (OnionContent.GetVariable(identifier) is { } variable)
            {
                return variable.value;
            }
            throw new Exception("Variable does not exist");
        }

        public override object VisitAdditiveExpression([NotNull] OnionParser.AdditiveExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            switch (context.ADDOP().GetText())
            {
                case "+":                  
                    if (left is int && right is int)
                    {
                        return (int)left + (int)right;
                    } else if (left is float && right is float)
                    {
                        return (float)left + (float)right;
                    } else if (left is string && right is string)
                    {
                        return (string)left + (string)right;
                    } else
                    {
                        throw new Exception("Type mismatch");
                    }
                case "-":
                    if (left is int && right is int)
                    {
                        return (int)left - (int)right;
                    } else if (left is float && right is float)
                    {
                        return (float)left - (float)right;
                    } else
                    {
                        throw new Exception("Type mismatch");
                    }
            }

            throw new NotImplementedException();
        }

        public override object VisitMultiplicativeExpression([NotNull] OnionParser.MultiplicativeExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            switch (context.MULOP().GetText())
            {
                case "*":
                    if (left is int && right is int)
                    {
                        return (int)left * (int)right;
                    } else if (left is float && right is float)
                    {
                        return (float)left * (float)right;
                    } else
                    {
                        throw new Exception("Type mismatch");
                    }
                case "/":
                    if (left is int && right is int)
                    {
                        return (int)left / (int)right;
                    } else if (left is float && right is float)
                    {
                        return (float)left / (float)right;
                    } else
                    {
                        throw new Exception("Type mismatch");
                    }
                case "%":
                    if (left is int && right is int)
                    {
                        return (int)left % (int)right;
                    } else
                    {
                        throw new Exception("Type mismatch");
                    }
            }

            throw new NotImplementedException();
        }

        public override object VisitComparisonExpression([NotNull] OnionParser.ComparisonExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            switch (context.COMPOP().GetText())
            {
                case "<":
                    if (left is int && right is int)
                    {
                        return (int)left < (int)right;
                    }
                    else if (left is float && right is float)
                    {
                        return (float)left < (float)right;
                    }
                    else
                    {
                        throw new Exception("Type mismatch");
                    }
                case ">":
                    if (left is int && right is int)
                    {
                        return (int)left > (int)right;
                    }
                    else if (left is float && right is float)
                    {
                        return (float)left > (float)right;
                    }
                    else
                    {
                        throw new Exception("Type mismatch");
                    }
                case "<=":
                    if (left is int && right is int)
                    {
                        return (int)left <= (int)right;
                    }
                    else if (left is float && right is float)
                    {
                        return (float)left <= (float)right;
                    }
                    else
                    {
                        throw new Exception("Type mismatch");
                    }
                case ">=":
                    if (left is int && right is int)
                    {
                        return (int)left >= (int)right;
                    }
                    else if (left is float && right is float)
                    {
                        return (float)left >= (float)right;
                    }
                    else
                    {
                        throw new Exception("Type mismatch");
                    }
                case "==":
                    if (left is int && right is int)
                    {
                        return (int)left == (int)right;
                    }
                    else if (left is float && right is float)
                    {
                        return (float)left == (float)right;
                    }
                    else if (left is string && right is string)
                    {
                        return (string)left == (string)right;
                    }
                    else if (left is bool && right is bool)
                    {
                        return (bool)left == (bool)right;
                    }
                    else
                    {
                        throw new Exception("Type mismatch");
                    }
                case "!=":
                    if (left is int && right is int)
                    {
                        return (int)left != (int)right;
                    }
                    else if (left is float && right is float)
                    {
                        return (float)left != (float)right;
                    }
                    else if (left is string && right is string)
                    {
                        return (string)left != (string)right;
                    }
                    else if (left is bool && right is bool)
                    {
                        return (bool)left != (bool)right;
                    }
                    else
                    {
                        throw new Exception("Type mismatch");
                    }
            }

            throw new NotImplementedException();
        }

        public override object VisitTernaryExpression([NotNull] OnionParser.TernaryExpressionContext context)
        {
            var conditionExpression = Visit(context.expression(0));
            var trueExpression = Visit(context.expression(1));
            var falseExpression = Visit(context.expression(2));

            if (!(conditionExpression is { }))
                throw new Exception("Condition is null");
            if (!(trueExpression is { }))
                throw new Exception("True expression is null");
            if (!(falseExpression is { }))
                throw new Exception("False expression is null");

            if (conditionExpression is bool condition)
            {
                return condition ? trueExpression : falseExpression;
            }
            throw new Exception("Type mismatch");
        }

        public override object? VisitParenExpression([NotNull] OnionParser.ParenExpressionContext context)
        {
            return Visit(context.expression());
        }

        public override object? VisitFunctionCall([NotNull] OnionParser.FunctionCallContext context)
        {
            string identifier = context.ID().GetText();
            if (OnionContent.GetFunction(identifier) is { } function)
            {
                if (function.parameters.Count != context.expression().Length)
                {
                    throw new Exception("Parameter count mismatch");
                }
                for (int i = 0; i < function.parameters.Count; i++)
                {
                    var value = Visit(context.expression(i));
                    if (value is null)
                    {
                        throw new Exception("Parameter is null");
                    }
                    if (value.GetType().ToString() != function.parameters[i].type)
                    {
                        throw new Exception("Type mismatch");
                    }
                }

                Scope scope = new Scope();

                for (int i = 0; i < function.parameters.Count; i++)
                {
                    scope.Variables.Add(function.parameters[i].identifier, new Variable(function.parameters[i].identifier, Visit(context.expression(i)), function.parameters[i].type));
                }

                foreach (var line in function.lines)
                {
                    Visit(line);
                }

                if (function.returnType == "void")
                {
                    return null;
                } else
                {
                    var value = Visit(function.lines[function.lines.Count - 1]);
                    if (value is null)
                    {
                        throw new Exception("Function does not return a value");
                    } else if (value.GetType().ToString() != function.returnType)
                    {
                        throw new Exception("Type mismatch");
                    } else
                    {
                        return value;
                    }
                }
            }
            throw new Exception("Function does not exist");
        }

        public override object? VisitWriteStatement([NotNull] OnionParser.WriteStatementContext context)
        {
            for (int i = 0; i < context.expression().Length; i++)
            {
                var value = Visit(context.expression(i));
                if (value is null)
                {
                    Console.Write("null");
                } else
                {
                    Console.Write(value);
                }
            }
            return null;         
        }

        public override object VisitWhileBlock([NotNull] OnionParser.WhileBlockContext context)
        {
            var condition = Visit(context.expression());
            if (condition is bool conditionValue)
            {
                while (conditionValue)
                {
                    foreach (var line in context.line())
                    {
                        Visit(line);
                    }
                    conditionValue = (bool) Visit(context.expression());
                }
                return null;
            }
            throw new Exception("Type mismatch");
        }
    }
}
