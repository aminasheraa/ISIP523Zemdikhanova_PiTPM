using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523Zemdikhanova_PiTPM
{
    internal class Core
    {
        public static PR14GordovEntities Context { get; } = new PR14GordovEntities();

        public static User CurrentUser { get; set; }
        public static Films Film { get; set; }
        public static Session Sessions { get; set; }

    }

}
