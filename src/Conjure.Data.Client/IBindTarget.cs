using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Conjure.Data.Client
{
    public interface IBindTarget
    {
        bool IsReadOnly { get; }

        object Value { get; set; }
    }

    public interface IBindTarget<TValue> : IBindTarget
    {
        TValue TypedValue { get; set; }
    }
}
