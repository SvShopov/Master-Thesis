using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography.Model
{
    public abstract class Cipher
    {
        [Required, MaxLength(20)]
        public string ExactTime { get; set;}
        [Required, MaxLength(255)]
        public string MethodName { get; set; }
        [Required, MaxLength(1)]
        public char EncodeOrDecode { get; set; }
        [Required, MaxLength(255)]
        public string InputMessage { get; set; }
        [Required, MaxLength(255)]
        public string InputArgs { get; set; }
        [Required, MaxLength(255)]
        public string Result { get; set; }

        public Cipher()
        {

        }

    }
}
