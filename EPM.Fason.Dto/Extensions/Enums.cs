using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Dto.Extensions
{
    public class Enums
    {
        public enum LINESTATUS
        {
            WAITING=0,
            WAITINGFORSTART=1,
            STARTED=2,
            FINISHED=3
        }

        public enum HEADERSTATUS
        { 
            WAITINGFORSTART = 1,
            STARTED = 2,
            FINISHED = 3
        }
        public enum AQLHEADERSTATUS
        {
            FIRST_INIT=5,
            FINISHED=0,
            DELETED=1
            
        }
    }
}
