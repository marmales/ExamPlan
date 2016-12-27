namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exam
    {
        public int ExamID { get; set; }

        public int SubjectID { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime? DateAndHour { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
