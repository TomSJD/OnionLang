using Antlr4.Runtime;
using OnionLang.Content;
using OnionLang;

string scriptSource = File.ReadAllText("C:\\Users\\plais\\source\\repos\\OnionLang\\OnionLang\\Content\\Code\\test1.onion");
AntlrInputStream input = new AntlrInputStream(scriptSource);
OnionLexer lexer = new OnionLexer(input);
CommonTokenStream tokens = new CommonTokenStream(lexer);
OnionParser parser = new OnionParser(tokens);
OnionParser.ProgramContext tree = parser.program();
OnionVisitor visitor = new OnionVisitor();
visitor.Visit(tree);