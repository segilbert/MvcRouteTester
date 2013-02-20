using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using ServiceStack.Text;

namespace MvcRouteTester.Fluent
{
	public class ExpressionReader
	{
		public IDictionary<string, string> Read<TController>(Expression<Func<TController, object>> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}

			return Read(typeof(TController), (MethodCallExpression)action.Body);
		}

		public IDictionary<string, string> Read<TController>(Expression<Func<TController, ActionResult>> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}

			return Read(typeof(TController), (MethodCallExpression)action.Body);
		}

		private IDictionary<string, string> Read(Type controllerType, MethodCallExpression methodCall)
		{
			var values = new Dictionary<string, string>();
			values.Add("controller", ControllerName(controllerType));
			values.Add("action", ActionName(methodCall));
			AddParameters(methodCall, values);
			return values;
		}

		private string ControllerName(Type controllertype)
		{
			const int SuffixLength = 10;
			var controllerName = controllertype.Name;
			if ((controllerName.Length > SuffixLength) && controllerName.EndsWith("Controller"))
			{
				controllerName = controllerName.Substring(0, controllerName.Length - SuffixLength);
			}

			return controllerName;
		}

		private string ActionName(MethodCallExpression methodCall)
		{
			return  methodCall.Method.Name;
		}

		private void AddParameters(MethodCallExpression methodCall, IDictionary<string, string> values)
		{
			var parameters = methodCall.Method.GetParameters();
			var arguments = methodCall.Arguments;

			for (int i = 0; i < parameters.Length; i++)
			{
				var expectedValue = GetExpectedValue(arguments[i]);

                // Convention -> Type Name contains 'Model' convert to JSON
                if (IsNotCoreType(arguments[i].Type)) //.Name.Contains("Model"))
                    expectedValue = expectedValue.SerializeToString();
                
				var expectedString = expectedValue != null ? expectedValue.ToString() : null;

				values.Add(parameters[i].Name, expectedString);
			}
		}

        private bool IsNotCoreType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }

		private static object GetExpectedValue(Expression argumentExpression)
		{
            switch (argumentExpression.NodeType)
			{
				case ExpressionType.Constant:
					return ((ConstantExpression)argumentExpression).Value;

				case ExpressionType.MemberAccess:
					return Expression.Lambda(argumentExpression).Compile().DynamicInvoke();
				
				default:
					return null;
			}
		} 
	}
}