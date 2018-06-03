﻿using System.Collections.Generic;

namespace WhiteRaven.Data.AppModel
{
    public class AppPagedResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Page { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public List<GridFilter> Filter { get; set; }
        public List<GridSort> Sort { get; set; }
    }
}
