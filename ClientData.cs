using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_bot
{
    class ClientData
    {
        public int ClientId { get; private set; }
        
        public ClientData(int clientId)
        {
            ClientId = clientId;
        }

        enum StatusChoiceCity
        {
            NoCityChosen,
            WannaChoiceCity,
            ChooseCity

        }

        enum StatusChoiceTimeAlert
        {
            NoTimingChosen,
            ChooseTiming
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
