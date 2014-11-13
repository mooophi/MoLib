namespace Mophi.Xml
{
	public interface IMathParser
	{
		void SetExpression(string expression);
		string GetExpression();
		void SetVariable(string name, float value);
		void RemoveAllVariables();
		double Evaluate();
	}

	public interface IMathParserFactory
	{
		IMathParser CreateMathParser(string expression);
	}

	public interface IExpressionObject
	{
		void SetupVariable(IMathParser parser);
	}

	public interface IExpressionCalc
	{
		double CalcExpression(string exp, params IExpressionObject[] objs);
	}
}
