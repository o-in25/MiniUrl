using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniUrl.Dal.Interfaces {
    public interface IUrlCondenserService {
        string Condense(string longUrl, string? customShortUrl = null);
        bool Delete(string shortUrl);
        string? Get(string shortUrl);
        int GetRetrievalCount(string shortUrl);

    }
}
