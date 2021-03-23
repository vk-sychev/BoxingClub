using Autofac;
using BoxingClub.BLL.Interfaces;
using BoxingClub.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.WEB.Util
{
    public class StudentModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>().
                As<IStudentService>();
        }
    }
}
