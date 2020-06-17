using System;
using System.Collections.Generic;

namespace Relocation
{
    public abstract class ModelBase
    {
        protected void SetValue<T>(ref T field, T value, EventHandler? eventHandler)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }
            field = value;
            eventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}