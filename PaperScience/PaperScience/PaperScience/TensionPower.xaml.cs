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
    public partial class TensionPower : ContentPage
    {
        public TensionPower()
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
                label1.Text = "Speed (m/min)";
                label2.Text = "Tension (kN/m)";
                label3.Text = "Width (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Speed (ft/min)";
                label2.Text = "Tension (lbf/in.)";
                label3.Text = "Width (in.)";
                App.switched = false;
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
            if (string.IsNullOrWhiteSpace(N.Text) || string.IsNullOrWhiteSpace(F.Text) || string.IsNullOrWhiteSpace(w.Text))
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(N.Text, out num) && Double.TryParse(F.Text, out num) && Double.TryParse(w.Text, out num))
            {
                var speed = double.Parse(N.Text, CultureInfo.InvariantCulture);
                var tension = double.Parse(F.Text, CultureInfo.InvariantCulture);
                var width = double.Parse(w.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var HP = (speed * tension * width) / 33000.0;
                    label4.Text = "Tension (HP): " + String.Format("{0:n4}", HP) + " HP";
                }
                else
                {
                    var P = (speed * tension * width) / 60.0;
                    label4.Text = "Tension (P): " + String.Format("{0:n4}", P) + " kW";
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}