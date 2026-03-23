using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DW.ELA.Plugin.EDSM
{
    public interface IEdsmApiFacade
    {
        Task PostLogEvents(JObject[] events);
    }
}