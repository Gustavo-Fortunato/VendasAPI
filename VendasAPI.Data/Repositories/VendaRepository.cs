using Microsoft.EntityFrameworkCore;
using System;
using VendasAPI.Data.Configuration;
using VendasAPI.Domain.Entities;


namespace VendasAPI.Domain.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly AppDbContext _context;

        public VendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Venda venda){}

        public IEnumerable<Venda> GetAll() => _context.Vendas.Include(v => v.ItemVenda).ToList();

        public Venda GetById(int id) => _context.Vendas.Include(v => v.ItemVenda).FirstOrDefault(v => v.VendaId == id);

        public void Update(Venda venda){}
    }
}
