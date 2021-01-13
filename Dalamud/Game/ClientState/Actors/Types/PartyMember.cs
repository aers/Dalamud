using Dalamud.Game.ClientState.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dalamud.Game.ClientState.Actors.Types
{
    public class PartyMember
    {
        public string Name;
        public Structs.PartyMember PartyInfo;
        public Actor Actor;

        public PartyMember(ActorTable table, Structs.PartyMember rawData)
        {
            this.PartyInfo = rawData;
            unsafe
            {
                Name = Marshal.PtrToStringAnsi(new IntPtr(rawData.Name));
            }
            Actor = null;
            for (var i = 0; i < table.Length; i++)
            {
                if (table[i] != null && table[i].ActorId == rawData.ObjectID)
                {
                    Actor = table[i];
                    break;
                }
            }
        }
    }
}
