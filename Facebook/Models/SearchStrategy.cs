﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public interface SearchStrategy
    {
        List<Persona> BuscarAmigos(string busqueda);
    }
}
