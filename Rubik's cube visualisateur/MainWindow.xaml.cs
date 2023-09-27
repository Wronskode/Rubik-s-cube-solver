using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rubik_s_cube_visualisateur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //AxisAngleRotation3D ax3d;
        public MainWindow()
        {
            InitializeComponent();
            //ax3d = new AxisAngleRotation3D(new Vector3D(0, 2, 0), 1);
            //RotateTransform3D myRotateTransform = new RotateTransform3D(ax3d);
            //MyModel.Transform = myRotateTransform;
        }

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    ax3d.Angle += 1;
        //}
    }
}
