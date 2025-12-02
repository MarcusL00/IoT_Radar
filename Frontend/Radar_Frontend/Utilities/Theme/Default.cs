using MudBlazor;

namespace Radar_Frontend.Utilities.Theme
{
    public class Default : MudTheme
    {

        public Default()
        {   
         PaletteLight = new()
            {
                Primary = "#2A2C24",
                Secondary = "#575A4B",
                Tertiary = "#816C61",


            };

            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "4px"
            };
        }
    }
}