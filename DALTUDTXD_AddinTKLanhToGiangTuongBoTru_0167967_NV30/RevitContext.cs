using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30
{
    public static class RevitContext
    {
        public static UIApplication? UIApp { get; set; }
        public static Document? Doc { get; set; }
    }
}