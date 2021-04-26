
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkAPI.Repository
{
    public class BenchMarkRepo : IBenchMarckRepo
    {
        Dictionary<string,int> _internalandsoxdict = new Dictionary<string,int>();

        public BenchMarkRepo()
        {

        }

        public BenchMarkRepo(Dictionary<string,int> internalandsoxDict)
        {
            _internalandsoxdict = internalandsoxDict;
        }

        public Dictionary<string,int> GetInternaAndSOXNoCount(string auditType)
        {

            if (auditType == "Internal")
            {
                _internalandsoxdict.Add("Internal", 3);
            }
            else {
                if(auditType == "SOX")
                {
                    _internalandsoxdict.Add("SOX", 1);
                }
            }

            return _internalandsoxdict;
                

        }

       
    }
}
