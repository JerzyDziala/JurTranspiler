// Generated from /Users/strator/Documents/GitHub/JurTranspiler/JurTranspiler/src/ANTLR/Jur.g4 by ANTLR 4.7.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class JurParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, LINE_COMMENT=3, WHITESPACE=4, NEWLINE=5, COMA=6, DOT=7, 
		SEMICOLON=8, DOLLAR=9, ASSIGN=10, ADD=11, SUBTRACT=12, TIMES=13, DIVIDE=14, 
		LEFT_CURLY=15, RIGHT_CURLY=16, LEFT_PARENT=17, RIGHT_PARENT=18, LEFT_SQUARE_PARENT=19, 
		RIGHT_SQUARE_PARENT=20, LESS=21, LEQUAL=22, GREATER=23, GREQUAL=24, EQUAL=25, 
		IS=26, WHERE=27, NOT_EQUAL=28, AND=29, OR=30, STRING_VALUE=31, NUMBER_VALUE=32, 
		BOOL_VALUE=33, NULL_VALUE=34, VALUE=35, STRUCT=36, VOID=37, RETURN=38, 
		BREAK=39, CONTINUE=40, EXIT=41, IF=42, WHILE=43, ABSTRACTION=44, MAIN=45, 
		NEW=46, IN=47, ELSE=48, FOR=49, EXTERN=50, LET=51, POLY=52, ARROW=53, 
		PRIMITIVE=54, ARITHMETIC=55, LOGIC=56, ID=57;
	public static final int
		RULE_program = 0, RULE_main = 1, RULE_abstraction = 2, RULE_structDeclaration = 3, 
		RULE_inlinedType = 4, RULE_functionDeclaration = 5, RULE_constraints = 6, 
		RULE_constrain = 7, RULE_uninitializedVarDeclaration = 8, RULE_initializedVariableDeclaration = 9, 
		RULE_inferedVariableDeclaration = 10, RULE_type = 11, RULE_statement = 12, 
		RULE_block = 13, RULE_expression = 14;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "main", "abstraction", "structDeclaration", "inlinedType", 
			"functionDeclaration", "constraints", "constrain", "uninitializedVarDeclaration", 
			"initializedVariableDeclaration", "inferedVariableDeclaration", "type", 
			"statement", "block", "expression"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "':='", "':'", null, null, null, "','", "'.'", "';'", "'$'", "'='", 
			"'+'", "'-'", "'*'", "'/'", "'{'", "'}'", "'('", "')'", "'['", "']'", 
			"'<'", "'<='", "'>'", "'>='", "'=='", "'is'", "'where'", "'!='", null, 
			null, null, null, null, "'null'", null, "'struct'", "'void'", "'return'", 
			"'break'", "'continue'", "'exit'", "'if'", "'while'", "'abstraction'", 
			"'main'", "'new'", "'in'", "'else'", "'for'", "'extern'", "'let'", "'poly'", 
			"'->'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, "LINE_COMMENT", "WHITESPACE", "NEWLINE", "COMA", "DOT", 
			"SEMICOLON", "DOLLAR", "ASSIGN", "ADD", "SUBTRACT", "TIMES", "DIVIDE", 
			"LEFT_CURLY", "RIGHT_CURLY", "LEFT_PARENT", "RIGHT_PARENT", "LEFT_SQUARE_PARENT", 
			"RIGHT_SQUARE_PARENT", "LESS", "LEQUAL", "GREATER", "GREQUAL", "EQUAL", 
			"IS", "WHERE", "NOT_EQUAL", "AND", "OR", "STRING_VALUE", "NUMBER_VALUE", 
			"BOOL_VALUE", "NULL_VALUE", "VALUE", "STRUCT", "VOID", "RETURN", "BREAK", 
			"CONTINUE", "EXIT", "IF", "WHILE", "ABSTRACTION", "MAIN", "NEW", "IN", 
			"ELSE", "FOR", "EXTERN", "LET", "POLY", "ARROW", "PRIMITIVE", "ARITHMETIC", 
			"LOGIC", "ID"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Jur.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public JurParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(JurParser.EOF, 0); }
		public List<MainContext> main() {
			return getRuleContexts(MainContext.class);
		}
		public MainContext main(int i) {
			return getRuleContext(MainContext.class,i);
		}
		public List<AbstractionContext> abstraction() {
			return getRuleContexts(AbstractionContext.class);
		}
		public AbstractionContext abstraction(int i) {
			return getRuleContext(AbstractionContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterProgram(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitProgram(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitProgram(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(34);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ABSTRACTION || _la==MAIN) {
				{
				setState(32);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case MAIN:
					{
					setState(30);
					main();
					}
					break;
				case ABSTRACTION:
					{
					setState(31);
					abstraction();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(36);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(37);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MainContext extends ParserRuleContext {
		public TerminalNode MAIN() { return getToken(JurParser.MAIN, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public MainContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_main; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterMain(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitMain(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitMain(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MainContext main() throws RecognitionException {
		MainContext _localctx = new MainContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_main);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(39);
			match(MAIN);
			setState(40);
			statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AbstractionContext extends ParserRuleContext {
		public TerminalNode ABSTRACTION() { return getToken(JurParser.ABSTRACTION, 0); }
		public TerminalNode NUMBER_VALUE() { return getToken(JurParser.NUMBER_VALUE, 0); }
		public TerminalNode LEFT_CURLY() { return getToken(JurParser.LEFT_CURLY, 0); }
		public TerminalNode RIGHT_CURLY() { return getToken(JurParser.RIGHT_CURLY, 0); }
		public List<FunctionDeclarationContext> functionDeclaration() {
			return getRuleContexts(FunctionDeclarationContext.class);
		}
		public FunctionDeclarationContext functionDeclaration(int i) {
			return getRuleContext(FunctionDeclarationContext.class,i);
		}
		public List<StructDeclarationContext> structDeclaration() {
			return getRuleContexts(StructDeclarationContext.class);
		}
		public StructDeclarationContext structDeclaration(int i) {
			return getRuleContext(StructDeclarationContext.class,i);
		}
		public AbstractionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_abstraction; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterAbstraction(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitAbstraction(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitAbstraction(this);
			else return visitor.visitChildren(this);
		}
	}

	public final AbstractionContext abstraction() throws RecognitionException {
		AbstractionContext _localctx = new AbstractionContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_abstraction);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(42);
			match(ABSTRACTION);
			setState(43);
			match(NUMBER_VALUE);
			setState(44);
			match(LEFT_CURLY);
			setState(49);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << STRUCT) | (1L << VOID) | (1L << EXTERN) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
				{
				setState(47);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
				case 1:
					{
					setState(45);
					functionDeclaration();
					}
					break;
				case 2:
					{
					setState(46);
					structDeclaration();
					}
					break;
				}
				}
				setState(51);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(52);
			match(RIGHT_CURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StructDeclarationContext extends ParserRuleContext {
		public TerminalNode STRUCT() { return getToken(JurParser.STRUCT, 0); }
		public List<TerminalNode> ID() { return getTokens(JurParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(JurParser.ID, i);
		}
		public TerminalNode LEFT_CURLY() { return getToken(JurParser.LEFT_CURLY, 0); }
		public TerminalNode RIGHT_CURLY() { return getToken(JurParser.RIGHT_CURLY, 0); }
		public TerminalNode EXTERN() { return getToken(JurParser.EXTERN, 0); }
		public TerminalNode LESS() { return getToken(JurParser.LESS, 0); }
		public TerminalNode GREATER() { return getToken(JurParser.GREATER, 0); }
		public List<InlinedTypeContext> inlinedType() {
			return getRuleContexts(InlinedTypeContext.class);
		}
		public InlinedTypeContext inlinedType(int i) {
			return getRuleContext(InlinedTypeContext.class,i);
		}
		public List<UninitializedVarDeclarationContext> uninitializedVarDeclaration() {
			return getRuleContexts(UninitializedVarDeclarationContext.class);
		}
		public UninitializedVarDeclarationContext uninitializedVarDeclaration(int i) {
			return getRuleContext(UninitializedVarDeclarationContext.class,i);
		}
		public List<TerminalNode> SEMICOLON() { return getTokens(JurParser.SEMICOLON); }
		public TerminalNode SEMICOLON(int i) {
			return getToken(JurParser.SEMICOLON, i);
		}
		public List<TerminalNode> COMA() { return getTokens(JurParser.COMA); }
		public TerminalNode COMA(int i) {
			return getToken(JurParser.COMA, i);
		}
		public StructDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_structDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterStructDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitStructDeclaration(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitStructDeclaration(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StructDeclarationContext structDeclaration() throws RecognitionException {
		StructDeclarationContext _localctx = new StructDeclarationContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_structDeclaration);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(55);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==EXTERN) {
				{
				setState(54);
				match(EXTERN);
				}
			}

			setState(57);
			match(STRUCT);
			setState(58);
			match(ID);
			setState(69);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LESS) {
				{
				setState(59);
				match(LESS);
				setState(60);
				match(ID);
				setState(65);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMA) {
					{
					{
					setState(61);
					match(COMA);
					setState(62);
					match(ID);
					}
					}
					setState(67);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(68);
				match(GREATER);
				}
			}

			setState(71);
			match(LEFT_CURLY);
			setState(78);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << IS) | (1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
				{
				setState(76);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case VOID:
				case PRIMITIVE:
				case ID:
					{
					{
					setState(72);
					uninitializedVarDeclaration();
					setState(73);
					match(SEMICOLON);
					}
					}
					break;
				case IS:
					{
					setState(75);
					inlinedType();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(80);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(81);
			match(RIGHT_CURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InlinedTypeContext extends ParserRuleContext {
		public TerminalNode IS() { return getToken(JurParser.IS, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public InlinedTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inlinedType; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterInlinedType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitInlinedType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitInlinedType(this);
			else return visitor.visitChildren(this);
		}
	}

	public final InlinedTypeContext inlinedType() throws RecognitionException {
		InlinedTypeContext _localctx = new InlinedTypeContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_inlinedType);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(83);
			match(IS);
			setState(84);
			type(0);
			setState(85);
			match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionDeclarationContext extends ParserRuleContext {
		public List<TerminalNode> ID() { return getTokens(JurParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(JurParser.ID, i);
		}
		public TerminalNode LEFT_PARENT() { return getToken(JurParser.LEFT_PARENT, 0); }
		public TerminalNode RIGHT_PARENT() { return getToken(JurParser.RIGHT_PARENT, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode VOID() { return getToken(JurParser.VOID, 0); }
		public TerminalNode LESS() { return getToken(JurParser.LESS, 0); }
		public TerminalNode GREATER() { return getToken(JurParser.GREATER, 0); }
		public List<UninitializedVarDeclarationContext> uninitializedVarDeclaration() {
			return getRuleContexts(UninitializedVarDeclarationContext.class);
		}
		public UninitializedVarDeclarationContext uninitializedVarDeclaration(int i) {
			return getRuleContext(UninitializedVarDeclarationContext.class,i);
		}
		public ConstraintsContext constraints() {
			return getRuleContext(ConstraintsContext.class,0);
		}
		public List<TerminalNode> COMA() { return getTokens(JurParser.COMA); }
		public TerminalNode COMA(int i) {
			return getToken(JurParser.COMA, i);
		}
		public TerminalNode EXTERN() { return getToken(JurParser.EXTERN, 0); }
		public FunctionDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterFunctionDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitFunctionDeclaration(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitFunctionDeclaration(this);
			else return visitor.visitChildren(this);
		}
	}

	public final FunctionDeclarationContext functionDeclaration() throws RecognitionException {
		FunctionDeclarationContext _localctx = new FunctionDeclarationContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_functionDeclaration);
		int _la;
		try {
			setState(153);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case VOID:
			case PRIMITIVE:
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				{
				setState(89);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,9,_ctx) ) {
				case 1:
					{
					setState(87);
					type(0);
					}
					break;
				case 2:
					{
					setState(88);
					match(VOID);
					}
					break;
				}
				setState(91);
				match(ID);
				setState(102);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LESS) {
					{
					setState(92);
					match(LESS);
					setState(93);
					match(ID);
					setState(98);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(94);
						match(COMA);
						setState(95);
						match(ID);
						}
						}
						setState(100);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(101);
					match(GREATER);
					}
				}

				setState(104);
				match(LEFT_PARENT);
				setState(113);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(105);
					uninitializedVarDeclaration();
					setState(110);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(106);
						match(COMA);
						setState(107);
						uninitializedVarDeclaration();
						}
						}
						setState(112);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(115);
				match(RIGHT_PARENT);
				setState(117);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==WHERE) {
					{
					setState(116);
					constraints();
					}
				}

				setState(119);
				statement();
				}
				}
				break;
			case EXTERN:
				enterOuterAlt(_localctx, 2);
				{
				{
				setState(120);
				match(EXTERN);
				setState(123);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,15,_ctx) ) {
				case 1:
					{
					setState(121);
					type(0);
					}
					break;
				case 2:
					{
					setState(122);
					match(VOID);
					}
					break;
				}
				setState(125);
				match(ID);
				setState(136);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LESS) {
					{
					setState(126);
					match(LESS);
					setState(127);
					match(ID);
					setState(132);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(128);
						match(COMA);
						setState(129);
						match(ID);
						}
						}
						setState(134);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(135);
					match(GREATER);
					}
				}

				setState(138);
				match(LEFT_PARENT);
				setState(147);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(139);
					uninitializedVarDeclaration();
					setState(144);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(140);
						match(COMA);
						setState(141);
						uninitializedVarDeclaration();
						}
						}
						setState(146);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(149);
				match(RIGHT_PARENT);
				setState(151);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==WHERE) {
					{
					setState(150);
					constraints();
					}
				}

				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstraintsContext extends ParserRuleContext {
		public TerminalNode WHERE() { return getToken(JurParser.WHERE, 0); }
		public List<ConstrainContext> constrain() {
			return getRuleContexts(ConstrainContext.class);
		}
		public ConstrainContext constrain(int i) {
			return getRuleContext(ConstrainContext.class,i);
		}
		public List<TerminalNode> AND() { return getTokens(JurParser.AND); }
		public TerminalNode AND(int i) {
			return getToken(JurParser.AND, i);
		}
		public ConstraintsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constraints; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterConstraints(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitConstraints(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitConstraints(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ConstraintsContext constraints() throws RecognitionException {
		ConstraintsContext _localctx = new ConstraintsContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_constraints);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(155);
			match(WHERE);
			setState(156);
			constrain();
			setState(161);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==AND) {
				{
				{
				setState(157);
				match(AND);
				setState(158);
				constrain();
				}
				}
				setState(163);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConstrainContext extends ParserRuleContext {
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public TerminalNode IS() { return getToken(JurParser.IS, 0); }
		public ConstrainContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constrain; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterConstrain(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitConstrain(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitConstrain(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ConstrainContext constrain() throws RecognitionException {
		ConstrainContext _localctx = new ConstrainContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_constrain);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(164);
			type(0);
			setState(165);
			match(IS);
			setState(166);
			type(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class UninitializedVarDeclarationContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public UninitializedVarDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_uninitializedVarDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterUninitializedVarDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitUninitializedVarDeclaration(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitUninitializedVarDeclaration(this);
			else return visitor.visitChildren(this);
		}
	}

	public final UninitializedVarDeclarationContext uninitializedVarDeclaration() throws RecognitionException {
		UninitializedVarDeclarationContext _localctx = new UninitializedVarDeclarationContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_uninitializedVarDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(168);
			type(0);
			setState(169);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InitializedVariableDeclarationContext extends ParserRuleContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public TerminalNode ASSIGN() { return getToken(JurParser.ASSIGN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public InitializedVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initializedVariableDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterInitializedVariableDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitInitializedVariableDeclaration(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitInitializedVariableDeclaration(this);
			else return visitor.visitChildren(this);
		}
	}

	public final InitializedVariableDeclarationContext initializedVariableDeclaration() throws RecognitionException {
		InitializedVariableDeclarationContext _localctx = new InitializedVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_initializedVariableDeclaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(171);
			type(0);
			setState(172);
			match(ID);
			setState(173);
			match(ASSIGN);
			setState(174);
			expression(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InferedVariableDeclarationContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode LET() { return getToken(JurParser.LET, 0); }
		public TerminalNode ASSIGN() { return getToken(JurParser.ASSIGN, 0); }
		public InferedVariableDeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inferedVariableDeclaration; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterInferedVariableDeclaration(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitInferedVariableDeclaration(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitInferedVariableDeclaration(this);
			else return visitor.visitChildren(this);
		}
	}

	public final InferedVariableDeclarationContext inferedVariableDeclaration() throws RecognitionException {
		InferedVariableDeclarationContext _localctx = new InferedVariableDeclarationContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_inferedVariableDeclaration);
		try {
			setState(183);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(176);
				match(ID);
				setState(177);
				match(T__0);
				setState(178);
				expression(0);
				}
				break;
			case LET:
				enterOuterAlt(_localctx, 2);
				{
				setState(179);
				match(LET);
				setState(180);
				match(ID);
				setState(181);
				match(ASSIGN);
				setState(182);
				expression(0);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	 
		public TypeContext() { }
		public void copyFrom(TypeContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ArrayTypeContext extends TypeContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public TerminalNode LEFT_SQUARE_PARENT() { return getToken(JurParser.LEFT_SQUARE_PARENT, 0); }
		public TerminalNode RIGHT_SQUARE_PARENT() { return getToken(JurParser.RIGHT_SQUARE_PARENT, 0); }
		public ArrayTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterArrayType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitArrayType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitArrayType(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class TypeParameterOrStructTypeContext extends TypeContext {
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public TypeParameterOrStructTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterTypeParameterOrStructType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitTypeParameterOrStructType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitTypeParameterOrStructType(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class FunctionPointerTypeContext extends TypeContext {
		public TerminalNode VOID() { return getToken(JurParser.VOID, 0); }
		public TerminalNode LEFT_PARENT() { return getToken(JurParser.LEFT_PARENT, 0); }
		public TerminalNode RIGHT_PARENT() { return getToken(JurParser.RIGHT_PARENT, 0); }
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public List<TerminalNode> COMA() { return getTokens(JurParser.COMA); }
		public TerminalNode COMA(int i) {
			return getToken(JurParser.COMA, i);
		}
		public FunctionPointerTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterFunctionPointerType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitFunctionPointerType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitFunctionPointerType(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class GenericStructTypeContext extends TypeContext {
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public TerminalNode LESS() { return getToken(JurParser.LESS, 0); }
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public TerminalNode GREATER() { return getToken(JurParser.GREATER, 0); }
		public List<TerminalNode> COMA() { return getTokens(JurParser.COMA); }
		public TerminalNode COMA(int i) {
			return getToken(JurParser.COMA, i);
		}
		public GenericStructTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterGenericStructType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitGenericStructType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitGenericStructType(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class PrimitiveTypeContext extends TypeContext {
		public TerminalNode PRIMITIVE() { return getToken(JurParser.PRIMITIVE, 0); }
		public PrimitiveTypeContext(TypeContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterPrimitiveType(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitPrimitiveType(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitPrimitiveType(this);
			else return visitor.visitChildren(this);
		}
	}

	public final TypeContext type() throws RecognitionException {
		return type(0);
	}

	private TypeContext type(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		TypeContext _localctx = new TypeContext(_ctx, _parentState);
		TypeContext _prevctx = _localctx;
		int _startState = 22;
		enterRecursionRule(_localctx, 22, RULE_type, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(213);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,27,_ctx) ) {
			case 1:
				{
				_localctx = new PrimitiveTypeContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(186);
				match(PRIMITIVE);
				}
				break;
			case 2:
				{
				_localctx = new TypeParameterOrStructTypeContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(187);
				match(ID);
				}
				break;
			case 3:
				{
				_localctx = new GenericStructTypeContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(188);
				match(ID);
				{
				setState(189);
				match(LESS);
				setState(190);
				type(0);
				setState(195);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==COMA) {
					{
					{
					setState(191);
					match(COMA);
					setState(192);
					type(0);
					}
					}
					setState(197);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(198);
				match(GREATER);
				}
				}
				break;
			case 4:
				{
				_localctx = new FunctionPointerTypeContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(200);
				match(VOID);
				setState(201);
				match(LEFT_PARENT);
				setState(210);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(202);
					type(0);
					setState(207);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(203);
						match(COMA);
						setState(204);
						type(0);
						}
						}
						setState(209);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(212);
				match(RIGHT_PARENT);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(233);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,31,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(231);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,30,_ctx) ) {
					case 1:
						{
						_localctx = new FunctionPointerTypeContext(new TypeContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_type);
						setState(215);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(216);
						match(LEFT_PARENT);
						setState(225);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
							{
							setState(217);
							type(0);
							setState(222);
							_errHandler.sync(this);
							_la = _input.LA(1);
							while (_la==COMA) {
								{
								{
								setState(218);
								match(COMA);
								setState(219);
								type(0);
								}
								}
								setState(224);
								_errHandler.sync(this);
								_la = _input.LA(1);
							}
							}
						}

						setState(227);
						match(RIGHT_PARENT);
						}
						break;
					case 2:
						{
						_localctx = new ArrayTypeContext(new TypeContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_type);
						setState(228);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(229);
						match(LEFT_SQUARE_PARENT);
						setState(230);
						match(RIGHT_SQUARE_PARENT);
						}
						break;
					}
					} 
				}
				setState(235);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,31,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	 
		public StatementContext() { }
		public void copyFrom(StatementContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class BlockStatementContext extends StatementContext {
		public TerminalNode LEFT_CURLY() { return getToken(JurParser.LEFT_CURLY, 0); }
		public TerminalNode RIGHT_CURLY() { return getToken(JurParser.RIGHT_CURLY, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public BlockStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterBlockStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitBlockStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitBlockStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ExitStatementContext extends StatementContext {
		public TerminalNode EXIT() { return getToken(JurParser.EXIT, 0); }
		public TerminalNode LEFT_PARENT() { return getToken(JurParser.LEFT_PARENT, 0); }
		public TerminalNode STRING_VALUE() { return getToken(JurParser.STRING_VALUE, 0); }
		public TerminalNode RIGHT_PARENT() { return getToken(JurParser.RIGHT_PARENT, 0); }
		public ExitStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterExitStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitExitStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitExitStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class AssignmentStatementContext extends StatementContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode ASSIGN() { return getToken(JurParser.ASSIGN, 0); }
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public AssignmentStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterAssignmentStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitAssignmentStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitAssignmentStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class UninitializedVarDeclarationStatementContext extends StatementContext {
		public UninitializedVarDeclarationContext uninitializedVarDeclaration() {
			return getRuleContext(UninitializedVarDeclarationContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public UninitializedVarDeclarationStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterUninitializedVarDeclarationStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitUninitializedVarDeclarationStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitUninitializedVarDeclarationStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ForStatementContext extends StatementContext {
		public TerminalNode FOR() { return getToken(JurParser.FOR, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public List<TerminalNode> SEMICOLON() { return getTokens(JurParser.SEMICOLON); }
		public TerminalNode SEMICOLON(int i) {
			return getToken(JurParser.SEMICOLON, i);
		}
		public InitializedVariableDeclarationContext initializedVariableDeclaration() {
			return getRuleContext(InitializedVariableDeclarationContext.class,0);
		}
		public InferedVariableDeclarationContext inferedVariableDeclaration() {
			return getRuleContext(InferedVariableDeclarationContext.class,0);
		}
		public ForStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterForStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitForStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitForStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class BreakStatementContext extends StatementContext {
		public TerminalNode BREAK() { return getToken(JurParser.BREAK, 0); }
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public BreakStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterBreakStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitBreakStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitBreakStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class IfStatementContext extends StatementContext {
		public TerminalNode IF() { return getToken(JurParser.IF, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(JurParser.ELSE, 0); }
		public IfStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterIfStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitIfStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitIfStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ReturnStatementContext extends StatementContext {
		public TerminalNode RETURN() { return getToken(JurParser.RETURN, 0); }
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public ReturnStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterReturnStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitReturnStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitReturnStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class InferedVariableDeclarationStatementContext extends StatementContext {
		public InferedVariableDeclarationContext inferedVariableDeclaration() {
			return getRuleContext(InferedVariableDeclarationContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public InferedVariableDeclarationStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterInferedVariableDeclarationStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitInferedVariableDeclarationStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitInferedVariableDeclarationStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class InitializedVariableDeclarationStatementContext extends StatementContext {
		public InitializedVariableDeclarationContext initializedVariableDeclaration() {
			return getRuleContext(InitializedVariableDeclarationContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public InitializedVariableDeclarationStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterInitializedVariableDeclarationStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitInitializedVariableDeclarationStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitInitializedVariableDeclarationStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ForeachStatementContext extends StatementContext {
		public TerminalNode FOR() { return getToken(JurParser.FOR, 0); }
		public UninitializedVarDeclarationContext uninitializedVarDeclaration() {
			return getRuleContext(UninitializedVarDeclarationContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TerminalNode IN() { return getToken(JurParser.IN, 0); }
		public ForeachStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterForeachStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitForeachStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitForeachStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ContinueStatementContext extends StatementContext {
		public TerminalNode CONTINUE() { return getToken(JurParser.CONTINUE, 0); }
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public ContinueStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterContinueStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitContinueStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitContinueStatement(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ExpressionStatementContext extends StatementContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode SEMICOLON() { return getToken(JurParser.SEMICOLON, 0); }
		public ExpressionStatementContext(StatementContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterExpressionStatement(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitExpressionStatement(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitExpressionStatement(this);
			else return visitor.visitChildren(this);
		}
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_statement);
		int _la;
		try {
			setState(303);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,38,_ctx) ) {
			case 1:
				_localctx = new BlockStatementContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(236);
				match(LEFT_CURLY);
				setState(240);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_CURLY) | (1L << LEFT_PARENT) | (1L << STRING_VALUE) | (1L << NUMBER_VALUE) | (1L << BOOL_VALUE) | (1L << NULL_VALUE) | (1L << VOID) | (1L << RETURN) | (1L << BREAK) | (1L << CONTINUE) | (1L << EXIT) | (1L << IF) | (1L << NEW) | (1L << FOR) | (1L << LET) | (1L << ARROW) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					{
					setState(237);
					statement();
					}
					}
					setState(242);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(243);
				match(RIGHT_CURLY);
				}
				break;
			case 2:
				_localctx = new IfStatementContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(244);
				match(IF);
				setState(245);
				expression(0);
				setState(246);
				statement();
				setState(249);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,33,_ctx) ) {
				case 1:
					{
					setState(247);
					match(ELSE);
					setState(248);
					statement();
					}
					break;
				}
				}
				break;
			case 3:
				_localctx = new ForStatementContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(251);
				match(FOR);
				setState(258);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,35,_ctx) ) {
				case 1:
					{
					setState(254);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,34,_ctx) ) {
					case 1:
						{
						setState(252);
						initializedVariableDeclaration();
						}
						break;
					case 2:
						{
						setState(253);
						inferedVariableDeclaration();
						}
						break;
					}
					setState(256);
					match(SEMICOLON);
					}
					break;
				}
				setState(260);
				expression(0);
				setState(263);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==SEMICOLON) {
					{
					setState(261);
					match(SEMICOLON);
					setState(262);
					expression(0);
					}
				}

				setState(265);
				statement();
				}
				break;
			case 4:
				_localctx = new ForeachStatementContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(267);
				match(FOR);
				setState(268);
				uninitializedVarDeclaration();
				setState(269);
				_la = _input.LA(1);
				if ( !(_la==T__1 || _la==IN) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(270);
				expression(0);
				setState(271);
				statement();
				}
				break;
			case 5:
				_localctx = new ReturnStatementContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(273);
				match(RETURN);
				setState(275);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_PARENT) | (1L << STRING_VALUE) | (1L << NUMBER_VALUE) | (1L << BOOL_VALUE) | (1L << NULL_VALUE) | (1L << VOID) | (1L << NEW) | (1L << ARROW) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(274);
					expression(0);
					}
				}

				setState(277);
				match(SEMICOLON);
				}
				break;
			case 6:
				_localctx = new BreakStatementContext(_localctx);
				enterOuterAlt(_localctx, 6);
				{
				setState(278);
				match(BREAK);
				setState(279);
				match(SEMICOLON);
				}
				break;
			case 7:
				_localctx = new ContinueStatementContext(_localctx);
				enterOuterAlt(_localctx, 7);
				{
				setState(280);
				match(CONTINUE);
				setState(281);
				match(SEMICOLON);
				}
				break;
			case 8:
				_localctx = new InferedVariableDeclarationStatementContext(_localctx);
				enterOuterAlt(_localctx, 8);
				{
				setState(282);
				inferedVariableDeclaration();
				setState(283);
				match(SEMICOLON);
				}
				break;
			case 9:
				_localctx = new InitializedVariableDeclarationStatementContext(_localctx);
				enterOuterAlt(_localctx, 9);
				{
				setState(285);
				initializedVariableDeclaration();
				setState(286);
				match(SEMICOLON);
				}
				break;
			case 10:
				_localctx = new UninitializedVarDeclarationStatementContext(_localctx);
				enterOuterAlt(_localctx, 10);
				{
				setState(288);
				uninitializedVarDeclaration();
				setState(289);
				match(SEMICOLON);
				}
				break;
			case 11:
				_localctx = new AssignmentStatementContext(_localctx);
				enterOuterAlt(_localctx, 11);
				{
				setState(291);
				expression(0);
				setState(292);
				match(ASSIGN);
				setState(293);
				expression(0);
				setState(294);
				match(SEMICOLON);
				}
				break;
			case 12:
				_localctx = new ExpressionStatementContext(_localctx);
				enterOuterAlt(_localctx, 12);
				{
				setState(296);
				expression(0);
				setState(297);
				match(SEMICOLON);
				}
				break;
			case 13:
				_localctx = new ExitStatementContext(_localctx);
				enterOuterAlt(_localctx, 13);
				{
				setState(299);
				match(EXIT);
				setState(300);
				match(LEFT_PARENT);
				setState(301);
				match(STRING_VALUE);
				setState(302);
				match(RIGHT_PARENT);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BlockContext extends ParserRuleContext {
		public TerminalNode LEFT_CURLY() { return getToken(JurParser.LEFT_CURLY, 0); }
		public TerminalNode RIGHT_CURLY() { return getToken(JurParser.RIGHT_CURLY, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public BlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_block; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterBlock(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitBlock(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitBlock(this);
			else return visitor.visitChildren(this);
		}
	}

	public final BlockContext block() throws RecognitionException {
		BlockContext _localctx = new BlockContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(305);
			match(LEFT_CURLY);
			setState(309);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_CURLY) | (1L << LEFT_PARENT) | (1L << STRING_VALUE) | (1L << NUMBER_VALUE) | (1L << BOOL_VALUE) | (1L << NULL_VALUE) | (1L << VOID) | (1L << RETURN) | (1L << BREAK) | (1L << CONTINUE) | (1L << EXIT) | (1L << IF) | (1L << NEW) | (1L << FOR) | (1L << LET) | (1L << ARROW) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
				{
				{
				setState(306);
				statement();
				}
				}
				setState(311);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(312);
			match(RIGHT_CURLY);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	 
		public ExpressionContext() { }
		public void copyFrom(ExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class VariableAccessContext extends ExpressionContext {
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public VariableAccessContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterVariableAccess(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitVariableAccess(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitVariableAccess(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class AnonymusFunctionContext extends ExpressionContext {
		public TerminalNode ARROW() { return getToken(JurParser.ARROW, 0); }
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public List<UninitializedVarDeclarationContext> uninitializedVarDeclaration() {
			return getRuleContexts(UninitializedVarDeclarationContext.class);
		}
		public UninitializedVarDeclarationContext uninitializedVarDeclaration(int i) {
			return getRuleContext(UninitializedVarDeclarationContext.class,i);
		}
		public TerminalNode LEFT_PARENT() { return getToken(JurParser.LEFT_PARENT, 0); }
		public TerminalNode RIGHT_PARENT() { return getToken(JurParser.RIGHT_PARENT, 0); }
		public List<TerminalNode> COMA() { return getTokens(JurParser.COMA); }
		public TerminalNode COMA(int i) {
			return getToken(JurParser.COMA, i);
		}
		public AnonymusFunctionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterAnonymusFunction(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitAnonymusFunction(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitAnonymusFunction(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class PrimitiveValueContext extends ExpressionContext {
		public Token value;
		public TerminalNode NUMBER_VALUE() { return getToken(JurParser.NUMBER_VALUE, 0); }
		public TerminalNode STRING_VALUE() { return getToken(JurParser.STRING_VALUE, 0); }
		public TerminalNode BOOL_VALUE() { return getToken(JurParser.BOOL_VALUE, 0); }
		public TerminalNode NULL_VALUE() { return getToken(JurParser.NULL_VALUE, 0); }
		public PrimitiveValueContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterPrimitiveValue(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitPrimitiveValue(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitPrimitiveValue(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class FunctionCallContext extends ExpressionContext {
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public TerminalNode LEFT_PARENT() { return getToken(JurParser.LEFT_PARENT, 0); }
		public TerminalNode RIGHT_PARENT() { return getToken(JurParser.RIGHT_PARENT, 0); }
		public List<TerminalNode> LESS() { return getTokens(JurParser.LESS); }
		public TerminalNode LESS(int i) {
			return getToken(JurParser.LESS, i);
		}
		public TerminalNode POLY() { return getToken(JurParser.POLY, 0); }
		public List<TerminalNode> GREATER() { return getTokens(JurParser.GREATER); }
		public TerminalNode GREATER(int i) {
			return getToken(JurParser.GREATER, i);
		}
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<TerminalNode> COMA() { return getTokens(JurParser.COMA); }
		public TerminalNode COMA(int i) {
			return getToken(JurParser.COMA, i);
		}
		public TerminalNode DOT() { return getToken(JurParser.DOT, 0); }
		public FunctionCallContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterFunctionCall(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitFunctionCall(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitFunctionCall(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class FieldAccessContext extends ExpressionContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode DOT() { return getToken(JurParser.DOT, 0); }
		public TerminalNode ID() { return getToken(JurParser.ID, 0); }
		public FieldAccessContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterFieldAccess(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitFieldAccess(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitFieldAccess(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ArrayIndexAccessContext extends ExpressionContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode LEFT_SQUARE_PARENT() { return getToken(JurParser.LEFT_SQUARE_PARENT, 0); }
		public TerminalNode RIGHT_SQUARE_PARENT() { return getToken(JurParser.RIGHT_SQUARE_PARENT, 0); }
		public ArrayIndexAccessContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterArrayIndexAccess(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitArrayIndexAccess(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitArrayIndexAccess(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ConstructorContext extends ExpressionContext {
		public TerminalNode NEW() { return getToken(JurParser.NEW, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public ConstructorContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterConstructor(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitConstructor(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitConstructor(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class OperationContext extends ExpressionContext {
		public Token operator;
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode TIMES() { return getToken(JurParser.TIMES, 0); }
		public TerminalNode DIVIDE() { return getToken(JurParser.DIVIDE, 0); }
		public TerminalNode ADD() { return getToken(JurParser.ADD, 0); }
		public TerminalNode SUBTRACT() { return getToken(JurParser.SUBTRACT, 0); }
		public TerminalNode LESS() { return getToken(JurParser.LESS, 0); }
		public TerminalNode GREATER() { return getToken(JurParser.GREATER, 0); }
		public TerminalNode LEQUAL() { return getToken(JurParser.LEQUAL, 0); }
		public TerminalNode GREQUAL() { return getToken(JurParser.GREQUAL, 0); }
		public TerminalNode EQUAL() { return getToken(JurParser.EQUAL, 0); }
		public TerminalNode IS() { return getToken(JurParser.IS, 0); }
		public TerminalNode NOT_EQUAL() { return getToken(JurParser.NOT_EQUAL, 0); }
		public TerminalNode AND() { return getToken(JurParser.AND, 0); }
		public TerminalNode OR() { return getToken(JurParser.OR, 0); }
		public OperationContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterOperation(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitOperation(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitOperation(this);
			else return visitor.visitChildren(this);
		}
	}
	public static class ParExpressionContext extends ExpressionContext {
		public TerminalNode LEFT_PARENT() { return getToken(JurParser.LEFT_PARENT, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode RIGHT_PARENT() { return getToken(JurParser.RIGHT_PARENT, 0); }
		public ParExpressionContext(ExpressionContext ctx) { copyFrom(ctx); }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).enterParExpression(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof JurListener ) ((JurListener)listener).exitParExpression(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof JurVisitor ) return ((JurVisitor<? extends T>)visitor).visitParExpression(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ExpressionContext expression() throws RecognitionException {
		return expression(0);
	}

	private ExpressionContext expression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpressionContext _localctx = new ExpressionContext(_ctx, _parentState);
		ExpressionContext _prevctx = _localctx;
		int _startState = 28;
		enterRecursionRule(_localctx, 28, RULE_expression, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(379);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,50,_ctx) ) {
			case 1:
				{
				_localctx = new VariableAccessContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(315);
				match(ID);
				}
				break;
			case 2:
				{
				_localctx = new PrimitiveValueContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(316);
				((PrimitiveValueContext)_localctx).value = _input.LT(1);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << STRING_VALUE) | (1L << NUMBER_VALUE) | (1L << BOOL_VALUE) | (1L << NULL_VALUE))) != 0)) ) {
					((PrimitiveValueContext)_localctx).value = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				break;
			case 3:
				{
				_localctx = new AnonymusFunctionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(318);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(317);
					uninitializedVarDeclaration();
					}
				}

				setState(320);
				match(ARROW);
				setState(323);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LEFT_CURLY:
					{
					setState(321);
					block();
					}
					break;
				case LEFT_PARENT:
				case STRING_VALUE:
				case NUMBER_VALUE:
				case BOOL_VALUE:
				case NULL_VALUE:
				case VOID:
				case NEW:
				case ARROW:
				case PRIMITIVE:
				case ID:
					{
					setState(322);
					expression(0);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				break;
			case 4:
				{
				_localctx = new AnonymusFunctionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(325);
				match(LEFT_PARENT);
				setState(334);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VOID) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(326);
					uninitializedVarDeclaration();
					setState(331);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(327);
						match(COMA);
						setState(328);
						uninitializedVarDeclaration();
						}
						}
						setState(333);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(336);
				match(RIGHT_PARENT);
				setState(337);
				match(ARROW);
				setState(340);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case LEFT_CURLY:
					{
					setState(338);
					block();
					}
					break;
				case LEFT_PARENT:
				case STRING_VALUE:
				case NUMBER_VALUE:
				case BOOL_VALUE:
				case NULL_VALUE:
				case VOID:
				case NEW:
				case ARROW:
				case PRIMITIVE:
				case ID:
					{
					setState(339);
					expression(0);
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				break;
			case 5:
				{
				_localctx = new FunctionCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(342);
				match(ID);
				setState(346);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,45,_ctx) ) {
				case 1:
					{
					setState(343);
					match(LESS);
					setState(344);
					match(POLY);
					setState(345);
					match(GREATER);
					}
					break;
				}
				setState(359);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LESS) {
					{
					setState(348);
					match(LESS);
					setState(349);
					type(0);
					setState(354);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(350);
						match(COMA);
						setState(351);
						type(0);
						}
						}
						setState(356);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(357);
					match(GREATER);
					}
				}

				setState(361);
				match(LEFT_PARENT);
				setState(370);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_PARENT) | (1L << STRING_VALUE) | (1L << NUMBER_VALUE) | (1L << BOOL_VALUE) | (1L << NULL_VALUE) | (1L << VOID) | (1L << NEW) | (1L << ARROW) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
					{
					setState(362);
					expression(0);
					setState(367);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==COMA) {
						{
						{
						setState(363);
						match(COMA);
						setState(364);
						expression(0);
						}
						}
						setState(369);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(372);
				match(RIGHT_PARENT);
				}
				break;
			case 6:
				{
				_localctx = new ConstructorContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(373);
				match(NEW);
				setState(374);
				type(0);
				}
				break;
			case 7:
				{
				_localctx = new ParExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(375);
				match(LEFT_PARENT);
				setState(376);
				expression(0);
				setState(377);
				match(RIGHT_PARENT);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(442);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,57,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(440);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,56,_ctx) ) {
					case 1:
						{
						_localctx = new OperationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(381);
						if (!(precpred(_ctx, 6))) throw new FailedPredicateException(this, "precpred(_ctx, 6)");
						setState(382);
						((OperationContext)_localctx).operator = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==TIMES || _la==DIVIDE) ) {
							((OperationContext)_localctx).operator = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(383);
						expression(7);
						}
						break;
					case 2:
						{
						_localctx = new OperationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(384);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(385);
						((OperationContext)_localctx).operator = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==ADD || _la==SUBTRACT) ) {
							((OperationContext)_localctx).operator = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(386);
						expression(6);
						}
						break;
					case 3:
						{
						_localctx = new OperationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(387);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(388);
						((OperationContext)_localctx).operator = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LESS) | (1L << LEQUAL) | (1L << GREATER) | (1L << GREQUAL))) != 0)) ) {
							((OperationContext)_localctx).operator = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(389);
						expression(5);
						}
						break;
					case 4:
						{
						_localctx = new OperationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(390);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(391);
						((OperationContext)_localctx).operator = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << EQUAL) | (1L << IS) | (1L << NOT_EQUAL))) != 0)) ) {
							((OperationContext)_localctx).operator = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(392);
						expression(4);
						}
						break;
					case 5:
						{
						_localctx = new OperationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(393);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(394);
						match(AND);
						setState(395);
						expression(3);
						}
						break;
					case 6:
						{
						_localctx = new OperationContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(396);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(397);
						match(OR);
						setState(398);
						expression(2);
						}
						break;
					case 7:
						{
						_localctx = new FunctionCallContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(399);
						if (!(precpred(_ctx, 11))) throw new FailedPredicateException(this, "precpred(_ctx, 11)");
						setState(400);
						match(DOT);
						setState(401);
						match(ID);
						setState(405);
						_errHandler.sync(this);
						switch ( getInterpreter().adaptivePredict(_input,51,_ctx) ) {
						case 1:
							{
							setState(402);
							match(LESS);
							setState(403);
							match(POLY);
							setState(404);
							match(GREATER);
							}
							break;
						}
						setState(418);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==LESS) {
							{
							setState(407);
							match(LESS);
							setState(408);
							type(0);
							setState(413);
							_errHandler.sync(this);
							_la = _input.LA(1);
							while (_la==COMA) {
								{
								{
								setState(409);
								match(COMA);
								setState(410);
								type(0);
								}
								}
								setState(415);
								_errHandler.sync(this);
								_la = _input.LA(1);
							}
							setState(416);
							match(GREATER);
							}
						}

						setState(420);
						match(LEFT_PARENT);
						setState(429);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LEFT_PARENT) | (1L << STRING_VALUE) | (1L << NUMBER_VALUE) | (1L << BOOL_VALUE) | (1L << NULL_VALUE) | (1L << VOID) | (1L << NEW) | (1L << ARROW) | (1L << PRIMITIVE) | (1L << ID))) != 0)) {
							{
							setState(421);
							expression(0);
							setState(426);
							_errHandler.sync(this);
							_la = _input.LA(1);
							while (_la==COMA) {
								{
								{
								setState(422);
								match(COMA);
								setState(423);
								expression(0);
								}
								}
								setState(428);
								_errHandler.sync(this);
								_la = _input.LA(1);
							}
							}
						}

						setState(431);
						match(RIGHT_PARENT);
						}
						break;
					case 8:
						{
						_localctx = new FieldAccessContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(432);
						if (!(precpred(_ctx, 10))) throw new FailedPredicateException(this, "precpred(_ctx, 10)");
						setState(433);
						match(DOT);
						setState(434);
						match(ID);
						}
						break;
					case 9:
						{
						_localctx = new ArrayIndexAccessContext(new ExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expression);
						setState(435);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(436);
						match(LEFT_SQUARE_PARENT);
						setState(437);
						expression(0);
						setState(438);
						match(RIGHT_SQUARE_PARENT);
						}
						break;
					}
					} 
				}
				setState(444);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,57,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 11:
			return type_sempred((TypeContext)_localctx, predIndex);
		case 14:
			return expression_sempred((ExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean type_sempred(TypeContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 3);
		case 1:
			return precpred(_ctx, 1);
		}
		return true;
	}
	private boolean expression_sempred(ExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 2:
			return precpred(_ctx, 6);
		case 3:
			return precpred(_ctx, 5);
		case 4:
			return precpred(_ctx, 4);
		case 5:
			return precpred(_ctx, 3);
		case 6:
			return precpred(_ctx, 2);
		case 7:
			return precpred(_ctx, 1);
		case 8:
			return precpred(_ctx, 11);
		case 9:
			return precpred(_ctx, 10);
		case 10:
			return precpred(_ctx, 8);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3;\u01c0\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\3\2\3\2\7\2#\n\2\f\2"+
		"\16\2&\13\2\3\2\3\2\3\3\3\3\3\3\3\4\3\4\3\4\3\4\3\4\7\4\62\n\4\f\4\16"+
		"\4\65\13\4\3\4\3\4\3\5\5\5:\n\5\3\5\3\5\3\5\3\5\3\5\3\5\7\5B\n\5\f\5\16"+
		"\5E\13\5\3\5\5\5H\n\5\3\5\3\5\3\5\3\5\3\5\7\5O\n\5\f\5\16\5R\13\5\3\5"+
		"\3\5\3\6\3\6\3\6\3\6\3\7\3\7\5\7\\\n\7\3\7\3\7\3\7\3\7\3\7\7\7c\n\7\f"+
		"\7\16\7f\13\7\3\7\5\7i\n\7\3\7\3\7\3\7\3\7\7\7o\n\7\f\7\16\7r\13\7\5\7"+
		"t\n\7\3\7\3\7\5\7x\n\7\3\7\3\7\3\7\3\7\5\7~\n\7\3\7\3\7\3\7\3\7\3\7\7"+
		"\7\u0085\n\7\f\7\16\7\u0088\13\7\3\7\5\7\u008b\n\7\3\7\3\7\3\7\3\7\7\7"+
		"\u0091\n\7\f\7\16\7\u0094\13\7\5\7\u0096\n\7\3\7\3\7\5\7\u009a\n\7\5\7"+
		"\u009c\n\7\3\b\3\b\3\b\3\b\7\b\u00a2\n\b\f\b\16\b\u00a5\13\b\3\t\3\t\3"+
		"\t\3\t\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\5\f\u00ba\n\f\3\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\7\r\u00c4\n\r\f\r\16"+
		"\r\u00c7\13\r\3\r\3\r\3\r\3\r\3\r\3\r\3\r\7\r\u00d0\n\r\f\r\16\r\u00d3"+
		"\13\r\5\r\u00d5\n\r\3\r\5\r\u00d8\n\r\3\r\3\r\3\r\3\r\3\r\7\r\u00df\n"+
		"\r\f\r\16\r\u00e2\13\r\5\r\u00e4\n\r\3\r\3\r\3\r\3\r\7\r\u00ea\n\r\f\r"+
		"\16\r\u00ed\13\r\3\16\3\16\7\16\u00f1\n\16\f\16\16\16\u00f4\13\16\3\16"+
		"\3\16\3\16\3\16\3\16\3\16\5\16\u00fc\n\16\3\16\3\16\3\16\5\16\u0101\n"+
		"\16\3\16\3\16\5\16\u0105\n\16\3\16\3\16\3\16\5\16\u010a\n\16\3\16\3\16"+
		"\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\5\16\u0116\n\16\3\16\3\16\3\16"+
		"\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16"+
		"\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\5\16\u0132\n\16\3\17\3\17"+
		"\7\17\u0136\n\17\f\17\16\17\u0139\13\17\3\17\3\17\3\20\3\20\3\20\3\20"+
		"\5\20\u0141\n\20\3\20\3\20\3\20\5\20\u0146\n\20\3\20\3\20\3\20\3\20\7"+
		"\20\u014c\n\20\f\20\16\20\u014f\13\20\5\20\u0151\n\20\3\20\3\20\3\20\3"+
		"\20\5\20\u0157\n\20\3\20\3\20\3\20\3\20\5\20\u015d\n\20\3\20\3\20\3\20"+
		"\3\20\7\20\u0163\n\20\f\20\16\20\u0166\13\20\3\20\3\20\5\20\u016a\n\20"+
		"\3\20\3\20\3\20\3\20\7\20\u0170\n\20\f\20\16\20\u0173\13\20\5\20\u0175"+
		"\n\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\5\20\u017e\n\20\3\20\3\20\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\20\3\20\5\20\u0198\n\20\3\20\3\20\3\20\3\20"+
		"\7\20\u019e\n\20\f\20\16\20\u01a1\13\20\3\20\3\20\5\20\u01a5\n\20\3\20"+
		"\3\20\3\20\3\20\7\20\u01ab\n\20\f\20\16\20\u01ae\13\20\5\20\u01b0\n\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\7\20\u01bb\n\20\f\20\16"+
		"\20\u01be\13\20\3\20\2\4\30\36\21\2\4\6\b\n\f\16\20\22\24\26\30\32\34"+
		"\36\2\b\4\2\4\4\61\61\3\2!$\3\2\17\20\3\2\r\16\3\2\27\32\4\2\33\34\36"+
		"\36\2\u0203\2$\3\2\2\2\4)\3\2\2\2\6,\3\2\2\2\b9\3\2\2\2\nU\3\2\2\2\f\u009b"+
		"\3\2\2\2\16\u009d\3\2\2\2\20\u00a6\3\2\2\2\22\u00aa\3\2\2\2\24\u00ad\3"+
		"\2\2\2\26\u00b9\3\2\2\2\30\u00d7\3\2\2\2\32\u0131\3\2\2\2\34\u0133\3\2"+
		"\2\2\36\u017d\3\2\2\2 #\5\4\3\2!#\5\6\4\2\" \3\2\2\2\"!\3\2\2\2#&\3\2"+
		"\2\2$\"\3\2\2\2$%\3\2\2\2%\'\3\2\2\2&$\3\2\2\2\'(\7\2\2\3(\3\3\2\2\2)"+
		"*\7/\2\2*+\5\32\16\2+\5\3\2\2\2,-\7.\2\2-.\7\"\2\2.\63\7\21\2\2/\62\5"+
		"\f\7\2\60\62\5\b\5\2\61/\3\2\2\2\61\60\3\2\2\2\62\65\3\2\2\2\63\61\3\2"+
		"\2\2\63\64\3\2\2\2\64\66\3\2\2\2\65\63\3\2\2\2\66\67\7\22\2\2\67\7\3\2"+
		"\2\28:\7\64\2\298\3\2\2\29:\3\2\2\2:;\3\2\2\2;<\7&\2\2<G\7;\2\2=>\7\27"+
		"\2\2>C\7;\2\2?@\7\b\2\2@B\7;\2\2A?\3\2\2\2BE\3\2\2\2CA\3\2\2\2CD\3\2\2"+
		"\2DF\3\2\2\2EC\3\2\2\2FH\7\31\2\2G=\3\2\2\2GH\3\2\2\2HI\3\2\2\2IP\7\21"+
		"\2\2JK\5\22\n\2KL\7\n\2\2LO\3\2\2\2MO\5\n\6\2NJ\3\2\2\2NM\3\2\2\2OR\3"+
		"\2\2\2PN\3\2\2\2PQ\3\2\2\2QS\3\2\2\2RP\3\2\2\2ST\7\22\2\2T\t\3\2\2\2U"+
		"V\7\34\2\2VW\5\30\r\2WX\7\n\2\2X\13\3\2\2\2Y\\\5\30\r\2Z\\\7\'\2\2[Y\3"+
		"\2\2\2[Z\3\2\2\2\\]\3\2\2\2]h\7;\2\2^_\7\27\2\2_d\7;\2\2`a\7\b\2\2ac\7"+
		";\2\2b`\3\2\2\2cf\3\2\2\2db\3\2\2\2de\3\2\2\2eg\3\2\2\2fd\3\2\2\2gi\7"+
		"\31\2\2h^\3\2\2\2hi\3\2\2\2ij\3\2\2\2js\7\23\2\2kp\5\22\n\2lm\7\b\2\2"+
		"mo\5\22\n\2nl\3\2\2\2or\3\2\2\2pn\3\2\2\2pq\3\2\2\2qt\3\2\2\2rp\3\2\2"+
		"\2sk\3\2\2\2st\3\2\2\2tu\3\2\2\2uw\7\24\2\2vx\5\16\b\2wv\3\2\2\2wx\3\2"+
		"\2\2xy\3\2\2\2y\u009c\5\32\16\2z}\7\64\2\2{~\5\30\r\2|~\7\'\2\2}{\3\2"+
		"\2\2}|\3\2\2\2~\177\3\2\2\2\177\u008a\7;\2\2\u0080\u0081\7\27\2\2\u0081"+
		"\u0086\7;\2\2\u0082\u0083\7\b\2\2\u0083\u0085\7;\2\2\u0084\u0082\3\2\2"+
		"\2\u0085\u0088\3\2\2\2\u0086\u0084\3\2\2\2\u0086\u0087\3\2\2\2\u0087\u0089"+
		"\3\2\2\2\u0088\u0086\3\2\2\2\u0089\u008b\7\31\2\2\u008a\u0080\3\2\2\2"+
		"\u008a\u008b\3\2\2\2\u008b\u008c\3\2\2\2\u008c\u0095\7\23\2\2\u008d\u0092"+
		"\5\22\n\2\u008e\u008f\7\b\2\2\u008f\u0091\5\22\n\2\u0090\u008e\3\2\2\2"+
		"\u0091\u0094\3\2\2\2\u0092\u0090\3\2\2\2\u0092\u0093\3\2\2\2\u0093\u0096"+
		"\3\2\2\2\u0094\u0092\3\2\2\2\u0095\u008d\3\2\2\2\u0095\u0096\3\2\2\2\u0096"+
		"\u0097\3\2\2\2\u0097\u0099\7\24\2\2\u0098\u009a\5\16\b\2\u0099\u0098\3"+
		"\2\2\2\u0099\u009a\3\2\2\2\u009a\u009c\3\2\2\2\u009b[\3\2\2\2\u009bz\3"+
		"\2\2\2\u009c\r\3\2\2\2\u009d\u009e\7\35\2\2\u009e\u00a3\5\20\t\2\u009f"+
		"\u00a0\7\37\2\2\u00a0\u00a2\5\20\t\2\u00a1\u009f\3\2\2\2\u00a2\u00a5\3"+
		"\2\2\2\u00a3\u00a1\3\2\2\2\u00a3\u00a4\3\2\2\2\u00a4\17\3\2\2\2\u00a5"+
		"\u00a3\3\2\2\2\u00a6\u00a7\5\30\r\2\u00a7\u00a8\7\34\2\2\u00a8\u00a9\5"+
		"\30\r\2\u00a9\21\3\2\2\2\u00aa\u00ab\5\30\r\2\u00ab\u00ac\7;\2\2\u00ac"+
		"\23\3\2\2\2\u00ad\u00ae\5\30\r\2\u00ae\u00af\7;\2\2\u00af\u00b0\7\f\2"+
		"\2\u00b0\u00b1\5\36\20\2\u00b1\25\3\2\2\2\u00b2\u00b3\7;\2\2\u00b3\u00b4"+
		"\7\3\2\2\u00b4\u00ba\5\36\20\2\u00b5\u00b6\7\65\2\2\u00b6\u00b7\7;\2\2"+
		"\u00b7\u00b8\7\f\2\2\u00b8\u00ba\5\36\20\2\u00b9\u00b2\3\2\2\2\u00b9\u00b5"+
		"\3\2\2\2\u00ba\27\3\2\2\2\u00bb\u00bc\b\r\1\2\u00bc\u00d8\78\2\2\u00bd"+
		"\u00d8\7;\2\2\u00be\u00bf\7;\2\2\u00bf\u00c0\7\27\2\2\u00c0\u00c5\5\30"+
		"\r\2\u00c1\u00c2\7\b\2\2\u00c2\u00c4\5\30\r\2\u00c3\u00c1\3\2\2\2\u00c4"+
		"\u00c7\3\2\2\2\u00c5\u00c3\3\2\2\2\u00c5\u00c6\3\2\2\2\u00c6\u00c8\3\2"+
		"\2\2\u00c7\u00c5\3\2\2\2\u00c8\u00c9\7\31\2\2\u00c9\u00d8\3\2\2\2\u00ca"+
		"\u00cb\7\'\2\2\u00cb\u00d4\7\23\2\2\u00cc\u00d1\5\30\r\2\u00cd\u00ce\7"+
		"\b\2\2\u00ce\u00d0\5\30\r\2\u00cf\u00cd\3\2\2\2\u00d0\u00d3\3\2\2\2\u00d1"+
		"\u00cf\3\2\2\2\u00d1\u00d2\3\2\2\2\u00d2\u00d5\3\2\2\2\u00d3\u00d1\3\2"+
		"\2\2\u00d4\u00cc\3\2\2\2\u00d4\u00d5\3\2\2\2\u00d5\u00d6\3\2\2\2\u00d6"+
		"\u00d8\7\24\2\2\u00d7\u00bb\3\2\2\2\u00d7\u00bd\3\2\2\2\u00d7\u00be\3"+
		"\2\2\2\u00d7\u00ca\3\2\2\2\u00d8\u00eb\3\2\2\2\u00d9\u00da\f\5\2\2\u00da"+
		"\u00e3\7\23\2\2\u00db\u00e0\5\30\r\2\u00dc\u00dd\7\b\2\2\u00dd\u00df\5"+
		"\30\r\2\u00de\u00dc\3\2\2\2\u00df\u00e2\3\2\2\2\u00e0\u00de\3\2\2\2\u00e0"+
		"\u00e1\3\2\2\2\u00e1\u00e4\3\2\2\2\u00e2\u00e0\3\2\2\2\u00e3\u00db\3\2"+
		"\2\2\u00e3\u00e4\3\2\2\2\u00e4\u00e5\3\2\2\2\u00e5\u00ea\7\24\2\2\u00e6"+
		"\u00e7\f\3\2\2\u00e7\u00e8\7\25\2\2\u00e8\u00ea\7\26\2\2\u00e9\u00d9\3"+
		"\2\2\2\u00e9\u00e6\3\2\2\2\u00ea\u00ed\3\2\2\2\u00eb\u00e9\3\2\2\2\u00eb"+
		"\u00ec\3\2\2\2\u00ec\31\3\2\2\2\u00ed\u00eb\3\2\2\2\u00ee\u00f2\7\21\2"+
		"\2\u00ef\u00f1\5\32\16\2\u00f0\u00ef\3\2\2\2\u00f1\u00f4\3\2\2\2\u00f2"+
		"\u00f0\3\2\2\2\u00f2\u00f3\3\2\2\2\u00f3\u00f5\3\2\2\2\u00f4\u00f2\3\2"+
		"\2\2\u00f5\u0132\7\22\2\2\u00f6\u00f7\7,\2\2\u00f7\u00f8\5\36\20\2\u00f8"+
		"\u00fb\5\32\16\2\u00f9\u00fa\7\62\2\2\u00fa\u00fc\5\32\16\2\u00fb\u00f9"+
		"\3\2\2\2\u00fb\u00fc\3\2\2\2\u00fc\u0132\3\2\2\2\u00fd\u0104\7\63\2\2"+
		"\u00fe\u0101\5\24\13\2\u00ff\u0101\5\26\f\2\u0100\u00fe\3\2\2\2\u0100"+
		"\u00ff\3\2\2\2\u0101\u0102\3\2\2\2\u0102\u0103\7\n\2\2\u0103\u0105\3\2"+
		"\2\2\u0104\u0100\3\2\2\2\u0104\u0105\3\2\2\2\u0105\u0106\3\2\2\2\u0106"+
		"\u0109\5\36\20\2\u0107\u0108\7\n\2\2\u0108\u010a\5\36\20\2\u0109\u0107"+
		"\3\2\2\2\u0109\u010a\3\2\2\2\u010a\u010b\3\2\2\2\u010b\u010c\5\32\16\2"+
		"\u010c\u0132\3\2\2\2\u010d\u010e\7\63\2\2\u010e\u010f\5\22\n\2\u010f\u0110"+
		"\t\2\2\2\u0110\u0111\5\36\20\2\u0111\u0112\5\32\16\2\u0112\u0132\3\2\2"+
		"\2\u0113\u0115\7(\2\2\u0114\u0116\5\36\20\2\u0115\u0114\3\2\2\2\u0115"+
		"\u0116\3\2\2\2\u0116\u0117\3\2\2\2\u0117\u0132\7\n\2\2\u0118\u0119\7)"+
		"\2\2\u0119\u0132\7\n\2\2\u011a\u011b\7*\2\2\u011b\u0132\7\n\2\2\u011c"+
		"\u011d\5\26\f\2\u011d\u011e\7\n\2\2\u011e\u0132\3\2\2\2\u011f\u0120\5"+
		"\24\13\2\u0120\u0121\7\n\2\2\u0121\u0132\3\2\2\2\u0122\u0123\5\22\n\2"+
		"\u0123\u0124\7\n\2\2\u0124\u0132\3\2\2\2\u0125\u0126\5\36\20\2\u0126\u0127"+
		"\7\f\2\2\u0127\u0128\5\36\20\2\u0128\u0129\7\n\2\2\u0129\u0132\3\2\2\2"+
		"\u012a\u012b\5\36\20\2\u012b\u012c\7\n\2\2\u012c\u0132\3\2\2\2\u012d\u012e"+
		"\7+\2\2\u012e\u012f\7\23\2\2\u012f\u0130\7!\2\2\u0130\u0132\7\24\2\2\u0131"+
		"\u00ee\3\2\2\2\u0131\u00f6\3\2\2\2\u0131\u00fd\3\2\2\2\u0131\u010d\3\2"+
		"\2\2\u0131\u0113\3\2\2\2\u0131\u0118\3\2\2\2\u0131\u011a\3\2\2\2\u0131"+
		"\u011c\3\2\2\2\u0131\u011f\3\2\2\2\u0131\u0122\3\2\2\2\u0131\u0125\3\2"+
		"\2\2\u0131\u012a\3\2\2\2\u0131\u012d\3\2\2\2\u0132\33\3\2\2\2\u0133\u0137"+
		"\7\21\2\2\u0134\u0136\5\32\16\2\u0135\u0134\3\2\2\2\u0136\u0139\3\2\2"+
		"\2\u0137\u0135\3\2\2\2\u0137\u0138\3\2\2\2\u0138\u013a\3\2\2\2\u0139\u0137"+
		"\3\2\2\2\u013a\u013b\7\22\2\2\u013b\35\3\2\2\2\u013c\u013d\b\20\1\2\u013d"+
		"\u017e\7;\2\2\u013e\u017e\t\3\2\2\u013f\u0141\5\22\n\2\u0140\u013f\3\2"+
		"\2\2\u0140\u0141\3\2\2\2\u0141\u0142\3\2\2\2\u0142\u0145\7\67\2\2\u0143"+
		"\u0146\5\34\17\2\u0144\u0146\5\36\20\2\u0145\u0143\3\2\2\2\u0145\u0144"+
		"\3\2\2\2\u0146\u017e\3\2\2\2\u0147\u0150\7\23\2\2\u0148\u014d\5\22\n\2"+
		"\u0149\u014a\7\b\2\2\u014a\u014c\5\22\n\2\u014b\u0149\3\2\2\2\u014c\u014f"+
		"\3\2\2\2\u014d\u014b\3\2\2\2\u014d\u014e\3\2\2\2\u014e\u0151\3\2\2\2\u014f"+
		"\u014d\3\2\2\2\u0150\u0148\3\2\2\2\u0150\u0151\3\2\2\2\u0151\u0152\3\2"+
		"\2\2\u0152\u0153\7\24\2\2\u0153\u0156\7\67\2\2\u0154\u0157\5\34\17\2\u0155"+
		"\u0157\5\36\20\2\u0156\u0154\3\2\2\2\u0156\u0155\3\2\2\2\u0157\u017e\3"+
		"\2\2\2\u0158\u015c\7;\2\2\u0159\u015a\7\27\2\2\u015a\u015b\7\66\2\2\u015b"+
		"\u015d\7\31\2\2\u015c\u0159\3\2\2\2\u015c\u015d\3\2\2\2\u015d\u0169\3"+
		"\2\2\2\u015e\u015f\7\27\2\2\u015f\u0164\5\30\r\2\u0160\u0161\7\b\2\2\u0161"+
		"\u0163\5\30\r\2\u0162\u0160\3\2\2\2\u0163\u0166\3\2\2\2\u0164\u0162\3"+
		"\2\2\2\u0164\u0165\3\2\2\2\u0165\u0167\3\2\2\2\u0166\u0164\3\2\2\2\u0167"+
		"\u0168\7\31\2\2\u0168\u016a\3\2\2\2\u0169\u015e\3\2\2\2\u0169\u016a\3"+
		"\2\2\2\u016a\u016b\3\2\2\2\u016b\u0174\7\23\2\2\u016c\u0171\5\36\20\2"+
		"\u016d\u016e\7\b\2\2\u016e\u0170\5\36\20\2\u016f\u016d\3\2\2\2\u0170\u0173"+
		"\3\2\2\2\u0171\u016f\3\2\2\2\u0171\u0172\3\2\2\2\u0172\u0175\3\2\2\2\u0173"+
		"\u0171\3\2\2\2\u0174\u016c\3\2\2\2\u0174\u0175\3\2\2\2\u0175\u0176\3\2"+
		"\2\2\u0176\u017e\7\24\2\2\u0177\u0178\7\60\2\2\u0178\u017e\5\30\r\2\u0179"+
		"\u017a\7\23\2\2\u017a\u017b\5\36\20\2\u017b\u017c\7\24\2\2\u017c\u017e"+
		"\3\2\2\2\u017d\u013c\3\2\2\2\u017d\u013e\3\2\2\2\u017d\u0140\3\2\2\2\u017d"+
		"\u0147\3\2\2\2\u017d\u0158\3\2\2\2\u017d\u0177\3\2\2\2\u017d\u0179\3\2"+
		"\2\2\u017e\u01bc\3\2\2\2\u017f\u0180\f\b\2\2\u0180\u0181\t\4\2\2\u0181"+
		"\u01bb\5\36\20\t\u0182\u0183\f\7\2\2\u0183\u0184\t\5\2\2\u0184\u01bb\5"+
		"\36\20\b\u0185\u0186\f\6\2\2\u0186\u0187\t\6\2\2\u0187\u01bb\5\36\20\7"+
		"\u0188\u0189\f\5\2\2\u0189\u018a\t\7\2\2\u018a\u01bb\5\36\20\6\u018b\u018c"+
		"\f\4\2\2\u018c\u018d\7\37\2\2\u018d\u01bb\5\36\20\5\u018e\u018f\f\3\2"+
		"\2\u018f\u0190\7 \2\2\u0190\u01bb\5\36\20\4\u0191\u0192\f\r\2\2\u0192"+
		"\u0193\7\t\2\2\u0193\u0197\7;\2\2\u0194\u0195\7\27\2\2\u0195\u0196\7\66"+
		"\2\2\u0196\u0198\7\31\2\2\u0197\u0194\3\2\2\2\u0197\u0198\3\2\2\2\u0198"+
		"\u01a4\3\2\2\2\u0199\u019a\7\27\2\2\u019a\u019f\5\30\r\2\u019b\u019c\7"+
		"\b\2\2\u019c\u019e\5\30\r\2\u019d\u019b\3\2\2\2\u019e\u01a1\3\2\2\2\u019f"+
		"\u019d\3\2\2\2\u019f\u01a0\3\2\2\2\u01a0\u01a2\3\2\2\2\u01a1\u019f\3\2"+
		"\2\2\u01a2\u01a3\7\31\2\2\u01a3\u01a5\3\2\2\2\u01a4\u0199\3\2\2\2\u01a4"+
		"\u01a5\3\2\2\2\u01a5\u01a6\3\2\2\2\u01a6\u01af\7\23\2\2\u01a7\u01ac\5"+
		"\36\20\2\u01a8\u01a9\7\b\2\2\u01a9\u01ab\5\36\20\2\u01aa\u01a8\3\2\2\2"+
		"\u01ab\u01ae\3\2\2\2\u01ac\u01aa\3\2\2\2\u01ac\u01ad\3\2\2\2\u01ad\u01b0"+
		"\3\2\2\2\u01ae\u01ac\3\2\2\2\u01af\u01a7\3\2\2\2\u01af\u01b0\3\2\2\2\u01b0"+
		"\u01b1\3\2\2\2\u01b1\u01bb\7\24\2\2\u01b2\u01b3\f\f\2\2\u01b3\u01b4\7"+
		"\t\2\2\u01b4\u01bb\7;\2\2\u01b5\u01b6\f\n\2\2\u01b6\u01b7\7\25\2\2\u01b7"+
		"\u01b8\5\36\20\2\u01b8\u01b9\7\26\2\2\u01b9\u01bb\3\2\2\2\u01ba\u017f"+
		"\3\2\2\2\u01ba\u0182\3\2\2\2\u01ba\u0185\3\2\2\2\u01ba\u0188\3\2\2\2\u01ba"+
		"\u018b\3\2\2\2\u01ba\u018e\3\2\2\2\u01ba\u0191\3\2\2\2\u01ba\u01b2\3\2"+
		"\2\2\u01ba\u01b5\3\2\2\2\u01bb\u01be\3\2\2\2\u01bc\u01ba\3\2\2\2\u01bc"+
		"\u01bd\3\2\2\2\u01bd\37\3\2\2\2\u01be\u01bc\3\2\2\2<\"$\61\639CGNP[dh"+
		"psw}\u0086\u008a\u0092\u0095\u0099\u009b\u00a3\u00b9\u00c5\u00d1\u00d4"+
		"\u00d7\u00e0\u00e3\u00e9\u00eb\u00f2\u00fb\u0100\u0104\u0109\u0115\u0131"+
		"\u0137\u0140\u0145\u014d\u0150\u0156\u015c\u0164\u0169\u0171\u0174\u017d"+
		"\u0197\u019f\u01a4\u01ac\u01af\u01ba\u01bc";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}