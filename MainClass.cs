using System;
using System.Windows;
using System.Windows.Interop;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Security.Cryptography.X509Certificates;
using Autodesk.Revit.DB.Architecture;

namespace Finishing_Plugin
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class MainClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {

            Document doc = commandData.Application.ActiveUIDocument.Document;
            List<Level> levels = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType()
                .Cast<Level>()
                .ToList();


            MyWindow WindowOpen = new MyWindow(doc, levels);
            WindowInteropHelper wih = new WindowInteropHelper(WindowOpen);
            wih.Owner = commandData.Application.MainWindowHandle;

            WindowOpen.ShowDialog();



            return Result.Succeeded;
        }
    }
}
