﻿using Bingo.Application;
using Bingo.Infraestructura;
using Bingo.Infraestructura.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ConsoleUI
{
    class Program
    {

        static void Main(string[] args)
        {

            var email = new SendgridSender();

            var optionsInMemory = new DbContextOptionsBuilder<BingoContext>()
            .UseInMemoryDatabase("Bingo")
            .Options;

            using var db = new BingoContext(optionsInMemory);

            CrearCarton(db, email);

        }
        private static void CrearCarton(BingoContext context, SendgridSender emailsender)
        {
            CrearCartonService _service = new CrearCartonService(new UnitOfWork(context), emailsender);
            var requestCrear = new CrearCartonRequest() { JugadorID = "100010" };

            CrearCartonResponse responseCrear = _service.Ejecutar(requestCrear);

            Console.WriteLine(responseCrear.Mensaje);
        }
    }
}