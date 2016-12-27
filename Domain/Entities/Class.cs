namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            StudentsLists = new HashSet<StudentsList>();
        }

        public int ClassID { get; set; }

        public int DayID { get; set; }

        public TimeSpan BeginTime { get; set; }

        public TimeSpan ClassesLength { get; set; }

        public int Classroom { get; set; }

        public int LecturelID { get; set; }

        public int SubjectID { get; set; }

        public virtual Day DayOfWeek { get; set; }

        public virtual Lecturel Lecturel { get; set; }

        public virtual Subject Subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentsList> StudentsLists { get; set; }
    }
}
