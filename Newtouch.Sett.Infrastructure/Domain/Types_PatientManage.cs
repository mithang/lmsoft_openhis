using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class BaseGaXnhS30Entity
    {
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public object Id { get; set; }
        public object OrganizeId { get; set; }
        public string datetime { get; set; }
        public string zt { get; set; }
    }

    public class GACardReadInfoDTO
    {
    }

    

    

    

    public class PatAccPayVO
    {
    }

    public class PatientListInputVO
    {
    }

    public class PendingExecutionPatientVO
    {
    }

    public class ReserveDto
    {
    }

    public class S03RequestDTO
    {
    }

    

    

    

    

    

    

    

    public class TreeViewModel
    {
        public string Ex1 { get; set; }
        public object checkstate { get; set; }
        public bool complete { get; set; }
        public bool hasChildren { get; set; }
        public string id { get; set; }
        public bool isexpand { get; set; }
        public string parentId { get; set; }
        public bool showcheck { get; set; }
        public string text { get; set; }
        public string Code { get; set; }
        public string img { get; set; }
        public string value { get; set; }
    }

    

    public class ZDSelect
    {
        public object icd10 { get; set; }
        public object py { get; set; }
        public object zdbh { get; set; }
        public object zdmc { get; set; }
        public object zdnm { get; set; }
    }

    public class ZYInfoVO
    {
        public object email { get; set; }
        public string hy { get; set; }
        public string lxrgx { get; set; }
        public string lxrgx2 { get; set; }
        public object patid { get; set; }
        public object phone { get; set; }
        public object webchat { get; set; }
    }

    public enum PatientBasicAndCardInfoVO
    {
        cardtype = 0,
        kh = 1,
    }

    public enum S04ReqDTO
    {
        admissionDate = 0,
        admissionDepartments = 1,
        inpatientNo = 2,
        memberId = 3,
    }

    public enum S04ResDto
    {
        message = 0,
        state = 1,
    }

    public enum S04entity
    {
        inpId = 0,
    }

    public enum S05ResDto
    {
        message = 0,
        state = 1,
    }

    public enum S06Res
    {
        message = 0,
        state = 1,
    }

    public enum S06entity
    {
        Id = 0,
        OrganizeId = 1,
        inpId = 2,
        xnhgrbm = 3,
        xnhylzh = 4,
        zyh = 5,
    }

    public enum VO
    {
        inpId = 0,
        xnhgrbm = 1,
        xnhylzh = 2,
        zyh = 3,
    }

}