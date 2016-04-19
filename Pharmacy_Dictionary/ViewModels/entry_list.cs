using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Dictionary
{
    class entry_list
    {
        public string id { get; set; }
        public string sound { get; set; }
        public string fl { get; set; }
        public string def { get; set; }
        public string suggestion { get; set; }

        public bool IsDataLoaded { get; set; }

        public void LoadData()
        {

            IsDataLoaded = true;
        }
    }
}
