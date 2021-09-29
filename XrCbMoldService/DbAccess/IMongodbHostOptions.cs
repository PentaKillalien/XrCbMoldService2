using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrCbMoldService.DbAccess
{
   public interface IMongodbHostOptions
    {
        string Connection { get; set; }
        string DataBase { get; set; }
        string Table { get; set; }
    }
}
