using DW.ELA.Plugin.EDDN.Model;
using System.Threading.Tasks;

namespace DW.ELA.Plugin.EDDN
{
    public interface IEddnApiFacade
    {
        Task PostEventAsync(EddnEvent events);
    }
}