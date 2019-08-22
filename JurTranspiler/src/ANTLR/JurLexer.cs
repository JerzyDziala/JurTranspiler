//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/strator/Documents/GitHub/JurTranspiler/JurTranspiler/src/ANTLR/Jur.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public partial class JurLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, LINE_COMMENT=2, WHITESPACE=3, NEWLINE=4, COMA=5, DOT=6, SEMICOLON=7, 
		ASSIGN=8, ADD=9, SUBTRACT=10, TIMES=11, DIVIDE=12, LEFT_CURLY=13, RIGHT_CURLY=14, 
		LEFT_PARENT=15, RIGHT_PARENT=16, LEFT_SQUARE_PARENT=17, RIGHT_SQUARE_PARENT=18, 
		LESS=19, LEQUAL=20, GREATER=21, GREQUAL=22, EQUAL=23, WHERE=24, NOT_EQUAL=25, 
		NOT=26, LOGICAL_AND=27, OR=28, STRING_VALUE=29, NUMBER_VALUE=30, BOOL_VALUE=31, 
		NULL_VALUE=32, VALUE=33, STRUCT=34, VOID=35, ANY=36, RETURN=37, BREAK=38, 
		CONTINUE=39, IF=40, ABSTRACTION=41, MAIN=42, NEW=43, IS=44, AND=45, DEFAULT_VALUE=46, 
		TYPE=47, ELSE=48, FOR=49, EXTERN=50, POLY=51, ARROW=52, PRIMITIVE=53, 
		ARITHMETIC=54, LOGIC=55, ID=56;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "LINE_COMMENT", "WHITESPACE", "NEWLINE", "COMA", "DOT", "SEMICOLON", 
		"ASSIGN", "ADD", "SUBTRACT", "TIMES", "DIVIDE", "LEFT_CURLY", "RIGHT_CURLY", 
		"LEFT_PARENT", "RIGHT_PARENT", "LEFT_SQUARE_PARENT", "RIGHT_SQUARE_PARENT", 
		"LESS", "LEQUAL", "GREATER", "GREQUAL", "EQUAL", "WHERE", "NOT_EQUAL", 
		"NOT", "LOGICAL_AND", "OR", "STRING_VALUE", "SingleStringCharacter", "EscapeSequence", 
		"CharacterEscapeSequence", "HexEscapeSequence", "UnicodeEscapeSequence", 
		"ExtendedUnicodeEscapeSequence", "SingleEscapeCharacter", "NonEscapeCharacter", 
		"EscapeCharacter", "LineContinuation", "HexDigit", "NUMBER_VALUE", "BOOL_VALUE", 
		"NULL_VALUE", "VALUE", "STRUCT", "VOID", "ANY", "RETURN", "BREAK", "CONTINUE", 
		"IF", "ABSTRACTION", "MAIN", "NEW", "IS", "AND", "DEFAULT_VALUE", "TYPE", 
		"ELSE", "FOR", "EXTERN", "POLY", "ARROW", "PRIMITIVE", "DIGIT", "LETTER", 
		"ARITHMETIC", "LOGIC", "ID"
	};


	public JurLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public JurLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "':='", null, null, null, "','", "'.'", "';'", "'='", "'+'", "'-'", 
		"'*'", "'/'", "'{'", "'}'", "'('", "')'", "'['", "']'", "'<'", "'<='", 
		"'>'", "'>='", "'=='", "'where'", "'!='", "'!'", "'&&'", "'||'", null, 
		null, null, "'null'", null, "'struct'", "'void'", "'any'", "'return'", 
		"'break'", "'continue'", "'if'", "'abstraction'", "'main'", "'new'", "'is'", 
		"'and'", "'default'", "'typeof'", "'else'", "'for'", "'extern'", "'poly'", 
		"'->'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, "LINE_COMMENT", "WHITESPACE", "NEWLINE", "COMA", "DOT", "SEMICOLON", 
		"ASSIGN", "ADD", "SUBTRACT", "TIMES", "DIVIDE", "LEFT_CURLY", "RIGHT_CURLY", 
		"LEFT_PARENT", "RIGHT_PARENT", "LEFT_SQUARE_PARENT", "RIGHT_SQUARE_PARENT", 
		"LESS", "LEQUAL", "GREATER", "GREQUAL", "EQUAL", "WHERE", "NOT_EQUAL", 
		"NOT", "LOGICAL_AND", "OR", "STRING_VALUE", "NUMBER_VALUE", "BOOL_VALUE", 
		"NULL_VALUE", "VALUE", "STRUCT", "VOID", "ANY", "RETURN", "BREAK", "CONTINUE", 
		"IF", "ABSTRACTION", "MAIN", "NEW", "IS", "AND", "DEFAULT_VALUE", "TYPE", 
		"ELSE", "FOR", "EXTERN", "POLY", "ARROW", "PRIMITIVE", "ARITHMETIC", "LOGIC", 
		"ID"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Jur.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static JurLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', ':', '\x1DA', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x4', 
		'#', '\t', '#', '\x4', '$', '\t', '$', '\x4', '%', '\t', '%', '\x4', '&', 
		'\t', '&', '\x4', '\'', '\t', '\'', '\x4', '(', '\t', '(', '\x4', ')', 
		'\t', ')', '\x4', '*', '\t', '*', '\x4', '+', '\t', '+', '\x4', ',', '\t', 
		',', '\x4', '-', '\t', '-', '\x4', '.', '\t', '.', '\x4', '/', '\t', '/', 
		'\x4', '\x30', '\t', '\x30', '\x4', '\x31', '\t', '\x31', '\x4', '\x32', 
		'\t', '\x32', '\x4', '\x33', '\t', '\x33', '\x4', '\x34', '\t', '\x34', 
		'\x4', '\x35', '\t', '\x35', '\x4', '\x36', '\t', '\x36', '\x4', '\x37', 
		'\t', '\x37', '\x4', '\x38', '\t', '\x38', '\x4', '\x39', '\t', '\x39', 
		'\x4', ':', '\t', ':', '\x4', ';', '\t', ';', '\x4', '<', '\t', '<', '\x4', 
		'=', '\t', '=', '\x4', '>', '\t', '>', '\x4', '?', '\t', '?', '\x4', '@', 
		'\t', '@', '\x4', '\x41', '\t', '\x41', '\x4', '\x42', '\t', '\x42', '\x4', 
		'\x43', '\t', '\x43', '\x4', '\x44', '\t', '\x44', '\x4', '\x45', '\t', 
		'\x45', '\x4', '\x46', '\t', '\x46', '\x3', '\x2', '\x3', '\x2', '\x3', 
		'\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\a', '\x3', 
		'\x95', '\n', '\x3', '\f', '\x3', '\xE', '\x3', '\x98', '\v', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x4', '\x6', '\x4', '\x9D', '\n', '\x4', 
		'\r', '\x4', '\xE', '\x4', '\x9E', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x5', '\x5', '\x5', '\xA4', '\n', '\x5', '\x3', '\x5', '\x3', '\x5', 
		'\x3', '\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3', 
		'\a', '\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', 
		'\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', 
		'\r', '\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', '\xF', 
		'\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12', 
		'\x3', '\x12', '\x3', '\x13', '\x3', '\x13', '\x3', '\x14', '\x3', '\x14', 
		'\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x16', '\x3', '\x16', 
		'\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x18', '\x3', '\x18', 
		'\x3', '\x18', '\x3', '\x19', '\x3', '\x19', '\x3', '\x19', '\x3', '\x19', 
		'\x3', '\x19', '\x3', '\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1A', 
		'\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', 
		'\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1E', '\x3', '\x1E', 
		'\a', '\x1E', '\xE6', '\n', '\x1E', '\f', '\x1E', '\xE', '\x1E', '\xE9', 
		'\v', '\x1E', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', 
		'\x3', '\x1F', '\x3', '\x1F', '\x5', '\x1F', '\xF1', '\n', '\x1F', '\x3', 
		' ', '\x3', ' ', '\x3', ' ', '\x3', ' ', '\x3', ' ', '\x5', ' ', '\xF8', 
		'\n', ' ', '\x3', '!', '\x3', '!', '\x5', '!', '\xFC', '\n', '!', '\x3', 
		'\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '#', '\x3', '#', '\x3', 
		'#', '\x3', '#', '\x3', '#', '\x3', '#', '\x3', '$', '\x3', '$', '\x3', 
		'$', '\x6', '$', '\x10B', '\n', '$', '\r', '$', '\xE', '$', '\x10C', '\x3', 
		'$', '\x3', '$', '\x3', '%', '\x3', '%', '\x3', '&', '\x3', '&', '\x3', 
		'\'', '\x3', '\'', '\x5', '\'', '\x117', '\n', '\'', '\x3', '(', '\x3', 
		'(', '\x3', '(', '\x3', ')', '\x3', ')', '\x3', '*', '\x5', '*', '\x11F', 
		'\n', '*', '\x3', '*', '\x6', '*', '\x122', '\n', '*', '\r', '*', '\xE', 
		'*', '\x123', '\x3', '*', '\x3', '*', '\x6', '*', '\x128', '\n', '*', 
		'\r', '*', '\xE', '*', '\x129', '\x5', '*', '\x12C', '\n', '*', '\x3', 
		'+', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', 
		'+', '\x3', '+', '\x3', '+', '\x5', '+', '\x137', '\n', '+', '\x3', ',', 
		'\x3', ',', '\x3', ',', '\x3', ',', '\x3', ',', '\x3', '-', '\x3', '-', 
		'\x3', '-', '\x3', '-', '\x5', '-', '\x142', '\n', '-', '\x3', '.', '\x3', 
		'.', '\x3', '.', '\x3', '.', '\x3', '.', '\x3', '.', '\x3', '.', '\x3', 
		'/', '\x3', '/', '\x3', '/', '\x3', '/', '\x3', '/', '\x3', '\x30', '\x3', 
		'\x30', '\x3', '\x30', '\x3', '\x30', '\x3', '\x31', '\x3', '\x31', '\x3', 
		'\x31', '\x3', '\x31', '\x3', '\x31', '\x3', '\x31', '\x3', '\x31', '\x3', 
		'\x32', '\x3', '\x32', '\x3', '\x32', '\x3', '\x32', '\x3', '\x32', '\x3', 
		'\x32', '\x3', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', 
		'\x33', '\x3', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', 
		'\x34', '\x3', '\x34', '\x3', '\x34', '\x3', '\x35', '\x3', '\x35', '\x3', 
		'\x35', '\x3', '\x35', '\x3', '\x35', '\x3', '\x35', '\x3', '\x35', '\x3', 
		'\x35', '\x3', '\x35', '\x3', '\x35', '\x3', '\x35', '\x3', '\x35', '\x3', 
		'\x36', '\x3', '\x36', '\x3', '\x36', '\x3', '\x36', '\x3', '\x36', '\x3', 
		'\x37', '\x3', '\x37', '\x3', '\x37', '\x3', '\x37', '\x3', '\x38', '\x3', 
		'\x38', '\x3', '\x38', '\x3', '\x39', '\x3', '\x39', '\x3', '\x39', '\x3', 
		'\x39', '\x3', ':', '\x3', ':', '\x3', ':', '\x3', ':', '\x3', ':', '\x3', 
		':', '\x3', ':', '\x3', ':', '\x3', ';', '\x3', ';', '\x3', ';', '\x3', 
		';', '\x3', ';', '\x3', ';', '\x3', ';', '\x3', '<', '\x3', '<', '\x3', 
		'<', '\x3', '<', '\x3', '<', '\x3', '=', '\x3', '=', '\x3', '=', '\x3', 
		'=', '\x3', '>', '\x3', '>', '\x3', '>', '\x3', '>', '\x3', '>', '\x3', 
		'>', '\x3', '>', '\x3', '?', '\x3', '?', '\x3', '?', '\x3', '?', '\x3', 
		'?', '\x3', '@', '\x3', '@', '\x3', '@', '\x3', '\x41', '\x3', '\x41', 
		'\x3', '\x41', '\x3', '\x41', '\x3', '\x41', '\x3', '\x41', '\x3', '\x41', 
		'\x3', '\x41', '\x3', '\x41', '\x3', '\x41', '\x3', '\x41', '\x3', '\x41', 
		'\x3', '\x41', '\x5', '\x41', '\x1BD', '\n', '\x41', '\x3', '\x42', '\x3', 
		'\x42', '\x3', '\x43', '\x3', '\x43', '\x3', '\x44', '\x3', '\x44', '\x3', 
		'\x44', '\x3', '\x44', '\x5', '\x44', '\x1C7', '\n', '\x44', '\x3', '\x45', 
		'\x3', '\x45', '\x3', '\x45', '\x3', '\x45', '\x3', '\x45', '\x3', '\x45', 
		'\x3', '\x45', '\x3', '\x45', '\x5', '\x45', '\x1D1', '\n', '\x45', '\x3', 
		'\x46', '\x3', '\x46', '\x3', '\x46', '\a', '\x46', '\x1D6', '\n', '\x46', 
		'\f', '\x46', '\xE', '\x46', '\x1D9', '\v', '\x46', '\x2', '\x2', 'G', 
		'\x3', '\x3', '\x5', '\x4', '\a', '\x5', '\t', '\x6', '\v', '\a', '\r', 
		'\b', '\xF', '\t', '\x11', '\n', '\x13', '\v', '\x15', '\f', '\x17', '\r', 
		'\x19', '\xE', '\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', '!', '\x12', 
		'#', '\x13', '%', '\x14', '\'', '\x15', ')', '\x16', '+', '\x17', '-', 
		'\x18', '/', '\x19', '\x31', '\x1A', '\x33', '\x1B', '\x35', '\x1C', '\x37', 
		'\x1D', '\x39', '\x1E', ';', '\x1F', '=', '\x2', '?', '\x2', '\x41', '\x2', 
		'\x43', '\x2', '\x45', '\x2', 'G', '\x2', 'I', '\x2', 'K', '\x2', 'M', 
		'\x2', 'O', '\x2', 'Q', '\x2', 'S', ' ', 'U', '!', 'W', '\"', 'Y', '#', 
		'[', '$', ']', '%', '_', '&', '\x61', '\'', '\x63', '(', '\x65', ')', 
		'g', '*', 'i', '+', 'k', ',', 'm', '-', 'o', '.', 'q', '/', 's', '\x30', 
		'u', '\x31', 'w', '\x32', 'y', '\x33', '{', '\x34', '}', '\x35', '\x7F', 
		'\x36', '\x81', '\x37', '\x83', '\x2', '\x85', '\x2', '\x87', '\x38', 
		'\x89', '\x39', '\x8B', ':', '\x3', '\x2', '\xE', '\x4', '\x2', '\f', 
		'\f', '\xF', '\xF', '\x5', '\x2', '\v', '\v', '\xE', '\xE', '\"', '\"', 
		'\x6', '\x2', '\f', '\f', '\xF', '\xF', ')', ')', '^', '^', '\v', '\x2', 
		'$', '$', ')', ')', '^', '^', '\x64', '\x64', 'h', 'h', 'p', 'p', 't', 
		't', 'v', 'v', 'x', 'x', '\xE', '\x2', '\f', '\f', '\xF', '\xF', '$', 
		'$', ')', ')', '\x32', ';', '^', '^', '\x64', '\x64', 'h', 'h', 'p', 'p', 
		't', 't', 'v', 'x', 'z', 'z', '\x5', '\x2', '\x32', ';', 'w', 'w', 'z', 
		'z', '\x5', '\x2', '\f', '\f', '\xF', '\xF', '\x202A', '\x202B', '\x5', 
		'\x2', '\x32', ';', '\x43', 'H', '\x63', 'h', '\x3', '\x2', '/', '/', 
		'\x3', '\x2', '\x32', ';', '\x3', '\x2', '\x30', '\x30', '\x5', '\x2', 
		'\x43', '\\', '\x61', '\x61', '\x63', '|', '\x2', '\x1EF', '\x2', '\x3', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', '\x2', '\x11', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1B', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', ')', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', '\x2', '-', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', '\x37', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', '\x2', '\x2', 
		';', '\x3', '\x2', '\x2', '\x2', '\x2', 'S', '\x3', '\x2', '\x2', '\x2', 
		'\x2', 'U', '\x3', '\x2', '\x2', '\x2', '\x2', 'W', '\x3', '\x2', '\x2', 
		'\x2', '\x2', 'Y', '\x3', '\x2', '\x2', '\x2', '\x2', '[', '\x3', '\x2', 
		'\x2', '\x2', '\x2', ']', '\x3', '\x2', '\x2', '\x2', '\x2', '_', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x61', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x63', '\x3', '\x2', '\x2', '\x2', '\x2', '\x65', '\x3', '\x2', '\x2', 
		'\x2', '\x2', 'g', '\x3', '\x2', '\x2', '\x2', '\x2', 'i', '\x3', '\x2', 
		'\x2', '\x2', '\x2', 'k', '\x3', '\x2', '\x2', '\x2', '\x2', 'm', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'o', '\x3', '\x2', '\x2', '\x2', '\x2', 'q', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 's', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'u', '\x3', '\x2', '\x2', '\x2', '\x2', 'w', '\x3', '\x2', '\x2', '\x2', 
		'\x2', 'y', '\x3', '\x2', '\x2', '\x2', '\x2', '{', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '}', '\x3', '\x2', '\x2', '\x2', '\x2', '\x7F', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x81', '\x3', '\x2', '\x2', '\x2', '\x2', '\x87', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x89', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x8B', '\x3', '\x2', '\x2', '\x2', '\x3', '\x8D', '\x3', '\x2', 
		'\x2', '\x2', '\x5', '\x90', '\x3', '\x2', '\x2', '\x2', '\a', '\x9C', 
		'\x3', '\x2', '\x2', '\x2', '\t', '\xA3', '\x3', '\x2', '\x2', '\x2', 
		'\v', '\xA9', '\x3', '\x2', '\x2', '\x2', '\r', '\xAB', '\x3', '\x2', 
		'\x2', '\x2', '\xF', '\xAD', '\x3', '\x2', '\x2', '\x2', '\x11', '\xAF', 
		'\x3', '\x2', '\x2', '\x2', '\x13', '\xB1', '\x3', '\x2', '\x2', '\x2', 
		'\x15', '\xB3', '\x3', '\x2', '\x2', '\x2', '\x17', '\xB5', '\x3', '\x2', 
		'\x2', '\x2', '\x19', '\xB7', '\x3', '\x2', '\x2', '\x2', '\x1B', '\xB9', 
		'\x3', '\x2', '\x2', '\x2', '\x1D', '\xBB', '\x3', '\x2', '\x2', '\x2', 
		'\x1F', '\xBD', '\x3', '\x2', '\x2', '\x2', '!', '\xBF', '\x3', '\x2', 
		'\x2', '\x2', '#', '\xC1', '\x3', '\x2', '\x2', '\x2', '%', '\xC3', '\x3', 
		'\x2', '\x2', '\x2', '\'', '\xC5', '\x3', '\x2', '\x2', '\x2', ')', '\xC7', 
		'\x3', '\x2', '\x2', '\x2', '+', '\xCA', '\x3', '\x2', '\x2', '\x2', '-', 
		'\xCC', '\x3', '\x2', '\x2', '\x2', '/', '\xCF', '\x3', '\x2', '\x2', 
		'\x2', '\x31', '\xD2', '\x3', '\x2', '\x2', '\x2', '\x33', '\xD8', '\x3', 
		'\x2', '\x2', '\x2', '\x35', '\xDB', '\x3', '\x2', '\x2', '\x2', '\x37', 
		'\xDD', '\x3', '\x2', '\x2', '\x2', '\x39', '\xE0', '\x3', '\x2', '\x2', 
		'\x2', ';', '\xE3', '\x3', '\x2', '\x2', '\x2', '=', '\xF0', '\x3', '\x2', 
		'\x2', '\x2', '?', '\xF7', '\x3', '\x2', '\x2', '\x2', '\x41', '\xFB', 
		'\x3', '\x2', '\x2', '\x2', '\x43', '\xFD', '\x3', '\x2', '\x2', '\x2', 
		'\x45', '\x101', '\x3', '\x2', '\x2', '\x2', 'G', '\x107', '\x3', '\x2', 
		'\x2', '\x2', 'I', '\x110', '\x3', '\x2', '\x2', '\x2', 'K', '\x112', 
		'\x3', '\x2', '\x2', '\x2', 'M', '\x116', '\x3', '\x2', '\x2', '\x2', 
		'O', '\x118', '\x3', '\x2', '\x2', '\x2', 'Q', '\x11B', '\x3', '\x2', 
		'\x2', '\x2', 'S', '\x11E', '\x3', '\x2', '\x2', '\x2', 'U', '\x136', 
		'\x3', '\x2', '\x2', '\x2', 'W', '\x138', '\x3', '\x2', '\x2', '\x2', 
		'Y', '\x141', '\x3', '\x2', '\x2', '\x2', '[', '\x143', '\x3', '\x2', 
		'\x2', '\x2', ']', '\x14A', '\x3', '\x2', '\x2', '\x2', '_', '\x14F', 
		'\x3', '\x2', '\x2', '\x2', '\x61', '\x153', '\x3', '\x2', '\x2', '\x2', 
		'\x63', '\x15A', '\x3', '\x2', '\x2', '\x2', '\x65', '\x160', '\x3', '\x2', 
		'\x2', '\x2', 'g', '\x169', '\x3', '\x2', '\x2', '\x2', 'i', '\x16C', 
		'\x3', '\x2', '\x2', '\x2', 'k', '\x178', '\x3', '\x2', '\x2', '\x2', 
		'm', '\x17D', '\x3', '\x2', '\x2', '\x2', 'o', '\x181', '\x3', '\x2', 
		'\x2', '\x2', 'q', '\x184', '\x3', '\x2', '\x2', '\x2', 's', '\x188', 
		'\x3', '\x2', '\x2', '\x2', 'u', '\x190', '\x3', '\x2', '\x2', '\x2', 
		'w', '\x197', '\x3', '\x2', '\x2', '\x2', 'y', '\x19C', '\x3', '\x2', 
		'\x2', '\x2', '{', '\x1A0', '\x3', '\x2', '\x2', '\x2', '}', '\x1A7', 
		'\x3', '\x2', '\x2', '\x2', '\x7F', '\x1AC', '\x3', '\x2', '\x2', '\x2', 
		'\x81', '\x1BC', '\x3', '\x2', '\x2', '\x2', '\x83', '\x1BE', '\x3', '\x2', 
		'\x2', '\x2', '\x85', '\x1C0', '\x3', '\x2', '\x2', '\x2', '\x87', '\x1C6', 
		'\x3', '\x2', '\x2', '\x2', '\x89', '\x1D0', '\x3', '\x2', '\x2', '\x2', 
		'\x8B', '\x1D2', '\x3', '\x2', '\x2', '\x2', '\x8D', '\x8E', '\a', '<', 
		'\x2', '\x2', '\x8E', '\x8F', '\a', '?', '\x2', '\x2', '\x8F', '\x4', 
		'\x3', '\x2', '\x2', '\x2', '\x90', '\x91', '\a', '\x31', '\x2', '\x2', 
		'\x91', '\x92', '\a', '\x31', '\x2', '\x2', '\x92', '\x96', '\x3', '\x2', 
		'\x2', '\x2', '\x93', '\x95', '\n', '\x2', '\x2', '\x2', '\x94', '\x93', 
		'\x3', '\x2', '\x2', '\x2', '\x95', '\x98', '\x3', '\x2', '\x2', '\x2', 
		'\x96', '\x94', '\x3', '\x2', '\x2', '\x2', '\x96', '\x97', '\x3', '\x2', 
		'\x2', '\x2', '\x97', '\x99', '\x3', '\x2', '\x2', '\x2', '\x98', '\x96', 
		'\x3', '\x2', '\x2', '\x2', '\x99', '\x9A', '\b', '\x3', '\x2', '\x2', 
		'\x9A', '\x6', '\x3', '\x2', '\x2', '\x2', '\x9B', '\x9D', '\t', '\x3', 
		'\x2', '\x2', '\x9C', '\x9B', '\x3', '\x2', '\x2', '\x2', '\x9D', '\x9E', 
		'\x3', '\x2', '\x2', '\x2', '\x9E', '\x9C', '\x3', '\x2', '\x2', '\x2', 
		'\x9E', '\x9F', '\x3', '\x2', '\x2', '\x2', '\x9F', '\xA0', '\x3', '\x2', 
		'\x2', '\x2', '\xA0', '\xA1', '\b', '\x4', '\x2', '\x2', '\xA1', '\b', 
		'\x3', '\x2', '\x2', '\x2', '\xA2', '\xA4', '\a', '\xF', '\x2', '\x2', 
		'\xA3', '\xA2', '\x3', '\x2', '\x2', '\x2', '\xA3', '\xA4', '\x3', '\x2', 
		'\x2', '\x2', '\xA4', '\xA5', '\x3', '\x2', '\x2', '\x2', '\xA5', '\xA6', 
		'\a', '\f', '\x2', '\x2', '\xA6', '\xA7', '\x3', '\x2', '\x2', '\x2', 
		'\xA7', '\xA8', '\b', '\x5', '\x2', '\x2', '\xA8', '\n', '\x3', '\x2', 
		'\x2', '\x2', '\xA9', '\xAA', '\a', '.', '\x2', '\x2', '\xAA', '\f', '\x3', 
		'\x2', '\x2', '\x2', '\xAB', '\xAC', '\a', '\x30', '\x2', '\x2', '\xAC', 
		'\xE', '\x3', '\x2', '\x2', '\x2', '\xAD', '\xAE', '\a', '=', '\x2', '\x2', 
		'\xAE', '\x10', '\x3', '\x2', '\x2', '\x2', '\xAF', '\xB0', '\a', '?', 
		'\x2', '\x2', '\xB0', '\x12', '\x3', '\x2', '\x2', '\x2', '\xB1', '\xB2', 
		'\a', '-', '\x2', '\x2', '\xB2', '\x14', '\x3', '\x2', '\x2', '\x2', '\xB3', 
		'\xB4', '\a', '/', '\x2', '\x2', '\xB4', '\x16', '\x3', '\x2', '\x2', 
		'\x2', '\xB5', '\xB6', '\a', ',', '\x2', '\x2', '\xB6', '\x18', '\x3', 
		'\x2', '\x2', '\x2', '\xB7', '\xB8', '\a', '\x31', '\x2', '\x2', '\xB8', 
		'\x1A', '\x3', '\x2', '\x2', '\x2', '\xB9', '\xBA', '\a', '}', '\x2', 
		'\x2', '\xBA', '\x1C', '\x3', '\x2', '\x2', '\x2', '\xBB', '\xBC', '\a', 
		'\x7F', '\x2', '\x2', '\xBC', '\x1E', '\x3', '\x2', '\x2', '\x2', '\xBD', 
		'\xBE', '\a', '*', '\x2', '\x2', '\xBE', ' ', '\x3', '\x2', '\x2', '\x2', 
		'\xBF', '\xC0', '\a', '+', '\x2', '\x2', '\xC0', '\"', '\x3', '\x2', '\x2', 
		'\x2', '\xC1', '\xC2', '\a', ']', '\x2', '\x2', '\xC2', '$', '\x3', '\x2', 
		'\x2', '\x2', '\xC3', '\xC4', '\a', '_', '\x2', '\x2', '\xC4', '&', '\x3', 
		'\x2', '\x2', '\x2', '\xC5', '\xC6', '\a', '>', '\x2', '\x2', '\xC6', 
		'(', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC8', '\a', '>', '\x2', '\x2', 
		'\xC8', '\xC9', '\a', '?', '\x2', '\x2', '\xC9', '*', '\x3', '\x2', '\x2', 
		'\x2', '\xCA', '\xCB', '\a', '@', '\x2', '\x2', '\xCB', ',', '\x3', '\x2', 
		'\x2', '\x2', '\xCC', '\xCD', '\a', '@', '\x2', '\x2', '\xCD', '\xCE', 
		'\a', '?', '\x2', '\x2', '\xCE', '.', '\x3', '\x2', '\x2', '\x2', '\xCF', 
		'\xD0', '\a', '?', '\x2', '\x2', '\xD0', '\xD1', '\a', '?', '\x2', '\x2', 
		'\xD1', '\x30', '\x3', '\x2', '\x2', '\x2', '\xD2', '\xD3', '\a', 'y', 
		'\x2', '\x2', '\xD3', '\xD4', '\a', 'j', '\x2', '\x2', '\xD4', '\xD5', 
		'\a', 'g', '\x2', '\x2', '\xD5', '\xD6', '\a', 't', '\x2', '\x2', '\xD6', 
		'\xD7', '\a', 'g', '\x2', '\x2', '\xD7', '\x32', '\x3', '\x2', '\x2', 
		'\x2', '\xD8', '\xD9', '\a', '#', '\x2', '\x2', '\xD9', '\xDA', '\a', 
		'?', '\x2', '\x2', '\xDA', '\x34', '\x3', '\x2', '\x2', '\x2', '\xDB', 
		'\xDC', '\a', '#', '\x2', '\x2', '\xDC', '\x36', '\x3', '\x2', '\x2', 
		'\x2', '\xDD', '\xDE', '\a', '(', '\x2', '\x2', '\xDE', '\xDF', '\a', 
		'(', '\x2', '\x2', '\xDF', '\x38', '\x3', '\x2', '\x2', '\x2', '\xE0', 
		'\xE1', '\a', '~', '\x2', '\x2', '\xE1', '\xE2', '\a', '~', '\x2', '\x2', 
		'\xE2', ':', '\x3', '\x2', '\x2', '\x2', '\xE3', '\xE7', '\a', ')', '\x2', 
		'\x2', '\xE4', '\xE6', '\x5', '=', '\x1F', '\x2', '\xE5', '\xE4', '\x3', 
		'\x2', '\x2', '\x2', '\xE6', '\xE9', '\x3', '\x2', '\x2', '\x2', '\xE7', 
		'\xE5', '\x3', '\x2', '\x2', '\x2', '\xE7', '\xE8', '\x3', '\x2', '\x2', 
		'\x2', '\xE8', '\xEA', '\x3', '\x2', '\x2', '\x2', '\xE9', '\xE7', '\x3', 
		'\x2', '\x2', '\x2', '\xEA', '\xEB', '\a', ')', '\x2', '\x2', '\xEB', 
		'<', '\x3', '\x2', '\x2', '\x2', '\xEC', '\xF1', '\n', '\x4', '\x2', '\x2', 
		'\xED', '\xEE', '\a', '^', '\x2', '\x2', '\xEE', '\xF1', '\x5', '?', ' ', 
		'\x2', '\xEF', '\xF1', '\x5', 'O', '(', '\x2', '\xF0', '\xEC', '\x3', 
		'\x2', '\x2', '\x2', '\xF0', '\xED', '\x3', '\x2', '\x2', '\x2', '\xF0', 
		'\xEF', '\x3', '\x2', '\x2', '\x2', '\xF1', '>', '\x3', '\x2', '\x2', 
		'\x2', '\xF2', '\xF8', '\x5', '\x41', '!', '\x2', '\xF3', '\xF8', '\a', 
		'\x32', '\x2', '\x2', '\xF4', '\xF8', '\x5', '\x43', '\"', '\x2', '\xF5', 
		'\xF8', '\x5', '\x45', '#', '\x2', '\xF6', '\xF8', '\x5', 'G', '$', '\x2', 
		'\xF7', '\xF2', '\x3', '\x2', '\x2', '\x2', '\xF7', '\xF3', '\x3', '\x2', 
		'\x2', '\x2', '\xF7', '\xF4', '\x3', '\x2', '\x2', '\x2', '\xF7', '\xF5', 
		'\x3', '\x2', '\x2', '\x2', '\xF7', '\xF6', '\x3', '\x2', '\x2', '\x2', 
		'\xF8', '@', '\x3', '\x2', '\x2', '\x2', '\xF9', '\xFC', '\x5', 'I', '%', 
		'\x2', '\xFA', '\xFC', '\x5', 'K', '&', '\x2', '\xFB', '\xF9', '\x3', 
		'\x2', '\x2', '\x2', '\xFB', '\xFA', '\x3', '\x2', '\x2', '\x2', '\xFC', 
		'\x42', '\x3', '\x2', '\x2', '\x2', '\xFD', '\xFE', '\a', 'z', '\x2', 
		'\x2', '\xFE', '\xFF', '\x5', 'Q', ')', '\x2', '\xFF', '\x100', '\x5', 
		'Q', ')', '\x2', '\x100', '\x44', '\x3', '\x2', '\x2', '\x2', '\x101', 
		'\x102', '\a', 'w', '\x2', '\x2', '\x102', '\x103', '\x5', 'Q', ')', '\x2', 
		'\x103', '\x104', '\x5', 'Q', ')', '\x2', '\x104', '\x105', '\x5', 'Q', 
		')', '\x2', '\x105', '\x106', '\x5', 'Q', ')', '\x2', '\x106', '\x46', 
		'\x3', '\x2', '\x2', '\x2', '\x107', '\x108', '\a', 'w', '\x2', '\x2', 
		'\x108', '\x10A', '\a', '}', '\x2', '\x2', '\x109', '\x10B', '\x5', 'Q', 
		')', '\x2', '\x10A', '\x109', '\x3', '\x2', '\x2', '\x2', '\x10B', '\x10C', 
		'\x3', '\x2', '\x2', '\x2', '\x10C', '\x10A', '\x3', '\x2', '\x2', '\x2', 
		'\x10C', '\x10D', '\x3', '\x2', '\x2', '\x2', '\x10D', '\x10E', '\x3', 
		'\x2', '\x2', '\x2', '\x10E', '\x10F', '\a', '\x7F', '\x2', '\x2', '\x10F', 
		'H', '\x3', '\x2', '\x2', '\x2', '\x110', '\x111', '\t', '\x5', '\x2', 
		'\x2', '\x111', 'J', '\x3', '\x2', '\x2', '\x2', '\x112', '\x113', '\n', 
		'\x6', '\x2', '\x2', '\x113', 'L', '\x3', '\x2', '\x2', '\x2', '\x114', 
		'\x117', '\x5', 'I', '%', '\x2', '\x115', '\x117', '\t', '\a', '\x2', 
		'\x2', '\x116', '\x114', '\x3', '\x2', '\x2', '\x2', '\x116', '\x115', 
		'\x3', '\x2', '\x2', '\x2', '\x117', 'N', '\x3', '\x2', '\x2', '\x2', 
		'\x118', '\x119', '\a', '^', '\x2', '\x2', '\x119', '\x11A', '\t', '\b', 
		'\x2', '\x2', '\x11A', 'P', '\x3', '\x2', '\x2', '\x2', '\x11B', '\x11C', 
		'\t', '\t', '\x2', '\x2', '\x11C', 'R', '\x3', '\x2', '\x2', '\x2', '\x11D', 
		'\x11F', '\t', '\n', '\x2', '\x2', '\x11E', '\x11D', '\x3', '\x2', '\x2', 
		'\x2', '\x11E', '\x11F', '\x3', '\x2', '\x2', '\x2', '\x11F', '\x121', 
		'\x3', '\x2', '\x2', '\x2', '\x120', '\x122', '\t', '\v', '\x2', '\x2', 
		'\x121', '\x120', '\x3', '\x2', '\x2', '\x2', '\x122', '\x123', '\x3', 
		'\x2', '\x2', '\x2', '\x123', '\x121', '\x3', '\x2', '\x2', '\x2', '\x123', 
		'\x124', '\x3', '\x2', '\x2', '\x2', '\x124', '\x12B', '\x3', '\x2', '\x2', 
		'\x2', '\x125', '\x127', '\t', '\f', '\x2', '\x2', '\x126', '\x128', '\t', 
		'\v', '\x2', '\x2', '\x127', '\x126', '\x3', '\x2', '\x2', '\x2', '\x128', 
		'\x129', '\x3', '\x2', '\x2', '\x2', '\x129', '\x127', '\x3', '\x2', '\x2', 
		'\x2', '\x129', '\x12A', '\x3', '\x2', '\x2', '\x2', '\x12A', '\x12C', 
		'\x3', '\x2', '\x2', '\x2', '\x12B', '\x125', '\x3', '\x2', '\x2', '\x2', 
		'\x12B', '\x12C', '\x3', '\x2', '\x2', '\x2', '\x12C', 'T', '\x3', '\x2', 
		'\x2', '\x2', '\x12D', '\x12E', '\a', 'v', '\x2', '\x2', '\x12E', '\x12F', 
		'\a', 't', '\x2', '\x2', '\x12F', '\x130', '\a', 'w', '\x2', '\x2', '\x130', 
		'\x137', '\a', 'g', '\x2', '\x2', '\x131', '\x132', '\a', 'h', '\x2', 
		'\x2', '\x132', '\x133', '\a', '\x63', '\x2', '\x2', '\x133', '\x134', 
		'\a', 'n', '\x2', '\x2', '\x134', '\x135', '\a', 'u', '\x2', '\x2', '\x135', 
		'\x137', '\a', 'g', '\x2', '\x2', '\x136', '\x12D', '\x3', '\x2', '\x2', 
		'\x2', '\x136', '\x131', '\x3', '\x2', '\x2', '\x2', '\x137', 'V', '\x3', 
		'\x2', '\x2', '\x2', '\x138', '\x139', '\a', 'p', '\x2', '\x2', '\x139', 
		'\x13A', '\a', 'w', '\x2', '\x2', '\x13A', '\x13B', '\a', 'n', '\x2', 
		'\x2', '\x13B', '\x13C', '\a', 'n', '\x2', '\x2', '\x13C', 'X', '\x3', 
		'\x2', '\x2', '\x2', '\x13D', '\x142', '\x5', ';', '\x1E', '\x2', '\x13E', 
		'\x142', '\x5', 'S', '*', '\x2', '\x13F', '\x142', '\x5', 'U', '+', '\x2', 
		'\x140', '\x142', '\x5', 'W', ',', '\x2', '\x141', '\x13D', '\x3', '\x2', 
		'\x2', '\x2', '\x141', '\x13E', '\x3', '\x2', '\x2', '\x2', '\x141', '\x13F', 
		'\x3', '\x2', '\x2', '\x2', '\x141', '\x140', '\x3', '\x2', '\x2', '\x2', 
		'\x142', 'Z', '\x3', '\x2', '\x2', '\x2', '\x143', '\x144', '\a', 'u', 
		'\x2', '\x2', '\x144', '\x145', '\a', 'v', '\x2', '\x2', '\x145', '\x146', 
		'\a', 't', '\x2', '\x2', '\x146', '\x147', '\a', 'w', '\x2', '\x2', '\x147', 
		'\x148', '\a', '\x65', '\x2', '\x2', '\x148', '\x149', '\a', 'v', '\x2', 
		'\x2', '\x149', '\\', '\x3', '\x2', '\x2', '\x2', '\x14A', '\x14B', '\a', 
		'x', '\x2', '\x2', '\x14B', '\x14C', '\a', 'q', '\x2', '\x2', '\x14C', 
		'\x14D', '\a', 'k', '\x2', '\x2', '\x14D', '\x14E', '\a', '\x66', '\x2', 
		'\x2', '\x14E', '^', '\x3', '\x2', '\x2', '\x2', '\x14F', '\x150', '\a', 
		'\x63', '\x2', '\x2', '\x150', '\x151', '\a', 'p', '\x2', '\x2', '\x151', 
		'\x152', '\a', '{', '\x2', '\x2', '\x152', '`', '\x3', '\x2', '\x2', '\x2', 
		'\x153', '\x154', '\a', 't', '\x2', '\x2', '\x154', '\x155', '\a', 'g', 
		'\x2', '\x2', '\x155', '\x156', '\a', 'v', '\x2', '\x2', '\x156', '\x157', 
		'\a', 'w', '\x2', '\x2', '\x157', '\x158', '\a', 't', '\x2', '\x2', '\x158', 
		'\x159', '\a', 'p', '\x2', '\x2', '\x159', '\x62', '\x3', '\x2', '\x2', 
		'\x2', '\x15A', '\x15B', '\a', '\x64', '\x2', '\x2', '\x15B', '\x15C', 
		'\a', 't', '\x2', '\x2', '\x15C', '\x15D', '\a', 'g', '\x2', '\x2', '\x15D', 
		'\x15E', '\a', '\x63', '\x2', '\x2', '\x15E', '\x15F', '\a', 'm', '\x2', 
		'\x2', '\x15F', '\x64', '\x3', '\x2', '\x2', '\x2', '\x160', '\x161', 
		'\a', '\x65', '\x2', '\x2', '\x161', '\x162', '\a', 'q', '\x2', '\x2', 
		'\x162', '\x163', '\a', 'p', '\x2', '\x2', '\x163', '\x164', '\a', 'v', 
		'\x2', '\x2', '\x164', '\x165', '\a', 'k', '\x2', '\x2', '\x165', '\x166', 
		'\a', 'p', '\x2', '\x2', '\x166', '\x167', '\a', 'w', '\x2', '\x2', '\x167', 
		'\x168', '\a', 'g', '\x2', '\x2', '\x168', '\x66', '\x3', '\x2', '\x2', 
		'\x2', '\x169', '\x16A', '\a', 'k', '\x2', '\x2', '\x16A', '\x16B', '\a', 
		'h', '\x2', '\x2', '\x16B', 'h', '\x3', '\x2', '\x2', '\x2', '\x16C', 
		'\x16D', '\a', '\x63', '\x2', '\x2', '\x16D', '\x16E', '\a', '\x64', '\x2', 
		'\x2', '\x16E', '\x16F', '\a', 'u', '\x2', '\x2', '\x16F', '\x170', '\a', 
		'v', '\x2', '\x2', '\x170', '\x171', '\a', 't', '\x2', '\x2', '\x171', 
		'\x172', '\a', '\x63', '\x2', '\x2', '\x172', '\x173', '\a', '\x65', '\x2', 
		'\x2', '\x173', '\x174', '\a', 'v', '\x2', '\x2', '\x174', '\x175', '\a', 
		'k', '\x2', '\x2', '\x175', '\x176', '\a', 'q', '\x2', '\x2', '\x176', 
		'\x177', '\a', 'p', '\x2', '\x2', '\x177', 'j', '\x3', '\x2', '\x2', '\x2', 
		'\x178', '\x179', '\a', 'o', '\x2', '\x2', '\x179', '\x17A', '\a', '\x63', 
		'\x2', '\x2', '\x17A', '\x17B', '\a', 'k', '\x2', '\x2', '\x17B', '\x17C', 
		'\a', 'p', '\x2', '\x2', '\x17C', 'l', '\x3', '\x2', '\x2', '\x2', '\x17D', 
		'\x17E', '\a', 'p', '\x2', '\x2', '\x17E', '\x17F', '\a', 'g', '\x2', 
		'\x2', '\x17F', '\x180', '\a', 'y', '\x2', '\x2', '\x180', 'n', '\x3', 
		'\x2', '\x2', '\x2', '\x181', '\x182', '\a', 'k', '\x2', '\x2', '\x182', 
		'\x183', '\a', 'u', '\x2', '\x2', '\x183', 'p', '\x3', '\x2', '\x2', '\x2', 
		'\x184', '\x185', '\a', '\x63', '\x2', '\x2', '\x185', '\x186', '\a', 
		'p', '\x2', '\x2', '\x186', '\x187', '\a', '\x66', '\x2', '\x2', '\x187', 
		'r', '\x3', '\x2', '\x2', '\x2', '\x188', '\x189', '\a', '\x66', '\x2', 
		'\x2', '\x189', '\x18A', '\a', 'g', '\x2', '\x2', '\x18A', '\x18B', '\a', 
		'h', '\x2', '\x2', '\x18B', '\x18C', '\a', '\x63', '\x2', '\x2', '\x18C', 
		'\x18D', '\a', 'w', '\x2', '\x2', '\x18D', '\x18E', '\a', 'n', '\x2', 
		'\x2', '\x18E', '\x18F', '\a', 'v', '\x2', '\x2', '\x18F', 't', '\x3', 
		'\x2', '\x2', '\x2', '\x190', '\x191', '\a', 'v', '\x2', '\x2', '\x191', 
		'\x192', '\a', '{', '\x2', '\x2', '\x192', '\x193', '\a', 'r', '\x2', 
		'\x2', '\x193', '\x194', '\a', 'g', '\x2', '\x2', '\x194', '\x195', '\a', 
		'q', '\x2', '\x2', '\x195', '\x196', '\a', 'h', '\x2', '\x2', '\x196', 
		'v', '\x3', '\x2', '\x2', '\x2', '\x197', '\x198', '\a', 'g', '\x2', '\x2', 
		'\x198', '\x199', '\a', 'n', '\x2', '\x2', '\x199', '\x19A', '\a', 'u', 
		'\x2', '\x2', '\x19A', '\x19B', '\a', 'g', '\x2', '\x2', '\x19B', 'x', 
		'\x3', '\x2', '\x2', '\x2', '\x19C', '\x19D', '\a', 'h', '\x2', '\x2', 
		'\x19D', '\x19E', '\a', 'q', '\x2', '\x2', '\x19E', '\x19F', '\a', 't', 
		'\x2', '\x2', '\x19F', 'z', '\x3', '\x2', '\x2', '\x2', '\x1A0', '\x1A1', 
		'\a', 'g', '\x2', '\x2', '\x1A1', '\x1A2', '\a', 'z', '\x2', '\x2', '\x1A2', 
		'\x1A3', '\a', 'v', '\x2', '\x2', '\x1A3', '\x1A4', '\a', 'g', '\x2', 
		'\x2', '\x1A4', '\x1A5', '\a', 't', '\x2', '\x2', '\x1A5', '\x1A6', '\a', 
		'p', '\x2', '\x2', '\x1A6', '|', '\x3', '\x2', '\x2', '\x2', '\x1A7', 
		'\x1A8', '\a', 'r', '\x2', '\x2', '\x1A8', '\x1A9', '\a', 'q', '\x2', 
		'\x2', '\x1A9', '\x1AA', '\a', 'n', '\x2', '\x2', '\x1AA', '\x1AB', '\a', 
		'{', '\x2', '\x2', '\x1AB', '~', '\x3', '\x2', '\x2', '\x2', '\x1AC', 
		'\x1AD', '\a', '/', '\x2', '\x2', '\x1AD', '\x1AE', '\a', '@', '\x2', 
		'\x2', '\x1AE', '\x80', '\x3', '\x2', '\x2', '\x2', '\x1AF', '\x1B0', 
		'\a', 'p', '\x2', '\x2', '\x1B0', '\x1B1', '\a', 'w', '\x2', '\x2', '\x1B1', 
		'\x1BD', '\a', 'o', '\x2', '\x2', '\x1B2', '\x1B3', '\a', 'u', '\x2', 
		'\x2', '\x1B3', '\x1B4', '\a', 'v', '\x2', '\x2', '\x1B4', '\x1B5', '\a', 
		't', '\x2', '\x2', '\x1B5', '\x1B6', '\a', 'k', '\x2', '\x2', '\x1B6', 
		'\x1B7', '\a', 'p', '\x2', '\x2', '\x1B7', '\x1BD', '\a', 'i', '\x2', 
		'\x2', '\x1B8', '\x1B9', '\a', '\x64', '\x2', '\x2', '\x1B9', '\x1BA', 
		'\a', 'q', '\x2', '\x2', '\x1BA', '\x1BB', '\a', 'q', '\x2', '\x2', '\x1BB', 
		'\x1BD', '\a', 'n', '\x2', '\x2', '\x1BC', '\x1AF', '\x3', '\x2', '\x2', 
		'\x2', '\x1BC', '\x1B2', '\x3', '\x2', '\x2', '\x2', '\x1BC', '\x1B8', 
		'\x3', '\x2', '\x2', '\x2', '\x1BD', '\x82', '\x3', '\x2', '\x2', '\x2', 
		'\x1BE', '\x1BF', '\x4', '\x32', ';', '\x2', '\x1BF', '\x84', '\x3', '\x2', 
		'\x2', '\x2', '\x1C0', '\x1C1', '\t', '\r', '\x2', '\x2', '\x1C1', '\x86', 
		'\x3', '\x2', '\x2', '\x2', '\x1C2', '\x1C7', '\x5', '\x13', '\n', '\x2', 
		'\x1C3', '\x1C7', '\x5', '\x17', '\f', '\x2', '\x1C4', '\x1C7', '\x5', 
		'\x19', '\r', '\x2', '\x1C5', '\x1C7', '\x5', '\x15', '\v', '\x2', '\x1C6', 
		'\x1C2', '\x3', '\x2', '\x2', '\x2', '\x1C6', '\x1C3', '\x3', '\x2', '\x2', 
		'\x2', '\x1C6', '\x1C4', '\x3', '\x2', '\x2', '\x2', '\x1C6', '\x1C5', 
		'\x3', '\x2', '\x2', '\x2', '\x1C7', '\x88', '\x3', '\x2', '\x2', '\x2', 
		'\x1C8', '\x1D1', '\x5', '\'', '\x14', '\x2', '\x1C9', '\x1D1', '\x5', 
		')', '\x15', '\x2', '\x1CA', '\x1D1', '\x5', '+', '\x16', '\x2', '\x1CB', 
		'\x1D1', '\x5', '-', '\x17', '\x2', '\x1CC', '\x1D1', '\x5', '/', '\x18', 
		'\x2', '\x1CD', '\x1D1', '\x5', '\x33', '\x1A', '\x2', '\x1CE', '\x1D1', 
		'\x5', '\x37', '\x1C', '\x2', '\x1CF', '\x1D1', '\x5', '\x39', '\x1D', 
		'\x2', '\x1D0', '\x1C8', '\x3', '\x2', '\x2', '\x2', '\x1D0', '\x1C9', 
		'\x3', '\x2', '\x2', '\x2', '\x1D0', '\x1CA', '\x3', '\x2', '\x2', '\x2', 
		'\x1D0', '\x1CB', '\x3', '\x2', '\x2', '\x2', '\x1D0', '\x1CC', '\x3', 
		'\x2', '\x2', '\x2', '\x1D0', '\x1CD', '\x3', '\x2', '\x2', '\x2', '\x1D0', 
		'\x1CE', '\x3', '\x2', '\x2', '\x2', '\x1D0', '\x1CF', '\x3', '\x2', '\x2', 
		'\x2', '\x1D1', '\x8A', '\x3', '\x2', '\x2', '\x2', '\x1D2', '\x1D7', 
		'\x5', '\x85', '\x43', '\x2', '\x1D3', '\x1D6', '\x5', '\x85', '\x43', 
		'\x2', '\x1D4', '\x1D6', '\x5', '\x83', '\x42', '\x2', '\x1D5', '\x1D3', 
		'\x3', '\x2', '\x2', '\x2', '\x1D5', '\x1D4', '\x3', '\x2', '\x2', '\x2', 
		'\x1D6', '\x1D9', '\x3', '\x2', '\x2', '\x2', '\x1D7', '\x1D5', '\x3', 
		'\x2', '\x2', '\x2', '\x1D7', '\x1D8', '\x3', '\x2', '\x2', '\x2', '\x1D8', 
		'\x8C', '\x3', '\x2', '\x2', '\x2', '\x1D9', '\x1D7', '\x3', '\x2', '\x2', 
		'\x2', '\x17', '\x2', '\x96', '\x9E', '\xA3', '\xE7', '\xF0', '\xF7', 
		'\xFB', '\x10C', '\x116', '\x11E', '\x123', '\x129', '\x12B', '\x136', 
		'\x141', '\x1BC', '\x1C6', '\x1D0', '\x1D5', '\x1D7', '\x3', '\x2', '\x3', 
		'\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
