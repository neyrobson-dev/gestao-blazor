using System.ComponentModel.DataAnnotations;

namespace Gestao.Domain.Enums
{
    public enum TypeFinancialTransaction
    {
        [Display(Name = "Pago")]
        Pay,
        [Display(Name = "Recebido")]
        Receive
    }
}
