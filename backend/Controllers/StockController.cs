using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            //sutvarkoma masyvo objektu forma pagal ToStockDto() 
            var stocks = _context.Stocks.ToList()
            .Select(s=>s.ToStockDto());

            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Ieškomas įrašas pagal Id
            var stock = _context.Stocks.Find(id);

            if(stock == null)return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            // sukuriamas tinkamos formos stock įrašas
            var stockModel = stockDto.ToStockToCreateDto();
            // pridedama į duomenų bazę
            _context.Stocks.Add(stockModel);

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById) , new {id = stockModel.Id} , stockModel.ToStockDto());
                                //panaudojamas GetById \ Paduodamas sukurtas  \ paduodama kokiu formatu 
                                // metodas                id GetById metofui      gražintu sukurta įrašą
        }

        [HttpPut("{id}")]
        
        public IActionResult Update([FromRoute] int id , [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x=>x.Id == id);
            // Ieškoma ar egzistuoja

            if(stockModel==null)return NotFound();

            //Pakeiciami visi kintamieji
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;
            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());

        }
        [HttpDelete("{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
             var stockModel = _context.Stocks.FirstOrDefault(x=>x.Id == id);
            // Ieškoma ar egzistuoja
            if(stockModel==null)return NotFound();

            _context.Stocks.Remove(stockModel);
            
            _context.SaveChanges();

            return NoContent();
        }
    }
}