using Microsoft.Extensions.Logging;
using VendasAPI.Domain.Dtos;
using VendasAPI.Domain.Entities;
using VendasAPI.Domain.Events;
using VendasAPI.Domain.Repositories;

namespace VendasAPI.Domain.Services
{
    public class VendaService(IVendaRepository vendaRepository, ILogger<VendaService> logger) : IVendaService
    {
        private readonly IVendaRepository _vendaRepository = vendaRepository;
        public readonly ILogger<VendaService> _logger = logger ;


        public Venda Create(VendaDto vendaDto)
        {
            if (vendaDto.Itens == null || !vendaDto.Itens.Any())
            {
                throw new ArgumentException("Itens de Venda não podem ser vazios.");
            }
            var venda = new Venda
            {
                ClienteId = vendaDto.ClienteId,
                ClienteNome = vendaDto.ClienteNome,
                ItemVenda = vendaDto.Itens.Select(i => new ItemVenda
                {
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.ProdutoNome,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,
                    Desconto = i.Desconto,
                    ValorTotal = i.ValorTotal
                }).ToList(),
                ValorTotal = vendaDto.Itens.Sum(i => i.ValorTotal),
                Cancelado = false,
                DataVenda = DateTime.UtcNow
            };

            _vendaRepository.Add(venda);

            // Publica o evento de CompraCriada
            var evento = new CompraCriadaEvent
            {
                VendaId = venda.VendaId,
                DataCriacao = DateTime.UtcNow,
                ClienteId = venda.ClienteId,
                ValorTotal = venda.ValorTotal
            };
            PublicarEvento(evento);

            return venda;
        }

        public IEnumerable<Venda> GetAll() => _vendaRepository.GetAll();

        public Venda GetById(int id) => _vendaRepository.GetById(id);

        public void Update(int id, VendaDto vendaDto)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null) return;

            venda.ClienteId = vendaDto.ClienteId;
            venda.ClienteNome = vendaDto.ClienteNome;
            venda.ItemVenda = vendaDto.Itens.Select(i => new ItemVenda
            {
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.ProdutoNome,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario,
                Desconto = i.Desconto,
                ValorTotal = i.ValorTotal
            }).ToList();
            venda.ValorTotal = vendaDto.Itens.Sum(i => i.ValorTotal);

            _vendaRepository.Update(venda);

            // Publica o evento de CompraAlterada
            var evento = new CompraAlteradaEvent
            {
                VendaId = venda.VendaId,
                DataAlteracao = DateTime.UtcNow,
                NovoValorTotal = venda.ValorTotal
            };
            PublicarEvento(evento);

        }
        public void Cancel(int id)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null) return;

            venda.Cancelado = true;
            _vendaRepository.Update(venda);

            // Publica o evento de CompraCancelada
            var evento = new CompraCanceladaEvent
            {
                VendaId = venda.VendaId,
                DataCancelamento = DateTime.UtcNow
            };
            PublicarEvento(evento);
        }

        private void PublicarEvento<T>(T evento)
        {


            // Registrar o evento no log, capturando o nome do evento e os atributos principais
            var eventType = evento.GetType().Name;
            if (_logger != null) { 
                if (evento is CompraCriadaEvent compraCriada)
                {
                    _logger.LogInformation("Evento {EventType} publicado: VendaId = {VendaId}, ClienteId = {ClienteId}, ValorTotal = {ValorTotal}",
                        eventType, compraCriada.VendaId, compraCriada.ClienteId, compraCriada.ValorTotal);
                }
                else if (evento is CompraAlteradaEvent compraAlterada)
                {
                    _logger.LogInformation("Evento {EventType} publicado: VendaId = {VendaId}, NovoValorTotal = {NovoValorTotal}",
                        eventType, compraAlterada.VendaId, compraAlterada.NovoValorTotal);
                }
                else if (evento is CompraCanceladaEvent compraCancelada)
                {
                    _logger.LogInformation("Evento {EventType} publicado: VendaId = {VendaId}, DataCancelamento = {DataCancelamento}",
                        eventType, compraCancelada.VendaId, compraCancelada.DataCancelamento);
                }
                else if (evento is ItemCanceladoEvent itemCancelado)
                {
                    _logger.LogInformation("Evento {EventType} publicado: VendaId = {VendaId}, ProdutoId = {ProdutoId}, DataCancelamento = {DataCancelamento}",
                        eventType, itemCancelado.VendaId, itemCancelado.ProdutoId, itemCancelado.DataCancelamento);
                }
                else
                {
                    _logger.LogInformation("Evento {EventType} publicado", eventType);
                }
            }
        }

    }
}
