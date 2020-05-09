
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
WHERE: 'where';
NOT_EQUAL: '!=';
NOT: '!';
LOGICAL_AND: '&&';
OR: '||';
DECREMENT: '--';
INCREMENT: '++';

STRING_VALUE: '\'' SingleStringCharacter* '\'';

//string

fragment SingleStringCharacter
    : ~['\\\r\n]
    | '\\' EscapeSequence
    | LineContinuation
    ;
fragment EscapeSequence
    : CharacterEscapeSequence
    | '0' // no digit ahead! TODO
    | HexEscapeSequence
    | UnicodeEscapeSequence
    | ExtendedUnicodeEscapeSequence
    ;

fragment CharacterEscapeSequence
    : SingleEscapeCharacter
    | NonEscapeCharacter
    ;

fragment HexEscapeSequence
      : 'x' HexDigit HexDigit
      ;
fragment UnicodeEscapeSequence
      : 'u' HexDigit HexDigit HexDigit HexDigit
      ;
fragment ExtendedUnicodeEscapeSequence
      : 'u' '{' HexDigit+ '}'
      ;
fragment SingleEscapeCharacter
      : ['"\\bfnrtv]
      ;

fragment NonEscapeCharacter
      : ~['"\\bfnrtv0-9xu\r\n]
      ;
fragment EscapeCharacter
      : SingleEscapeCharacter
      | [0-9]
      | [xu]
      ;
fragment LineContinuation
      : '\\' [\r\n\u2028\u2029]
      ;
fragment HexDigit
     : [0-9a-fA-F]
     ;

//------

NUMBER_VALUE: [0-9]+ ([.] [0-9]+)?;
BOOL_VALUE: 'true' | 'false';

VALUE: STRING_VALUE | NUMBER_VALUE | BOOL_VALUE;

//keywords
NOMINAL: 'nominal';
STRUCT : 'struct';
VOID: 'void';
ANY: 'any';
RETURN: 'return';
IF: 'if';
ABSTRACTION : 'abstraction';
MAIN: 'main';
NEW: 'new';
MUTABLE: 'mutable';
IS: 'is';
AND: 'and';
DEFAULT_VALUE: 'default';
TYPE: 'typeof';
ELSE: 'else';
FOR: 'for' ;
EXTERN: 'extern';
POLY: 'poly';
ARROW: '->';
MEMBER: 'member';
STATIC: 'static';
PRIVATE: 'private';
PRIMITIVE: 'num' | 'string' | 'bool' ;


fragment DIGIT  : '0'..'9' ;
fragment LETTER : 'a'..'z' | 'A'..'Z' | '_' ;
ARITHMETIC: ADD | TIMES | DIVIDE | SUBTRACT ;
LOGIC: LESS | LEQUAL | GREATER | GREQUAL | EQUAL | NOT_EQUAL | LOGICAL_AND | OR;

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

structDeclaration : NOMINAL? STRUCT ID ( '<' ID (',' ID)* '>' )? '{' ((PRIVATE? uninitializedVarDeclaration ';'?) | inlinedType)* '}'
                 ;


inlinedType : IS type ';'?
			;

//functions

functionDeclaration : (PRIVATE? (type | VOID) ID ('<' ID (',' ID)* '>')? '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' constraints? block)
					| (PRIVATE? (type | VOID) ID ('<' ID (',' ID)* '>')? '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' constraints? ARROW expression ';'?)
					| (PRIVATE? EXTERN MEMBER? (type | VOID) ID ('<' ID (',' ID)* '>')? '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' constraints? )
					| (PRIVATE? STATIC EXTERN (type | VOID) ID '.' ID ('<' ID (',' ID)* '>')? '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' constraints? )
                    ;

constraints : WHERE constrain (AND constrain)*
			;


constrain : type IS type
		  ;

//variables

uninitializedVarDeclaration : MUTABLE? type ID
                            ;

initializedVariableDeclaration : MUTABLE? type ID '=' expression
                               ;

inferedVariableDeclaration : MUTABLE? ID ':=' expression
						   ;

//types

type : ANY #anyType
	 | PRIMITIVE #primitiveType
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
		  | RETURN expression? ';'? #returnStatement
		  | inferedVariableDeclaration ';'? #inferedVariableDeclarationStatement
		  | initializedVariableDeclaration ';'? #initializedVariableDeclarationStatement
		  | uninitializedVarDeclaration ';'? #uninitializedVarDeclarationStatement
		  | expression ASSIGN expression ';'? #assignmentStatement
		  | expression ';'? #expressionStatement
		  ;

block: '{' statement* '}'
			   ;


expression : value=(NUMBER_VALUE | STRING_VALUE | BOOL_VALUE) #primitiveValue
           | uninitializedVarDeclaration? ARROW (block | expression)  #anonymusFunction
           | '(' (uninitializedVarDeclaration(',' uninitializedVarDeclaration)* )? ')' ARROW (block | expression)  #anonymusFunction
           | ID ('<'POLY'>')? ('<' type (',' type)* '>')? '(' (expression (',' expression)* )? ')' #functionCall
		   | expression '.' ID ('<'POLY'>')? ('<' type (',' type)* '>')? '(' (expression (',' expression)* )? ')' #functionCall
		   | type '.' DEFAULT_VALUE #defaultValue
		   | type '.' TYPE #typeExpression
		   | expression '.' ID #fieldAccess
		   | NEW type ('{' (ID '=' expression) (',' ID '=' expression)* '}')* #constructor
		   | ID #variableAccess
		   | LEFT_PARENT expression RIGHT_PARENT #parExpression
           | NOT expression #negation
           | SUBTRACT expression #arithmeticNegation
           | expression operator=( TIMES | DIVIDE ) expression #operation
           | expression operator=( ADD | SUBTRACT ) expression #operation
           | expression operator=( LESS | GREATER | LEQUAL | GREQUAL ) expression #operation
           | expression operator=( EQUAL | NOT_EQUAL ) expression #operation
           | expression LOGICAL_AND expression #operation
           | expression OR expression #operation
		   ;

