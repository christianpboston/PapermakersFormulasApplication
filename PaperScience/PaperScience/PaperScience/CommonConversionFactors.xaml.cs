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
    public partial class CommonConversionFactors : ContentPage
    {
        public CommonConversionFactors()
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
        * Hides additional answer labels
        */
        private void hideLabels()
        {
            answer1Frame.IsVisible = false;
            answer2Frame.IsVisible = false;
            answer3Frame.IsVisible = false;
            answer4Frame.IsVisible = false;
            answer5Frame.IsVisible = false;
            answer6Frame.IsVisible = false;
            answer7Frame.IsVisible = false;
            answer8Frame.IsVisible = false;
            answer9Frame.IsVisible = false;
        }

        /*
        * Switch changed
        */
        private void Units_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;

            if (isToggled)
            {
                label7.Text = "g/m\u00B2 to lb/3000ft\u00B2";
                label8.Text = "°C to °F";
                label9.Text = "kN/m to lbf/in.";
                label10.Text = "kg/cm to lbf/in.";
                label11.Text = "Metric ton to short ton";
                s.Text = "SI";
                App.switched = true;
                hideLabels();
            }
            else
            {
                label7.Text = "lb/3000ft\u00B2 to g/m\u00B2";
                label8.Text = "°F to °C";
                label9.Text = "lbf/in. to kN/m";
                label10.Text = "lbf/in. to kg/cm";
                label11.Text = "Short ton to metric ton";
                s.Text = "US";
                App.switched = false;
                hideLabels();
            }
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            answer1Frame.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(entry1.Text))
            {
                answer11.Text = "Please enter a value.";
            }
            else
            {
                if (!Double.TryParse(entry1.Text, out num))
                {
                    answer11.Text = "Please enter a valid number.";
                }
                else
                {
                    var HP = double.Parse(entry1.Text, CultureInfo.InvariantCulture);
                    var conversionMin = HP * 33000.0;
                    var conversionSec = HP * 550.0;
                    answer11.Text = HP + "HP = " + String.Format("{0:n4}", conversionMin) + " ft*lbf/min = " + String.Format("{0:n4}", conversionSec) + " ft*lbf/sec";

                    var w = HP * 746.0;
                    answer12.Text = HP + "HP = " + String.Format("{0:n4}", w) + " W";
                    answer12.IsVisible = true;

                    var BTU = HP * 42.4;
                    answer13.Text = HP + "HP = " + String.Format("{0:n4}", BTU) + " BTU/min";
                    answer13.IsVisible = true;
                }
            }
        }

        private void Button_Clicked3(object sender, EventArgs e)
        {
            answer2Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry2.Text) && !string.IsNullOrWhiteSpace(entry3.Text) && !string.IsNullOrWhiteSpace(entry4.Text))
            {
                if (!Double.TryParse(entry2.Text, out num) || !Double.TryParse(entry3.Text, out num) || !Double.TryParse(entry4.Text, out num))
                {
                    answer2.Text = "Please enter a valid number.";
                }
                else
                {
                    var amps = double.Parse(entry2.Text, CultureInfo.InvariantCulture);
                    var volts = double.Parse(entry3.Text, CultureInfo.InvariantCulture);
                    var dePercent = double.Parse(entry4.Text, CultureInfo.InvariantCulture);
                    var efficiency = dePercent / 100.0;
                    var electricHorsepower = amps / (746.0 / (volts * efficiency));
                    answer2.Text = "ElectricHP: " + String.Format("{0:n4}", electricHorsepower);
                }
            }
            else
            {
                answer2.Text = "Please enter all values.";
            }
        }

        private void Button_Clicked4(object sender, EventArgs e)
        {
            answer3Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry5.Text))
            {
                if (!Double.TryParse(entry5.Text, out num))
                {
                    answer3.Text = "Please enter a valid number.";
                }
                else
                {
                    var dens = double.Parse(entry5.Text, CultureInfo.InvariantCulture);

                    var conversionDensity = dens / 7.481;
                    answer3.Text = dens + " lb/ft\u00B2 = " + String.Format("{0:n4}", conversionDensity) + " lb/gal";
                }
            }
            else
            {
                answer3.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked5(object sender, EventArgs e)
        {
            answer3FrameReverse.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry5Reverse.Text))
            {
                if (!Double.TryParse(entry5Reverse.Text, out num))
                {
                    answer3Reverse.Text = "Please enter a valid number.";
                }
                else
                {
                    var gallons = double.Parse(entry5Reverse.Text, CultureInfo.InvariantCulture);
                    var conversionGallons = gallons * 7.481;
                    answer3Reverse.Text = gallons + " lb/gal = " + String.Format("{0:n4}", conversionGallons) + " lb/ft\u00B2";
                }
            }
            else
            {
                answer3Reverse.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked6(object sender, EventArgs e)
        {
            answer4Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry6.Text))
            {
                if (!Double.TryParse(entry6.Text, out num))
                {
                    answer4.Text = "Please enter a valid number.";
                }
                else
                {
                    var gallons = double.Parse(entry6.Text, CultureInfo.InvariantCulture);
                    var gallonsToLiters = gallons * 4.546;
                    answer4.Text = gallons + " imp. gallons = " + String.Format("{0:n4}", gallonsToLiters) + " liters";
                }
            }
            else
            {
                answer4.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked7(object sender, EventArgs e)
        {
            answer4FrameReverse.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry6Reverse.Text))
            {
                if (!Double.TryParse(entry6Reverse.Text, out num))
                {
                    answer4Reverse.Text = "Please enter a valid number.";
                }
                else
                {
                    var liters = double.Parse(entry6Reverse.Text, CultureInfo.InvariantCulture);
                    var litersToGallons = liters / 4.546;
                    answer4Reverse.Text = liters + " liters = " + String.Format("{0:n4}", litersToGallons) + " imp. gallons";
                }
            }
            else
            {
                answer4Reverse.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked8(object sender, EventArgs e)
        {
            answer5Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry7.Text))
            {
                if (!Double.TryParse(entry7.Text, out num))
                {
                    answer5.Text = "Please enter a valid number.";
                }
                else
                {
                    if (App.switched)
                    {
                        var gramsPerMeterSquared = double.Parse(entry7.Text, CultureInfo.InvariantCulture);
                        var gramsToLbs = gramsPerMeterSquared * 0.614448431;
                        answer5.Text = gramsPerMeterSquared + " g/m\u00B2 = " + String.Format("{0:n4}", gramsToLbs) + " lb/3000ft\u00B2";
                    }
                    else
                    {
                        var poundsPerThreeThousandFeetSquared = double.Parse(entry7.Text, CultureInfo.InvariantCulture);
                        var lbsToGrams = poundsPerThreeThousandFeetSquared * 1.627;
                        answer5.Text = poundsPerThreeThousandFeetSquared + " lb/3000ft\u00B2 = " + String.Format("{0:n4}", lbsToGrams) + " g/m\u00B2";
                    }
                }
            }
            else
            {
                answer5.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked9(object sender, EventArgs e)
        {
            answer6Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry8.Text))
            {
                if (!Double.TryParse(entry8.Text, out num))
                {
                    answer6.Text = "Please enter a valid number.";
                }
                else
                {
                    if (App.switched)
                    {
                        var celcius = double.Parse(entry8.Text, CultureInfo.InvariantCulture);
                        var celciusToFahrenheit = (9.0 / 5.0) * celcius + 32.0;
                        answer6.Text = celcius + " °C = " + String.Format("{0:n0}", celciusToFahrenheit) + " °F";
                    }
                    else
                    {
                        var fahrenheit = double.Parse(entry8.Text, CultureInfo.InvariantCulture);
                        var FahrenheitToCelcius = (fahrenheit - 32.0) * (5.0 / 9.0);
                        answer6.Text = fahrenheit + " °F = " + String.Format("{0:n0}", FahrenheitToCelcius) + " °C";
                    }
                }
            }
            else
            {
                answer6.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked10(object sender, EventArgs e)
        {
            answer7Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry9.Text))
            {
                if (!Double.TryParse(entry9.Text, out num))
                {
                    answer7.Text = "Please enter a valid number.";
                }
                else
                {
                    if (App.switched)
                    {
                        var entry = double.Parse(entry9.Text, CultureInfo.InvariantCulture);
                        var conversion = entry * 5.71;
                        answer7.Text = entry + " kN/m = " + String.Format("{0:n4}", conversion) + " lbf/in.";
                    }
                    else
                    {
                        var entry = double.Parse(entry9.Text, CultureInfo.InvariantCulture);
                        var conversion = entry / 5.71;
                        answer7.Text = entry + " lbf/in. = " + String.Format("{0:n4}", conversion) + " kN/m";
                    }
                }
            }
            else
            {
                answer7.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked11(object sender, EventArgs e)
        {
            answer8Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry10.Text))
            {
                if (!Double.TryParse(entry10.Text, out num))
                {
                    answer8.Text = "Please enter a valid number.";
                }
                else
                {
                    if (App.switched)
                    {
                        var entry = double.Parse(entry10.Text, CultureInfo.InvariantCulture);
                        var conversion = entry * 5.6;
                        answer8.Text = entry + " kg/cm = " + String.Format("{0:n4}", conversion) + " lbf/in.";
                    }
                    else
                    {
                        var entry = double.Parse(entry10.Text, CultureInfo.InvariantCulture);
                        var conversion = entry / 5.6;
                        answer8.Text = entry + " lbf/in. = " + String.Format("{0:n4}", conversion) + " kg/cm";
                    }
                }
            }
            else
            {
                answer8.Text = "Please enter a value.";
            }
        }

        private void Button_Clicked12(object sender, EventArgs e)
        {
            answer9Frame.IsVisible = true;
            double num;
            if (!string.IsNullOrWhiteSpace(entry11.Text))
            {
                if (!Double.TryParse(entry11.Text, out num))
                {
                    answer9.Text = "Please enter a valid number.";
                }
                else
                {
                    if (App.switched)
                    {
                        var entry = double.Parse(entry11.Text, CultureInfo.InvariantCulture);
                        var conversion = entry * 1.102;
                        answer9.Text = entry + " t = " + String.Format("{0:n4}", conversion) + " tons";
                    }
                    else
                    {
                        var entry = double.Parse(entry11.Text, CultureInfo.InvariantCulture);
                        var conversion = entry / 1.102;
                        answer9.Text = entry + " tons = " + String.Format("{0:n4}", conversion) + " t";
                    }
                }
            }
            else
            {
                answer9.Text = "Please enter a value.";
            }
        }
    }
}

