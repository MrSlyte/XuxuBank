using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuxuBank.Domain.Models;


public readonly record struct CreateTransactionResponseModel(long Limite, long Saldo);


//{
//    "limite" : 100000,
//    "saldo" : -9098
//}
