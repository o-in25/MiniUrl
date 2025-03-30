using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniUrl.Lib.Requests
{
    public class ShortUrlRequest
    {
        public required string LongUrl { get; set; }
        public string? CustomShortUrl { get; set; }
    }
}
