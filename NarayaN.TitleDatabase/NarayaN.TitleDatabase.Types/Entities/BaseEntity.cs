using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Types.Entities
{
    public abstract class BaseEntity
    {

        public DateTime? IslemTarihSaat { get; set; }

        [StringLength(11)]
        public string IslemKullaniciId { get; set; }

        [StringLength(50)]
        public string IslemSessionId { get; set; }

        //[Timestamp]
        //public byte[] ZamanDamgasi { get; set; }
    }
}
