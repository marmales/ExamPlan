namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Grade
    {
        public int? StudentID { get; set; }

        public int? SubjectID { get; set; }

        [Column("Grade")]
        public int? Grade1 { get; set; }

        public DateTime? EntryDate { get; set; }

        public int GradeID { get; set; }

        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
