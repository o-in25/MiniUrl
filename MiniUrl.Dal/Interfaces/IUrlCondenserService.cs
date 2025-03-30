using MiniUrl.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MiniUrl.Dal.Interfaces {
    public interface IUrlCondenserService {
        Transaction<string> Condense(string longUrl, string? customShortUrl = null);
        Transaction<bool> Delete(string shortUrl);
        Transaction<string> Get(string shortUrl);
        Transaction<int> GetRetrievalCount(string shortUrl);

    }
}
