using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniUrl.Lib{
    public class Transaction<T> {
        public string TransactionId { get; } = Guid.NewGuid().ToString();
        public bool HasError { get; set; } = false;
        public string StatusMessage { get; set; } = "Operation completed successfully.";

        public required T Data {  get; set; }

    }
}
