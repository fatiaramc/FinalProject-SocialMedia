using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class ConcreteMemento :IMemento
    {
        private string _state;

        public ConcreteMemento(string state)
        {
            _state = state;
        }

        public string GetState()
        {
            return this._state;
        }

    }
}
