using System.Collections.Generic;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Herp-local replacement for the FrameworkBase binary
    /// IBaseDataDmnService (no source, no Core equivalent was found).
    /// Only the two methods ProductSfxmController uses are declared.
    /// Shapes are fixed by the controller: SelectSfdl items carry dlCode /
    /// dlmc / ParentId (nullable int, controller does ParentId ?? 0);
    /// SelectSfxm items carry sfxmCode / sfxmmc (serialised as-is).
    /// </summary>
    public class SfdlVo
    {
        public string dlCode { get; set; }
        public string dlmc { get; set; }
        public int? ParentId { get; set; }
    }

    public class SfxmVo
    {
        public string sfxmCode { get; set; }
        public string sfxmmc { get; set; }
    }

    public interface IBaseDataDmnService
    {
        List<SfdlVo> SelectSfdl(string organizeId);
        List<SfxmVo> SelectSfxm(string sfdlCode, string organizeId);
    }
}
