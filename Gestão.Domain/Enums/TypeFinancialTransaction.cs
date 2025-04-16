using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestão.Domain.Enums
{
    public enum TypeFinancialTransaction
    {
        [Display(Name = "Pago")]
        Pay,
        [Display(Name = "Recebido")]
        Receive
    }
}
