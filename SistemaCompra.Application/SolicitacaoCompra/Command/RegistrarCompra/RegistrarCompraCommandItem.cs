using MediatR;
using SistemaCompra.Application.Produto.Command.RegistrarProduto;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandItem : IRequest<bool>
    {
        public RegistrarCompraCommandProduto Produto { get; set; }
        public int Qtde { get; set; }
    }
}