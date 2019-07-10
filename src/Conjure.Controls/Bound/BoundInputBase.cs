using Conjure.Data.Client;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conjure.Controls.Bound
{
    public class BoundInputBase<T> : ComponentBase
    {
        [Parameter] protected string Label { get; set; }
        [Parameter] protected object ValueRoot { get; set; }
        [Parameter] protected string ValuePath { get; set; }
        [Parameter] protected bool ReadOnly { get; set; }
        [Parameter] protected EventCallback Changed { get; set; }

        protected string inputId = $"input-{Guid.NewGuid()}";
        protected BindTarget valueTarget;

        protected T TypedValue
        {
            get
            {
                var val = valueTarget.Value;
                if (val is T)
                    return (T)val;
                else
                    return default(T);
            }
            set
            {
                valueTarget.Value = value;
                Changed.InvokeAsync(null);
            }
        }

        protected override void OnParametersSet()
        {
            valueTarget = new BindTarget(ValueRoot, ValuePath);
        }
    }
}
