
using System.Runtime.InteropServices;
using OnionLang.Content;

namespace OnionLang
{

    public abstract class OnionContent
    {
        public static Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();
        public static Dictionary<string, Function> Functions = new Dictionary<string, Function>();

        public static Variable? GetVariable(string identifier)
        {
            if (Variables.ContainsKey(identifier))
            {
                return Variables[identifier];
            }
            return null;
        }

        public static Function? GetFunction(string identifier)
        {
            if (Functions.ContainsKey(identifier))
            {
                return Functions[identifier];
            }
            return null;
        }
    }

    public class Variable
    {
        public string identifier { get; }
        public object? value { get; }
        public string type { get; }

        public Variable(string identifier, object? value, string type)
        {
            this.identifier = identifier;
            this.value = value;
            this.type = type;
        }
    }

    public class Function
    {
        public string identifier { get; }
        public string returnType { get; }
        public List<Parameter> parameters { get; }
        public List<OnionParser.LineContext> lines { get; }

        public Function(string identifier, string returnType, List<Parameter> parameters, List<OnionParser.LineContext> lines)
        {
            this.identifier = identifier;
            this.returnType = returnType;
            this.parameters = parameters;
            this.lines = lines;
        }
    }

    public class Parameter
    {
        public string identifier { get; }
        public string type { get; }

        public Parameter(string identifier, string type)
        {
            this.identifier = identifier;
            this.type = type;
        }
    }
}
