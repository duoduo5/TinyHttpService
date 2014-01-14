﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyHttpService.RequestData
{
    public class HttpRequestHeader
    {
        public string Uri { get; internal set; }
        public string RequestMethod { get; internal set; }
        public Dictionary<string, string> Properties { get; internal set; }

        public string this[string key]
        {
            get 
            {
                return this.Properties[key];
            }
            set 
            {
                this.Properties[key] = value;
            }
        }

        public HttpRequestHeader()
        {
            this.Properties = new Dictionary<string,string>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var property in Properties)
            {
                sb.AppendFormat(@"{0}:{1}{2}", property.Key, property.Value, Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
