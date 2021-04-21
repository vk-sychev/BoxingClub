using AutoMapper;
using BoxingClub.BLL.DTO;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.BoxingClub.BLL
{
    [TestFixture]
    class StudentServiceTests
    {
        private readonly Mapper _mapper;

        public StudentServiceTests(Mapper mapper)
        {
            _mapper = mapper;
        }

        [SetUp]
        public void SetUp()
        {

        }

        public void CreateStudent_InputIsStudentFullDTO_SuccefsullyAdd(StudentFullDTO studentDTO) //mock в setup, Сборка BoxingClub.BLL.UnitTest, сигнатуру метода изменить (ValidInput)
            //проверить, что метод репо дернулся
        {
            var mock = new Mock<IStudentRepository>();
            var student = _mapper.Map<Student>(studentDTO);
            mock.Setup(repo => repo.CreateAsync(student));
        }
    }
}
