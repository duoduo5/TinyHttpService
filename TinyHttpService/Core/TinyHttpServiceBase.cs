﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TinyHttpService.Core.Interface;

namespace TinyHttpService.Core
{
    public abstract class TinyHttpServiceBase : IHttpService
    {
        private TcpListener listener;
        private IHttpServiceHandler httpServiceHandler;

        public TinyHttpServiceBase(IHttpServiceHandler handler)
        {
            this.httpServiceHandler = handler;
        }

        public virtual void Bind(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            listener.BeginAcceptTcpClient(AcceptCallback, listener);
        }

        private void AcceptCallback(IAsyncResult ar) 
        {
            var listener = ar.AsyncState as TcpListener;

            var client = listener.EndAcceptTcpClient(ar);
            if (client != null)
            {
                Task task = new Task(new Action<object>((obj) => 
                {
                    var tcpClient = obj as TcpClient;
                    var stream = tcpClient.GetStream();
                    httpServiceHandler.ProcessRequest(stream);

                    try { stream.Close(); } catch { }
                    try { tcpClient.Close(); } catch { }

                }), client);

                task.Start();
            }
        }

        public virtual void Close()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }
    }
}
