using MiniUrl.Dal.Interfaces;
using MiniUrl.Lib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiniUrl.Dal {
    public class UrlCondenserService : IUrlCondenserService {
        private readonly ConcurrentDictionary<string, ShortUrl> _condenser = new();
        

        public Transaction<string> Condense(string longUrl, string? customShortUrl = null) {
            var response = new Transaction<string> { Data = "" };
            try {
                var shortUrl = new ShortUrl {
                    LongUrl = longUrl,
                    CustomShortUrl = customShortUrl
                };
                if(!_condenser.ContainsKey(shortUrl.HashKey)) {
                    _condenser.TryAdd(shortUrl.HashKey, shortUrl);
                }

                response.Data = shortUrl.HashKey;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                response.HasError = true;
                response.StatusMessage = e.Message;
            }

            return response;
        }

        public Transaction<bool> Delete(string shortUrl) {
            var response = new Transaction<bool> { Data = false };
            try {
                if(_condenser.TryRemove(shortUrl, out var data)) {
                    response.Data = true;
                }

            } catch(Exception e) {
                Console.WriteLine(e.Message);
                response.HasError = true;
                response.StatusMessage = e.Message;
            }

            return response;
        }

        public Transaction<string> Get(string shortUrl) {
            var response = new Transaction<string> { Data = "" };
            try {

                if(_condenser.TryGetValue(shortUrl, out var data)) {
                    var newData = new ShortUrl {
                        LongUrl = data.LongUrl,
                        CustomShortUrl = data.CustomShortUrl,
                        ClickCount = data.ClickCount + 1,
                        CreatedAt = data.CreatedAt
                    };

                    _condenser.TryUpdate(shortUrl,newData, data);
                    response.Data = newData.LongUrl;

                } else {
                    throw new Exception("Short URL not found.");
                }

            } catch(Exception e) {
                Console.WriteLine(e.Message);
                response.HasError = true;
                response.StatusMessage = e.Message;
            }

            return response;
        }

        public Transaction<int> GetRetrievalCount(string shortUrl) {
            var response = new Transaction<int> { Data = 0 };
            try {
                response.Data = _condenser.TryGetValue(shortUrl, out var data) ? data.ClickCount : throw new Exception("Short URL not found.");
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                response.HasError = true;
                response.StatusMessage = e.Message;
            }

            return response;
        }

        public Transaction<ShortUrl[]> List() {
            var response = new Transaction<ShortUrl[]> { Data = [] };
            try {
                if(!_condenser.IsEmpty) {
                    response.Data = [.. _condenser.Values];
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                response.HasError = true;
                response.StatusMessage = e.Message;
            }

            return response;
        }
    }
}
