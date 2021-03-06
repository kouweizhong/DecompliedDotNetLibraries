﻿namespace System.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;

    internal class MethodCallExpression3 : MethodCallExpression, IArgumentProvider
    {
        private object _arg0;
        private readonly Expression _arg1;
        private readonly Expression _arg2;

        public MethodCallExpression3(MethodInfo method, Expression arg0, Expression arg1, Expression arg2) : base(method)
        {
            this._arg0 = arg0;
            this._arg1 = arg1;
            this._arg2 = arg2;
        }

        internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
        {
            return Expression.ReturnReadOnly(this, ref this._arg0);
        }

        internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
        {
            if (args != null)
            {
                return Expression.Call(base.Method, args[0], args[1], args[2]);
            }
            return Expression.Call(base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
        }

        Expression IArgumentProvider.GetArgument(int index)
        {
            switch (index)
            {
                case 0:
                    return Expression.ReturnObject<Expression>(this._arg0);

                case 1:
                    return this._arg1;

                case 2:
                    return this._arg2;
            }
            throw new InvalidOperationException();
        }

        int IArgumentProvider.ArgumentCount
        {
            get
            {
                return 3;
            }
        }
    }
}

