using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperScience
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TissueCrepe : ContentPage
    {
        public TissueCrepe()
        {
            InitializeComponent();

            if (App.switched)
            {
                units.IsToggled = true;
            }
            else
            {
                units.IsToggled = false;
            }
        }

        /*
       * Switch changed
       */
        private void Units_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;

            if (isToggled)
            {
                s.Text = "SI";
                label1.Text = "Yankee speed (m/min)";
                label2.Text = "Reel speed (m/min)";
                App.switched = true;
                label3.IsVisible = false;
                label4.IsVisible = false;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Yankee speed (ft/min)";
                label2.Text = "Reel speed (ft/min)";
                App.switched = false;
                label3.IsVisible = false;
                label4.IsVisible = false;
                answer.IsVisible = false;
            }
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(ys.Text) || string.IsNullOrWhiteSpace(rs.Text))
            {
                label3.Text = "Please enter all values.";
                label3.IsVisible = true;
                label4.IsVisible = false;
            }
            else if (Double.TryParse(ys.Text, out num) && Double.TryParse(rs.Text, out num))
            {
                var yankeeSpeed = double.Parse(ys.Text, CultureInfo.InvariantCulture);
                var reelSpeed = double.Parse(rs.Text, CultureInfo.InvariantCulture);

                var crepe1 = ((yankeeSpeed - reelSpeed) / reelSpeed) * 100.0;
                var crepe2 = ((yankeeSpeed - reelSpeed) / yankeeSpeed) * 100.0;

                label3.Text = "Crepe (Method 1): " + String.Format("{0:n4}", crepe1) + "%";
                label3.IsVisible = true;
                label4.Text = "Crepe (Method 2): " + String.Format("{0:n4}", crepe2) + "%";
                label4.IsVisible = true;
            }
            else
            {
                label3.Text = "Please enter a valid number.";
                label3.IsVisible = true;
                label4.IsVisible = false;
            }
        }
    }
}




