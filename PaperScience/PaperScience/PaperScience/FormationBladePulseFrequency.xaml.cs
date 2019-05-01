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
    public partial class FormationBladePulseFrequency : ContentPage
    {
        public FormationBladePulseFrequency()
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
                label1.Text = "Wire speed (m/min)";
                label2.Text = "Blade spacing (m)";
                label3.Text = "Frequency (cycles/sec)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Wire speed (ft/min)";
                label2.Text = "Blade spacing (in.)";
                label3.Text = "Frequency (cycles/sec)";
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
            if (string.IsNullOrWhiteSpace(V.Text) || string.IsNullOrWhiteSpace(spacing.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(V.Text, out num) && Double.TryParse(spacing.Text, out num))
            {
                var speed = double.Parse(V.Text, CultureInfo.InvariantCulture);
                var bladeSpacing = double.Parse(spacing.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var f = speed / (5.0 * bladeSpacing);
                    label3.Text = "Frequency: " + String.Format("{0:n4}", f) + " cycles/sec";
                }
                else
                {
                    var f = speed / (60.0 * bladeSpacing);
                    label3.Text = "Frequency: " + String.Format("{0:n4}", f) + " cycles/sec";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}