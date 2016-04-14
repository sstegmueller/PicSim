using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim {
	enum OperationType {
		ByteOrientedFD = 1,
		ByteOrientedF = 2,
		BitOriented = 4,
		LiteralControl = 8,
		NoArgs = 16
	}
}
