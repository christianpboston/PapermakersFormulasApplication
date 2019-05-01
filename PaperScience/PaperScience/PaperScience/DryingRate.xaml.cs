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
    public partial class DryingRate : ContentPage
    {
        public DryingRate()
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
                label1.Text = "S: Machine speed (m/min)";
                label2.Text = "B: Basis weight of sheet (kg/m\u00B2)";
                label3.Text = "N: Number of steam-heated dryers";
                label4.Text = "A: Area of standard ream (m\u00B2)";
                label5.Text = "D: Diameter of dryer cylinders (m)";
                label6.Text = "L: Percent dryness of sheet leaving";
                label7.Text = "E: Percent dryness of sheet entering";
                label8.Text = "Drying rate (kg/h*m\u00B2)";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "S: Machine speed (ft/min)";
                label2.Text = "B: Basis weight of sheet (lb/ream)";
                label3.Text = "N: Number of steam-heated dryers";
                label4.Text = "A: Area of standard ream (ft\u00B2)";
                label5.Text = "D: Diameter of dryer cylinders (ft)";
                label6.Text = "L: Percent dryness of sheet leaving";
                label7.Text = "E: Percent dryness of sheet entering";
                label8.Text = "Drying rate (lb/h*ft\u00B2)";
                s.Text = "US";
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
            if (string.IsNullOrWhiteSpace(S.Text) || string.IsNullOrWhiteSpace(B.Text) || string.IsNullOrWhiteSpace(N.Text) || string.IsNullOrWhiteSpace(A.Text) || string.IsNullOrWhiteSpace(D.Text)
                || string.IsNullOrWhiteSpace(L.Text) || string.IsNullOrWhiteSpace(E.Text))
            {
                label8.Text = "Please enter all values.";
            }
            else if (Double.TryParse(S.Text, out num) && Double.TryParse(B.Text, out num) && Double.TryParse(N.Text, out num) && Double.TryParse(A.Text, out num) && Double.TryParse(D.Text, out num)
                    && Double.TryParse(L.Text, out num) && Double.TryParse(E.Text, out num))
            {
                var machineSpeed = double.Parse(S.Text, CultureInfo.InvariantCulture);
                var basisWeight = double.Parse(B.Text, CultureInfo.InvariantCulture);
                var numDryers = double.Parse(N.Text, CultureInfo.InvariantCulture);
                var area = double.Parse(A.Text, CultureInfo.InvariantCulture);
                var diameter = double.Parse(D.Text, CultureInfo.InvariantCulture);
                var percentLeaving = double.Parse(L.Text, CultureInfo.InvariantCulture);
                var percentEntering = double.Parse(E.Text, CultureInfo.InvariantCulture);
                var waterEvaporated = (percentLeaving / percentEntering) - 1.0;

                var top = machineSpeed * basisWeight * waterEvaporated;
                var bottom = numDryers * area * Math.PI * diameter;

                var dryingRate = 60.0 * (top / bottom);
                if (!App.switched)
                {
                    label8.Text = "Drying rate: " + String.Format("{0:n4}", dryingRate) + " lb/h*ft\u00B2";
                }
                else
                {
                    label8.Text = "Drying rate: " + String.Format("{0:n4}", dryingRate) + " kg/h*m\u00B2";
                }
            }
            else
            {
                label8.Text = "Please enter a valid number.";
            }
        }
    }
}