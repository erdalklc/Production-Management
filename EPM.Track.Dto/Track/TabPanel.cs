using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Dto.Track
{
    public class TabPanel
    {
        public TabPanel()
        {
        }
        public TabPanel(int _ID,string _NAME,string _DIV_NAME)
        {
            Id = _ID;
            Name = _NAME;
            Div_Name = _DIV_NAME;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Div_Name { get; set; }

        public List<TabPanel> GetList()
        {
            List<TabPanel> tabPanels = new List<TabPanel>();

            tabPanels.Add(new TabPanel(1, "ÖRME", "edOrmeTab"));
            tabPanels.Add(new TabPanel(2, "KUMAŞ DEPO", "edKumasDepoTab"));
            tabPanels.Add(new TabPanel(3, "KESİM", "edKesimTab"));
            tabPanels.Add(new TabPanel(4, "TASNİF", "edTasnifTab"));
            tabPanels.Add(new TabPanel(5, "BANT", "edBantTab"));
            tabPanels.Add(new TabPanel(6, "KALİTE", "edKaliteTab"));

            return tabPanels;
        }
    }
}
