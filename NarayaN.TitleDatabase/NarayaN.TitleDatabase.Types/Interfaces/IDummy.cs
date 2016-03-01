using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NarayaN.TitleDatabase.Types.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IDummy
    {
        [OperationContract]
        //[FaultContract(typeof(KIK.Ortak.VeriTipleri.KikException))]
        bool Ping();
    }
}
