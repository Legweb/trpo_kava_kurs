using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kava
{
    public class Tables : MyCollection
    {
        public bool HaveEmptyTable()
        {
            bool isEmpty=false;
            for(int i=0;i<Collection.Count;i++)
            {
                if(Collection[i].Name=="Empty Table")
                {
                    isEmpty = true;
                    return isEmpty;
                }
            }
            return isEmpty;
        }
    }
}
