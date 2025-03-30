using MiniUrl.Dal.Interfaces;
using MiniUrl.Lib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiniUrl.Dal {
    public class UrlCondenserService : IUrlCondenserService {
        private readonly ConcurrentDictionary<string, ShortUrl> _condenser = new();

        public string Condense(string longUrl, string? customShortUrl = null) {
            var shortUrl = new ShortUrl {
                LongUrl = longUrl
            };

            if(!_condenser.ContainsKey(shortUrl.HashKey)) {
                _condenser.TryAdd(shortUrl.HashKey, shortUrl);
            }

            return shortUrl.HashKey;
        }

        public bool Delete(string shortUrl) {
            throw new NotImplementedException();
        }

        public string? Get(string shortUrl) {

            _ = _condenser.TryGetValue(shortUrl, out ShortUrl value);

            return value.LongUrl;


        }

        public int GetRetrievalCount(string shortUrl) {
            throw new NotImplementedException();
        }
    }
}
