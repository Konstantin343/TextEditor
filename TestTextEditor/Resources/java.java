package expression.parser;

import expression.*;
import expression.exceptions.*;
import operations.Operation;

public class ExpressionParser<T> implements Parser<T> {
	
	private Tokenizer<T> Tokenizer;
	Operation<T> operation;
	
	public ExpressionParser(Operation<T> op) {
		operation = op;
	}
	
	private TripleExpression<T> unary() throws ParsingException {
		TripleExpression<T> ans;
		Token cur = Tokenizer.getNext();
		switch (cur) {
        	case CONST:
        		ans = new Const<T>(Tokenizer.getValue());
        		Tokenizer.getNext();
        		break;
        	case VARIABLE:
        		ans = new Variable<T>(Tokenizer.getVar());
        		Tokenizer.getNext();
        		break;
        	case MINUS:
        		ans = new Negate<T>(unary(), operation);
        		break;
        	case ABS:
        		ans = new Abs<T>(unary(), operation);
        		break;
        	case SQR:
        		ans = new Square<T>(unary(), operation);
        		break;
        	case OPEN_BRACKET:
        		int pos = Tokenizer.getPos() - 1;
        		ans = minMax();
        		if (Tokenizer.getCur() != Token.CLOSE_BRACKET) {
                    throw new MissingClosingBracketException(pos, Tokenizer.getExpression());
                }
        		Tokenizer.getNext();
        		break;
        	default: 
        		ans = new Const<T>(operation.parseNumber("0"));
			}
		return ans;
	}
	
	private TripleExpression<T> mulDivMod() throws ParsingException {
		TripleExpression<T> ans = unary();
		while(true) {
			switch (Tokenizer.getCur()) {
				case MUL:
					ans = new Multiply<T>(ans, unary(), operation);
					break;
				case DIV:
					ans = new Divide<T>(ans, unary(), operation);
					break;
				case MOD:
					ans = new Mod<T>(ans, unary(), operation);
					break;
				default:
					return ans;
			}
		}
	}
	
	private TripleExpression<T> addSub() throws ParsingException {
		TripleExpression<T> ans = mulDivMod();
		while(true) {
			switch (Tokenizer.getCur()) {
				case ADD:
					ans = new Add<T>(ans, mulDivMod(), operation);
					break;
				case SUB:
					ans = new Subtract<T>(ans, mulDivMod(), operation);
					break;
				default:
					return ans;
			}
		}		
	}
	
	private TripleExpression<T> minMax() throws ParsingException {
		TripleExpression<T> ans = addSub();
		while(true) {
			switch (Tokenizer.getCur()) {
				case MIN:
					ans = new Min<T>(ans, addSub(), operation);
					break;
				case MAX:
					ans = new Max<T>(ans, addSub(), operation);
					break;
				default:
					return ans;
			}
		}		
	}
	
	public TripleExpression<T> parse(String newExpression) throws ParsingException {
		Tokenizer = new Tokenizer<T>(newExpression, operation);
		return minMax();
	}
	
}