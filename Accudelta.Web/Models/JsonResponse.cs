using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accudelta.Web.Models
{
    public class JsonResponse
    {
        public bool Result { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}