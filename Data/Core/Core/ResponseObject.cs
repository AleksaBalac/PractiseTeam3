using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Core
{
    public class ResponseObject<T>
    {
        [DataMember()]
        public bool Success { get; set; } = true;

        [DataMember()]
        public string Message { get; set; }

        [DataMember()]
        public string Total { get; set; }

        [DataMember()]
        public T Data { get; set; }
      
    }
}
