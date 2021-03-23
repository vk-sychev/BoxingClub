using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using BoxingClub.DAL.EF;
using BoxingClub.DAL.Interfaces;
using BoxingClub.DAL.Repositories;

namespace BoxingClub.BLL.Infrascructure
{
    class ServiceModule : Module
    {
        private BoxingClubContext _context;
        
        public ServiceModule(BoxingClubContext context)
        {
            _context = context;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(u => new EFUnitOfWork(_context)).As<IUnitOfWork>();
        }
    }
}
