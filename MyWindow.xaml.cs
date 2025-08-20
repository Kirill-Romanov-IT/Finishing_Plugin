using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

namespace Finishing_Plugin
{
    /// <summary>
    /// Interaction logic for MyWindow.xaml
    /// </summary>
    public partial class MyWindow : Window
    {
        private List<Level> allLevels;
        private Document doc;

        public MyWindow(Document doc, List<Level> levels)
        {
            InitializeComponent();
            this.allLevels = levels;
            this.doc = doc;
            foreach (Level level in levels)
            {
                LevelsCombox.Items.Add(level.Name);
            }
            
        }

        private void LevelsCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedLevelName = LevelsCombox.SelectedItem as string;

            if (selectedLevelName != null) { 
                Level foundLevel = allLevels.FirstOrDefault(level => level.Name == selectedLevelName);
                if (foundLevel != null){
                    ElementId foundLevelId = foundLevel.Id;
                    List<Room> roomsOnLevel = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_Rooms)
                    .WherePasses(new ElementLevelFilter(foundLevelId))
                    .WhereElementIsNotElementType()
                    .Cast<Room>()
                    .ToList();
                    RoomsListBox.Items.Clear();
                    foreach(Room room in roomsOnLevel)
                    {
                        string infoAdd = room.Name;
                        string infoRemove = room.Number;

                        string infoRemoveResult = infoAdd.Replace(infoRemove, "");
                      
                        RoomsListBox.Items.Add(infoRemoveResult);


                    }
                }
            }
        }

        private void RoomsCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

