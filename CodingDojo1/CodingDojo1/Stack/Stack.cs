using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo1
{
    /// <summary>
    /// LIFO Stack - push, pop, peek
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>
    {
        private StackElement<T> _currentElement;

        /// <summary>
        /// pushes a value to the stack
        /// </summary>
        /// <param name="value">the value</param>
        public void Push(T value)
        {
            var newElement = new StackElement<T>(value);
            if (_currentElement == null)
                _currentElement = newElement;
            else
            {
                newElement.Previous = _currentElement;
                _currentElement = newElement;
            }
        }

        /// <summary>
        /// Pops a value from the stack (read and delete)
        /// </summary>
        /// <returns>the value of the last element</returns>
        public T Pop()
        {
            if (_currentElement == null)
                throw new NullReferenceException("No elements at the stack!");

            var value = _currentElement.Value;
            _currentElement = _currentElement.Previous;

            return value;
        }

        /// <summary>
        /// Reads the current value from the stack
        /// </summary>
        /// <returns>value of the last element</returns>
        public T Peek()
        {
            if (_currentElement == null)
                return default(T);

            return _currentElement.Value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== STACK ===");
            if (_currentElement == null)
                return String.Empty;

            var tmp = _currentElement;
            while (tmp != null)
            {
                sb.AppendLine(tmp.Value?.ToString());
                tmp = tmp.Previous;
            }

            sb.AppendLine("=== STACK END ===");
            return sb.ToString();
        }
    }
}
