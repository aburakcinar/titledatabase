using NarayaN.TitleDatabase.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;

namespace NarayaN.TitleDatabase.Server.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BSDummy : BaseService, IDummy
    {
        public bool Ping()
        {
            return true;
        }
    }
}