grammar Jur;

/*
 * Lexer Rules
 */

//special
LINE_COMMENT : '//' ~[\r\n]* -> channel(HIDDEN);
WHITESPACE: [ \t\f]+ -> channel(HIDDEN);
NEWLINE : '\r'? '\n' -> channel(HIDDEN);

COMA: ',';
DOT : '.';
SEMICOLON : ';';
DOLLAR: '$';
ASSIGN: '=';
ADD : '+';
SUBTRACT: '-';
TIMES : '*';
DIVIDE: '/';
LEFT_CURLY: '{';
RIGHT_CURLY : '}';
LEFT_PARENT : '(';
RIGHT_PARENT: ')';
LEFT_SQUARE_PARENT: '[';
RIGHT_SQUARE_PARENT: ']';
LESS: '<';
LEQUAL: '<=';
GREATER: '>';
GREQUAL: '>=';
EQUAL: '==';
IS: 'is';
WHERE: 'where';
NOT_EQUAL: '!=';
AND: 'and' | '&&';
OR: 'or' | '||';

STRING_VALUE: '\''.*?'\'';
NUMBER_VALUE: [-]?[0-9]+ ([.] [0-9]+)?;
BOOL_VALUE: 'true' | 'false';
NULL_VALUE: 'null';

VALUE: STRING_VALUE | NUMBER_VALUE | BOOL_VALUE | NULL_VALUE;

//keywords
STRUCT : 'struct';
VOID: 'void';
RETURN: 'return';
BREAK: 'break';
CONTINUE: 'continue';
EXIT: 'exit';
IF: 'if';
WHILE : 'while';
ABSTRACTION : 'abstraction';
MAIN: 'main';
NEW: 'new';
IN: 'in';
ELSE: 'else';
FOR: 'for' ;
EXTERN: 'extern';
LET: 'let';
POLY: 'poly';
ARROW: '->';
PRIMITIVE: 'num' | 'string' | 'bool' ;


fragment DIGIT  : '0'..'9' ;
fragment LETTER : 'a'..'z' | 'A'..'Z' | '_' ;
ARITHMETIC: ADD | TIMES | DIVIDE | SUBTRACT ;
LOGIC: LESS | LEQUAL | GREATER | GREQUAL | EQUAL | NOT_EQUAL | AND | OR;

ID : LETTER (LETTER | DIGIT)* ;

/*
 * Parser Rules
 */


//structure

program : (main | abstraction)* EOF
		;

main : MAIN statement
     ;

abstraction : ABSTRACTION NUMBER_VALUE '{' (functionDeclaration | structDeclaration )* '}'
            ;

//structs and constrains

structDeclaration : EXTERN? STRUCT ID ( '<' ID (',' ID)* '>' )? '{' ((uninitializedVarDeclaration ';') | inlinedType)* '}'
                 ;


inlinedType : IS type ';'
			;

//functions

functionDeclaration : ( (type | VOID) ID ('<' ID (',' ID)* '>')? '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' constraints? statement)
					| ( EXTERN (type | VOID) ID ('<' ID (',' ID)* '>')? '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' constraints? )
                    ;

constraints : WHERE constrain (AND constrain)*
			;


constrain : type IS type
		  ;

//variables

uninitializedVarDeclaration : type ID
                            ;

initializedVariableDeclaration : type ID '=' expression
                               ;

inferedVariableDeclaration : ID ':=' expression
						   | LET ID '=' expression
						   ;

//types

type : PRIMITIVE #primitiveType
     | ID #typeParameterOrStructType
     | ID ('<' type (',' type)* '>') #genericStructType
     | type '(' (type (',' type)*)? ')' #functionPointerType
     | VOID '(' (type (',' type)*)? ')'#functionPointerType
     | type '[' ']' #arrayType
	 ;

//statements and expressions


statement : '{' statement* '}' #blockStatement
		  | IF expression statement (ELSE statement)? #ifStatement
		  | FOR ((initializedVariableDeclaration | inferedVariableDeclaration) ';')? expression (';' expression)? statement #forStatement
		  | FOR uninitializedVarDeclaration (IN | ':') expression statement #foreachStatement
		  | RETURN expression? ';' #returnStatement
		  | BREAK ';' #breakStatement
		  | CONTINUE ';' #continueStatement
		  | inferedVariableDeclaration ';' #inferedVariableDeclarationStatement
		  | initializedVariableDeclaration ';' #initializedVariableDeclarationStatement
		  | uninitializedVarDeclaration ';' #uninitializedVarDeclarationStatement
		  | expression ASSIGN expression ';' #assignmentStatement
		  | expression ';' #expressionStatement
		  | EXIT '(' STRING_VALUE ')' #exitStatement
		  ;

block: '{' statement* '}'
			   ;

expression : ID #variableAccess
           | value=(NUMBER_VALUE | STRING_VALUE | BOOL_VALUE | NULL_VALUE ) #primitiveValue
           | uninitializedVarDeclaration? ARROW (block | expression)  #anonymusFunction
           | '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' ARROW (block | expression)  #anonymusFunction
           | ID ('<'POLY'>')? ('<' type (',' type)* '>')? '(' (expression (',' expression)* )? ')' #functionCall
		   | expression '.' ID ('<'POLY'>')? ('<' type (',' type)* '>')? '(' (expression (',' expression)* )? ')' #functionCall
		   | expression '.' ID #fieldAccess
		   | NEW type #constructor
		   | expression LEFT_SQUARE_PARENT expression RIGHT_SQUARE_PARENT #arrayIndexAccess
		   | LEFT_PARENT expression RIGHT_PARENT #parExpression
           | expression operator=( TIMES | DIVIDE ) expression #operation
           | expression operator=( ADD | SUBTRACT ) expression #operation
           | expression operator=( LESS | GREATER | LEQUAL | GREQUAL ) expression #operation
           | expression operator=( EQUAL | IS | NOT_EQUAL ) expression #operation
           | expression AND expression #operation
           | expression OR expression #operation
		   ;

