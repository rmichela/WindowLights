using System.Windows.Forms;

namespace WindowLights
{
    public partial class Form1 : Form
    {
        private Tlc5940 _tlc;
        private RgbLed _rgb1; 
        private RgbLed _rgb2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            _tlc = new Tlc5940("COM3", 1);
            _rgb1 = new RgbLed(_tlc, 1, 2, 3);
            _rgb2 = new RgbLed(_tlc, 4, 5, 6);

            _rgb1.R = 127;
            _rgb1.G = 127;
            _rgb1.B = 127;

            _rgb2.R = 127;
            _rgb2.G = 127;
            _rgb2.B = 127;

            _tlc.Update();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _tlc.Dispose();
        }

        private void trackBarRed_ValueChanged(object sender, System.EventArgs e)
        {
            _rgb1.R = (byte)trackBarRed.Value;
            _rgb2.R = (byte)(255 - trackBarRed.Value);
            _tlc.Update();
        }

        private void trackBarGreen_ValueChanged(object sender, System.EventArgs e)
        {
            _rgb1.G = (byte)trackBarGreen.Value;
            _rgb2.G = (byte)(255 - trackBarGreen.Value);
            _tlc.Update();
        }

        private void trackBarBlue_ValueChanged(object sender, System.EventArgs e)
        {
            _rgb1.B = (byte)trackBarBlue.Value;
            _rgb2.B = (byte)(255 - trackBarBlue.Value);
            _tlc.Update();
        }        
    }
}
