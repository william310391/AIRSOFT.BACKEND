﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airsoft.Application.DTOs.Request
{
    public class LoginRequest
    {
        public string? UsuarioNombre { get; set; }
        public string? Password { get; set; }
    }
}
