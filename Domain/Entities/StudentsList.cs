namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentsList")]
    public partial class StudentsList
    {
        public int? StudentID { get; set; }

        public int? ClassID { get; set; }

        [Key]
        public int ListID { get; set; }

        public virtual Class Class { get; set; }

        public virtual Student Student { get; set; }
    }
}
