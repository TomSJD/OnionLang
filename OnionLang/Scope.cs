
namespace OnionLang
{
    public class Scope
    {
        public Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();

        public Variable? GetVariable(string identifier)
        {
            if (Variables.ContainsKey(identifier))
            {
                return Variables[identifier];
            }
            else if (OnionContent.Variables.ContainsKey(identifier))
            {
                 return OnionContent.Variables[identifier];
            }
            return null;
        }
    }
}
