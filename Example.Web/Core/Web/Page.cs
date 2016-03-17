using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Web.Core.Web
{
    public class Page<TOption>
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public TOption Option { get; set; }
    }
}