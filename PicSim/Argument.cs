using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {

  [StructLayout(LayoutKind.Explicit)]
  struct Argument {

    [FieldOffset(0)]
    public byte byte1;

    [FieldOffset(0)]
    public bool bool1;

    [FieldOffset(1)]
    public byte byte2;
  }
}
