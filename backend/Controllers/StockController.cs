using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{   //Apsirašomas norimas Route
    [Route("api/stock")]
    [ApiController]
    public class StockController :ControllerBase
    {
        private readonly AppDBContext _context;
        public StockController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //sutvarkoma masyvo objektu forma pagal ToStockDto() 
            var stocks = await _context.Stocks.ToListAsync();
            var stockDto = stocks.Select(s=>s.ToStockDto());

            return Ok(stockDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Ieškomas įrašas pagal Id
            var stock = await _context.Stocks.FindAsync(id);

            if(stock == null)return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            // sukuriamas tinkamos formos stock įrašas
            var stockModel = stockDto.ToStockToCreateDto();
            // pridedama į duomenų bazę
            await _context.Stocks.AddAsync(stockModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById) , new {id = stockModel.Id} , stockModel.ToStockDto());
                                //panaudojamas GetById \ Paduodamas sukurtas  \ paduodama kokiu formatu 
                                // metodas                id GetById metofui      gražintu sukurta įrašą
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x=>x.Id == id);
            // Ieškoma ar egzistuoja

            if(stockModel==null)return NotFound();

            //Pakeiciami visi kintamieji
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;
            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());

        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
             var stockModel = await _context.Stocks.FirstOrDefaultAsync(x=>x.Id == id);
            // Ieškoma ar egzistuoja
            if(stockModel==null)return NotFound();

            _context.Stocks.Remove(stockModel);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}