using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo1
{
    /// <summary>
    /// Element that is used in Stack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class StackElement<T>
    {
        public T Value { get; private set; }

        public StackElement<T> Previous { get; internal set; }

        public StackElement() { } // default ctor for serialization etc.

        public StackElement(T value)
        {
            Value = value;
        }
    }
}
