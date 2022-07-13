using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assist.July._2022.BE2.Domain
{
    public enum gender1 : byte
    {
        male = 0,
        female = 1
    }
    public enum role : byte
    {
        admin = 0,
        validator = 1,
        user = 10
    }
}