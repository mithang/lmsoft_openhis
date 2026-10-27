using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public interface IJGTradUploadDmnService
    {
        object GetList(object p0, object p1, object p2, object p3, object p4, object p5);
        object GetListYp(object p0, object p1, object p2, object p3, object p4);
        object GetListsqsz(object p0, object p1, object p2, object p3, object p4, object p5);
        object Getmxlist(object p0, object p1, object p2);
        object JGUpload(object p0, object p1, object p2);
    }

    public interface IJGUploadDmnService
    {
        object GetList(object p0, object p1, object p2, object p3, object p4, object p5);
        object JGUpload(object p0, object p1);
    }
}
