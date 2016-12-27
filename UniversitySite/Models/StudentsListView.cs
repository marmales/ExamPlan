using System;
using System.Collections.Generic;
using Domain.Entities;
namespace UniversitySite.Models
{
    public class StudentsListView
    {
        public IEnumerable<Student> Students { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}