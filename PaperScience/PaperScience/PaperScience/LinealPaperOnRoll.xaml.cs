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
    public partial class LinealPaperOnRoll : ContentPage
    {
        public LinealPaperOnRoll()
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
                label1.Text = "Outer diameter (m)";
                label2.Text = "Inner diameter (m)";
                label3.Text = "Caliper (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Outer diameter (in.)";
                label2.Text = "Inner diameter (in.)";
                label3.Text = "Caliper (in.)";
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
            if (string.IsNullOrWhiteSpace(OD.Text) || string.IsNullOrWhiteSpace(ID.Text) || string.IsNullOrWhiteSpace(c.Text))
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(OD.Text, out num) && Double.TryParse(ID.Text, out num) && Double.TryParse(c.Text, out num))
            {
                var outerDiameter = double.Parse(OD.Text, CultureInfo.InvariantCulture);
                var innerDiameter = double.Parse(ID.Text, CultureInfo.InvariantCulture);
                var caliper = double.Parse(c.Text, CultureInfo.InvariantCulture);

                var top = Math.PI * (Math.Pow(outerDiameter, 2) - Math.Pow(innerDiameter, 2));

                if (!App.switched)
                {
                    var l = top / (48.0 * caliper);
                    label4.Text = "Lineal paper on roll: " + String.Format("{0:n4}", l) + " ft";
                }
                else
                {
                    var l = top / (4.0 * caliper);
                    label4.Text = "Lineal paper on roll: " + String.Format("{0:n4}", l) + " m";
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}