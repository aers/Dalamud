using Dalamud.Game.ClientState.Actors.Types;
using Dalamud.Hooking;
using Dalamud.Plugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Dalamud.Game.ClientState
{
    public class PartyList : IReadOnlyCollection<PartyMember>, ICollection
    {
        private ClientStateAddressResolver Address { get; }
        private Dalamud dalamud;

        private delegate long PartyListGetDelegate(IntPtr groupManager, bool secondManager);

        private Hook<PartyListGetDelegate> partyListGetHook;
        private IntPtr groupManager;

        public PartyList(Dalamud dalamud, ClientStateAddressResolver addressResolver)
        {
            Address = addressResolver;
            this.dalamud = dalamud;
            this.groupManager = addressResolver.GroupManager;
            Log.Verbose("Group manager address {GroupManager}", Address.GroupManager);
        }

        public PartyMember this[int index]
        {
            get {
                if (index >= Length)
                    return null;
                var tblIndex = groupManager + index * 0x230;
                var memberStruct = Marshal.PtrToStructure<Structs.PartyMember>(tblIndex);
                return new PartyMember(this.dalamud.ClientState.Actors, memberStruct);
            }
        }

        public void CopyTo(Array array, int index)
        {
            for (var i = 0; i < Length; i++)
            {
                array.SetValue(this[i], index);
                index++;
            }
        }

        public IEnumerator<PartyMember> GetEnumerator() {
            for (var i = 0; i < Length; i++) {
                if (this[i] != null) {
                    yield return this[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Length => Marshal.ReadByte(groupManager + 0x3D5C);

        int IReadOnlyCollection<PartyMember>.Count => Length;

        public int Count => Length;

        public object SyncRoot => this;

        public bool IsSynchronized => false;
    }
}
