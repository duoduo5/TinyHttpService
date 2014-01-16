﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TinyHttpService.HttpData;

namespace TinyHttpService.RequestParser.Interface
{
    public interface IHttpRequestParser
    {
        HttpRequest Parse(NetworkStream stream);
    }
}
