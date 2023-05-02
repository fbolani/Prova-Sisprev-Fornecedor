using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;


namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ISolicitacaoCompraRepository solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(IUnitOfWork uow, IMediator mediator, IProdutoRepository produtoRepository,ISolicitacaoCompraRepository solicitacaoCompraRepository) : base(uow, mediator)
        {
            this.produtoRepository = produtoRepository;
            this.solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
            var itens = new List<Item>();

            foreach (var iten in request.Itens)
            {
                var produto = produtoRepository.Obter(iten.Produto.Id);
                var item = new Item(produto, iten.Qtde);
                itens.Add(item);
            }

            solicitacaoCompra.RegistrarCompra(itens);
            solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}