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
    public partial class DryingRateCoated : ContentPage
    {
        public DryingRateCoated()
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
                label2.Text = "N: Number of steam-heated dryers";
                label3.Text = "A: Area of standard ream (m\u00B2)";
                label4.Text = "D: Diameter of dryer cylinders (m)";
                label5.Text = "L: Percent dryness (leaving last cyl.)";
                label6.Text = "Bc: Basis weight of sheet (kg/m\u00B2)";
                label7.Text = "W: Dry coating weight (kg/m\u00B2)";
                label8.Text = "P: Percent dryness (entering coater)";
                label9.Text = "C: Percent coating solids";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "S: Machine speed (ft/min)";
                label2.Text = "N: Number of steam-heated dryers";
                label3.Text = "A: Area of standard ream (ft\u00B2)";
                label4.Text = "D: Diameter of dryer cylinders (ft)";
                label5.Text = "L: Percent dryness (leaving last cylinder)";
                label6.Text = "Bc: Basis weight of sheet (lb/ream)";
                label7.Text = "W: Dry coating weight (lb/ream)";
                label8.Text = "P: Percent dryness (entering coater)";
                label9.Text = "C: Percent coating solids";
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
            if (string.IsNullOrWhiteSpace(S.Text) || string.IsNullOrWhiteSpace(N.Text) || string.IsNullOrWhiteSpace(A.Text) || string.IsNullOrWhiteSpace(D.Text) || string.IsNullOrWhiteSpace(L.Text) || string.IsNullOrWhiteSpace(Bc.Text)
                || string.IsNullOrWhiteSpace(W.Text) || string.IsNullOrWhiteSpace(P.Text) || string.IsNullOrWhiteSpace(C.Text))
            {
                label10.Text = "Please enter all values.";
            }
            else if (Double.TryParse(S.Text, out num) && Double.TryParse(N.Text, out num) && Double.TryParse(A.Text, out num) && Double.TryParse(D.Text, out num) && Double.TryParse(L.Text, out num) && Double.TryParse(Bc.Text, out num)
                && Double.TryParse(W.Text, out num) && Double.TryParse(P.Text, out num) && Double.TryParse(C.Text, out num))
            {
                var machineSpeed = double.Parse(S.Text, CultureInfo.InvariantCulture);
                var numDryers = double.Parse(N.Text, CultureInfo.InvariantCulture);
                var area = double.Parse(A.Text, CultureInfo.InvariantCulture);
                var diameter = double.Parse(D.Text, CultureInfo.InvariantCulture);
                var percentLeaving = double.Parse(L.Text, CultureInfo.InvariantCulture);
                var basisWeightEntering = double.Parse(Bc.Text, CultureInfo.InvariantCulture);
                var dryCoating = double.Parse(W.Text, CultureInfo.InvariantCulture);
                var basisPercent = double.Parse(P.Text, CultureInfo.InvariantCulture);
                var percentCoating = double.Parse(C.Text, CultureInfo.InvariantCulture);

                var top = (basisWeightEntering * (1.0 - (basisPercent / 100.0))) + (dryCoating * ((100.0 / percentCoating) - 1.0));
                var bottom = basisWeightEntering + ((100.0 * dryCoating) / percentCoating);

                var basisWeight = (basisWeightEntering * (basisPercent / 100.0) + dryCoating) / (percentLeaving / 100.0);
                var percentDrynessEntering = 100.0 - (100.0 * (top / bottom));
                var waterEvaporated = (percentLeaving / percentDrynessEntering) - 1.0;

                var dryingRate = 60.0 * ((machineSpeed * basisWeight * waterEvaporated) / (numDryers * area * Math.PI * diameter));

                if (!App.switched)
                {
                    label10.Text = "Drying rate: " + String.Format("{0:n4}", dryingRate) + " lb/h*ft\u00B2";
                }  
                else
                {
                    label10.Text = "Drying rate: " + String.Format("{0:n4}", dryingRate) + " kg/h*m\u00B2";
                }
            }
            else
            {
                label10.Text = "Please enter a valid number.";
            }
        }
    }
}