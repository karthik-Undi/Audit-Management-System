
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkAPI.Repository
{
    public interface IBenchMarckRepo
    { 
        
        Dictionary<string,int> GetInternaAndSOXNoCount(string auditType);
        


    }
    
}
