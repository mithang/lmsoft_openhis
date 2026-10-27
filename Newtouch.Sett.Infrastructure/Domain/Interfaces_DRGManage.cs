using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IDRGTradUploadDmnService
    {
        object DRGUpload(object p0, object p1, object p2);
        object GetList(object p0, object p1, object p2, object p3, object p4, object p5);
        object GetListYp(object p0, object p1, object p2, object p3, object p4);
        object GetListsqsz(object p0, object p1, object p2, object p3, object p4, object p5);
        object Getmxlist(object p0, object p1, object p2);
    }

    public interface IDRGUploadDmnService
    {
        object DRGUpload(object p0, object p1);
        object GetList(object p0, object p1, object p2, object p3, object p4, object p5);
    }
}
