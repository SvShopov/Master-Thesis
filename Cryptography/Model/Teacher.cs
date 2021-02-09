using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography.Model
{
    public class Teacher : User
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<TeacherCipher> Ciphers { get; set; }
    }
}
