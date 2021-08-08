using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_bot
{
    class ClientData
    {
        public long Id { get; private set; }
        public int statusChoiceCity ;
        public bool statusChoiceTime;
        public ClientData(long clientId)
        {
           Id = clientId;
        }

    }

}
