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
    public partial class FlowsTonsConsistencyRelationship : ContentPage
    {
        public FlowsTonsConsistencyRelationship()
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
                label1.Text = "Consistency (%)";
                label2.Text = "Flow (l/min)";
                label3.Text = "Temperature factor (°C)";
                label4.Text = "t/d";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Consistency (%)";
                label2.Text = "Flow (gal/min)";
                label3.Text = "Temperature factor (°F)";
                label4.Text = "Ton/d";
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
            if (string.IsNullOrWhiteSpace(C.Text) || string.IsNullOrWhiteSpace(Q.Text) || string.IsNullOrWhiteSpace(K.Text))
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(C.Text, out num) && Double.TryParse(Q.Text, out num) && Double.TryParse(K.Text, out num))
            {
                var consistency = double.Parse(C.Text, CultureInfo.InvariantCulture);
                var flow = double.Parse(Q.Text, CultureInfo.InvariantCulture);
                var factor = double.Parse(K.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var FTCrelationship = consistency * flow / factor;
                    label4.Text = String.Format("{0:n4}", FTCrelationship) + " Ton/d";
                }
                else
                {
                    var FTCrelationship = consistency * flow * 4.1727 / factor;
                    label4.Text = String.Format("{0:n4}", FTCrelationship) + " t/d";
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}