﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Interfaces;
using BoxingClub.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BoxingClub.BLL.Infrascructure
{
    public class ServiceModule : Module
    {
        private DbContextOptions<BoxingClubContext> _options;

        public ServiceModule(DbContextOptions<BoxingClubContext> options)
        {
            this._options = options;
        }


        //public ServiceModule(BoxingClubContext context)
        //{
        //    _context = context;
        //}

        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(u => new EFUnitOfWork(_context)).As<IUnitOfWork>();
            builder.RegisterType<EFUnitOfWork>()
                .As<IUnitOfWork>()
                .WithParameter("options", new BoxingClubContext(_options));
                
        }
    }
}
