using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Outpatient
{
    [SugarTable("mz_zsys")]
    public class OutpatientConsultDoctorEntity
    {
        // QA-phase2 (nhom B, muc 3): cot Id cua mz_zsys la IDENTITY (da kiem chung
        // COLUMNPROPERTY IsIdentity=1) nhung entity chi co [Key] nen SqlSugar INSERT
        // kem Id tuong minh va loi "Cannot insert explicit value for identity
        // column", chan ca SaveConsultDoctor (SubmitForm chay truoc UpdateZS). Giu
        // [Key] (RepositoryBase.GetKeyProperty chi nhan [Key]) va them IsIdentity.
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public string gh { get; set; }
        public string zsCode { get; set; }
        public DateTime rq { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public int px { get; set; }
    }
}
