﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiniUrl.Lib {
    public class ShortUrl {
        //public string HashCode { get; set; } = string.Empty;
        public string LongUrl { get; set; } = string.Empty;
        public string? CustomShortUrl { get; set; } = null;
        public int ClickCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string HashKey => GetHashKey(LongUrl, CustomShortUrl);

        private static string GetHashKey(string longUrl, string? customShortUrl = null) {
            // return either custom key or hash
            if(customShortUrl != null && customShortUrl != string.Empty) return customShortUrl;
            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(longUrl));
            return Convert.ToBase64String(hash)[..8]
            .Replace('+', '-')
            .Replace('/', '_');
        }
    }
}
