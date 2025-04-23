﻿namespace Gestao.Domain
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;
        public int? FinancialTransactionId { get; set; }
        public FinancialTransaction? FinancialTransaction { get; set; }

    }
}
