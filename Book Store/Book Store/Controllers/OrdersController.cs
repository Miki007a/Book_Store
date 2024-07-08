using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book_Store.Models.Models;
using Book_Store.Repository;
using Book_Store.Service.Implementation;
using Book_Store.Service.Interface;
using Newtonsoft.Json;
using System.Text;
using GemBox.Document;
using ClosedXML.Excel;

namespace Book_Store.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderservice)
        {
            _orderService = orderservice;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderService.GetDetailsForOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public FileContentResult CreateInvoice(int Id)
        {


            var data = _orderService.GetDetailsForOrder(Id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            document.Content.Replace("{{UserName}}", data.BookUser.UserName);
            StringBuilder sb = new StringBuilder();
            var total = 0;
            foreach (var item in data.BooksInOrders)
            {
                sb.Append("Book " + item.Book.Title + " with quantity " + item.Quantity + " with price " + item.Book.Price + "$, ");
                total += (item.Quantity * item.Book.Price);
            }
            var listproducts=sb.ToString().Remove(sb.Length - 2);
            document.Content.Replace("{{BookList}}", listproducts);
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "$");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");

        }

        [HttpGet]
        public FileContentResult ExportOrders()
        {
            string fileName = "AllOrders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Orders");

                worksheet.Cell(1, 1).Value = "Order ID";
                worksheet.Cell(1, 2).Value = "Customer Username";

               

                var data = _orderService.GetAllOrders();

                for (int i = 0; i < data.Count(); i++)
                {
                    var order = data[i];
                    worksheet.Cell(i + 2, 1).Value = order.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = order.BookUser.UserName;

                    for (int j = 0; j < order.BooksInOrders.Count(); j++)
                    {
                        worksheet.Cell(1, j + 3).Value = "Book - " + (j + 1);
                        worksheet.Cell(i + 2, j + 3).Value = order.BooksInOrders.ElementAt(j).Book.Title;

                    }
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }

        }

    }
}