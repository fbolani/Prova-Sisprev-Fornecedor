﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<Domain.SolicitacaoCompraAggregate.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<Domain.SolicitacaoCompraAggregate.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");

            builder.OwnsOne(c => c.TotalGeral, b => b.Property("Value").HasColumnName("TotalGeral"));
            builder.OwnsOne(c => c.UsuarioSolicitante, b => b.Property("Nome").HasColumnName("UsuarioSolicitante"));
            builder.OwnsOne(c => c.NomeFornecedor, b => b.Property("Nome").HasColumnName("NomeFornecedor"));
            builder.OwnsOne(c => c.CondicaoPagamento, b => b.Property("Valor").HasColumnName("CondicaoPagamento"));
        }
    }
}