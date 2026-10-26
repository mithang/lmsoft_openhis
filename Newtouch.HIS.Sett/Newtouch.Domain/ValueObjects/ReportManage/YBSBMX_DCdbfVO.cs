using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.ReportManage
{

	public class YBSBMX
	{

	}

	public class YBSBMX_DCdbfVO
	{
		public IList<ZGMJZ_Query> zgmjzvo { get; set; }
		public IList<ZGDBMZ_Qury> zgdbmzvo { get; set; }
		public IList<ZGZY_Query> zgzyvo { get; set; }
		public IList<ZGMJZTS_Query> zgmjztsvo { get; set; }
		public IList<JMMJZ_Query> jmmjzvo { get; set; }
		public IList<JMZY_Query> jmzyvo { get; set; }
		public IList<HZBK_Query> hzbkvo { get; set; }
		public IList<MZYD_Query> ydmzvo { get; set; }
		public IList<ZYYD_Query> ydzyvo { get; set; }

	}
	/// <summary>
	/// Ngoại trú/cấp cứu cán bộ công nhân viên 
	/// </summary>
	public class ZGMJZ_Query : YBSBMX
	{
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string JZDATE { get; set; }
		public string JZNUM { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJ { get; set; }
		public decimal GRZFD { get; set; }
		public decimal GRZFL { get; set; }
		public decimal FJBX { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY00 { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
		public decimal TCZF { get; set; }
	}

	/// <summary>
	/// Ngoại trú/cấp cứu đặc biệt cán bộ công nhân viên
	/// </summary>
	public class ZGMJZTS_Query : YBSBMX
	{
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string JZDATE { get; set; }
		public string JZNUM { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY11 { get; set; }
		public decimal FY00 { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
	}
	/// <summary>
	/// Ngoại trú bệnh nặng cán bộ công nhân viên
	/// </summary>
	public class ZGDBMZ_Qury : YBSBMX
	{
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string JZDATE { get; set; }
		public string JZNUM { get; set; }
		public string DBXM { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJ { get; set; }
		public decimal GRZFL { get; set; }
		public decimal FJBX { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY00 { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FLGRZ { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
		public decimal TCZF { get; set; }
	}
	/// <summary>
	/// Nội trú cán bộ công nhân viên Nội trú đặc biệt cán bộ công nhân viên
	/// </summary>
	public class ZGZY_Query : YBSBMX
	{
		public string ZYID { get; set; }
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string CYDATE { get; set; }
		public decimal ZYDNUM { get; set; }
		public string ZYJGBZ { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJD { get; set; }
		public decimal GRXJQ { get; set; }
		public decimal TCZF { get; set; }
		public decimal GRZFLD { get; set; }
		public decimal GRZFLQ { get; set; }
		public decimal FJBX { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FY11 { get; set; }
		public decimal FY12 { get; set; }
		public decimal FY13 { get; set; }
		public decimal FY14 { get; set; }
		public decimal FY15 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal FY31 { get; set; }
		public decimal FY32 { get; set; }
		public decimal FY33 { get; set; }
		public decimal FY34 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
	}
	/// <summary>
	/// Ngoại trú/cấp cứu cư dân
	/// </summary>
	public class JMMJZ_Query : YBSBMX
	{
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string JZDATE { get; set; }
		public string JZNUM { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJ { get; set; }
		public decimal YBZF { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY00 { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
	}
	/// <summary>
	/// Nội trú cư dân
	/// </summary>
	public class JMZY_Query : YBSBMX
	{
		public string ZYID { get; set; }
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string CYDATE { get; set; }
		public decimal ZYDNUM { get; set; }
		public string ZYJGBZ { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJD { get; set; }
		public decimal GRXJQ { get; set; }
		public decimal YBZF { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FY11 { get; set; }
		public decimal FY12 { get; set; }
		public decimal FY13 { get; set; }
		public decimal FY14 { get; set; }
		public decimal FY15 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal FY31 { get; set; }
		public decimal FY32 { get; set; }
		public decimal FY33 { get; set; }
		public decimal FY34 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
	}
	/// <summary>
	/// Giúp đỡ tương trợ
	/// </summary>
	public class HZBK_Query : YBSBMX
	{
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string JZDATE { get; set; }
		public string JZNUM { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJ { get; set; }
		public decimal GRZFD { get; set; }
		public decimal GRZFL { get; set; }
		public decimal YBZF { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY00 { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
	}
	/// <summary>
	/// Ngoại trú khác địa phương
	/// </summary>
	public class MZYD_Query : YBSBMX
	{
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string JZDATE { get; set; }
		public string JZNUM { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJ { get; set; }
		public decimal GRZFD { get; set; }
		public decimal GRZFL { get; set; }
		public decimal TCZF { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY00 { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
	}
	/// <summary>
	/// Nội trú khác địa phương
	/// </summary>
	public class ZYYD_Query : YBSBMX
	{
		public string ZYID { get; set; }
		public string NAME { get; set; }
		public string BXID { get; set; }
		public string YBID { get; set; }
		public string CYDATE { get; set; }
		public decimal ZYDNUM { get; set; }
		public string ZYJGBZ { get; set; }
		public string KSID { get; set; }
		public string KSNA { get; set; }
		public string GRXZ { get; set; }
		public decimal JYTOTFY { get; set; }
		public decimal GRXJD { get; set; }
		public decimal GRXJQ { get; set; }
		public decimal TCZF { get; set; }
		public decimal GRZFLD { get; set; }
		public decimal GRZFLQ { get; set; }
		public decimal FJBX { get; set; }
		public decimal TOTFY { get; set; }
		public decimal FY01 { get; set; }
		public decimal FY02 { get; set; }
		public decimal FY03 { get; set; }
		public decimal FY04 { get; set; }
		public decimal FY05 { get; set; }
		public decimal FY06 { get; set; }
		public decimal FY07 { get; set; }
		public decimal FY08 { get; set; }
		public decimal FY09 { get; set; }
		public decimal FY10 { get; set; }
		public decimal FY11 { get; set; }
		public decimal FY12 { get; set; }
		public decimal FY13 { get; set; }
		public decimal FY14 { get; set; }
		public decimal FY15 { get; set; }
		public decimal FLGRZF { get; set; }
		public decimal FY20 { get; set; }
		public decimal FY21 { get; set; }
		public decimal FY22 { get; set; }
		public decimal FY23 { get; set; }
		public decimal FY24 { get; set; }
		public decimal FY25 { get; set; }
		public decimal FY26 { get; set; }
		public decimal FY27 { get; set; }
		public decimal FY28 { get; set; }
		public decimal FY29 { get; set; }
		public decimal FY30 { get; set; }
		public decimal FY31 { get; set; }
		public decimal FY32 { get; set; }
		public decimal FY33 { get; set; }
		public decimal FY34 { get; set; }
		public decimal ZF { get; set; }
		public string ZDICD { get; set; }
		public string ZDNAME { get; set; }
		public string BJQK { get; set; }
		public string DAA { get; set; }
		public string YYID { get; set; }
		public string LSH { get; set; }
		public decimal GRZFG { get; set; }
	}
}
