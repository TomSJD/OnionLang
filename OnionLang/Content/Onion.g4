grammar Onion;

program: line* EOF;

line: statement | ifBlock | whileBlock;

statement: (assignment | functionCall | reAssignment | functionAssignment | writeStatement) ';';

assignment: TYPE ID '=' expression;

reAssignment: ID '=' expression;

parameter: TYPE ID;

functionAssignment: 'function' ID '(' (parameter (',' parameter)*)? ')' '->' RETURNTYPE '{' line* '}';

functionCall: ID '(' (expression (',' expression)*)? ')';

ifBlock: 'if' '(' expression ')' '{' line* '}' (elseIfBlock | elseBlock)?;

elseIfBlock: 'else' 'if' '(' expression ')' '{' line* '}';

elseBlock: 'else' '{' line* '}';

whileBlock: 'while' '(' expression ')' '{' line* '}';

writeStatement: 'Write' '(' (expression (',' expression)*)? ')';

expression:
    constant                                            # constantExpression
    | ID                                                # idExpression
    | functionCall                                      # functionCallExpression
    | '(' expression ')'                                # parenExpression
    | expression MULOP expression                       # multiplicativeExpression
    | expression ADDOP expression                       # additiveExpression
    | expression '?' expression ':' expression          # ternaryExpression
    | expression BOOLEANOP expression                   # booleanExpression
    | expression COMPOP expression                      # comparisonExpression;

BOOLEANOP: '&&' | '||';

COMPOP: '==' | '!=' | '<' | '<=' | '>' | '>=';

constant: INTEGER | FLOAT | STRING | BOOLEAN | 'null';

INTEGER: [0-9]+;

FLOAT: [0-9]+ '.' [0-9]+;

STRING: '"' [a-zA-Z0-9_]+ '"';

BOOLEAN: 'true' | 'false';

TYPE: 'int' | 'float' | 'string' | 'boolean';

RETURNTYPE: TYPE | 'void';

ID: [a-zA-Z0-9_]+;

MULOP: '*' | '/' | '%';

ADDOP: '+' | '-';

WS: [ \t\r\n]+ -> skip;