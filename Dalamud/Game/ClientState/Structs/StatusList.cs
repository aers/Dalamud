using System;
using System.Runtime.InteropServices;

namespace Dalamud.Game.ClientState.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 0x190)]
    public struct StatusList
    {
        [FieldOffset(0x0)] public IntPtr Owner; // this is often null - do not rely on it
        [FieldOffset(0x8)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)] public StatusEffect[] StatusEffects;
        [FieldOffset(0x170)] public uint Unk_170;
        [FieldOffset(0x174)] public ushort Unk_174;
        [FieldOffset(0x178)] public long Unk_178;
        [FieldOffset(0x180)] public byte Unk_180;
    }
}
