using flat_space.Lexor;
using flat_space.Memento;
using flat_space.QuadTree;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace flat_space
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;
        private bool running = false;
        private bool paused = false;
        private Thread UpdateThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Data files |*.csv;*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                vm = new FileReader(openFileDialog.FileName).readFromFile();
                this.DataContext = vm;
                this.UpdateThread = new Thread(update);
                UpdateThread.Start();
            }

            /* quad tree test */
            //Rectangle boundary = new Rectangle(800, 600, 800, 600);
            //var qt = new Tree(boundary);
            //foreach (var cell in vm.celestialObjects)
            //{
            //    qt.insert(cell);
            //}
            //Rectangle range = new Rectangle(200, 200, 200, 200);
            //List<celestialbody> planets = qt.query(range, new List<celestialbody>());
            /* end of quad tree test */
        }

        private void OpenUrl_Click(object sender, RoutedEventArgs e)
        {
            string httpUrl = GalaxyUrl.Text;
            vm = new FileReader(httpUrl).readFromFile();
            this.DataContext = vm;
            this.UpdateThread = new Thread(update);
            UpdateThread.Start();
        }
        

        private void StartStop(object sender, RoutedEventArgs e)
        {
            if(UpdateThread == null)
            {
                return;
            }
            if (!running)
            {
                //play
                running = true;
            }
            else
            {
                if (paused)
                {
                    paused = false;
                }
                else
                {
                    paused = true;
                }
            }
        }

        private void update()
        {
            while (true) {
                if (vm.paused)
                {
                    Thread.Sleep(vm.speed);
                    vm.updateModels();
                }
            }
        }

        private void Speedup(object sender, RoutedEventArgs e)
        {
            vm.Speedup();
        }

        private void Slowdown(object sender, RoutedEventArgs e)
        {
            vm.SlowDown();
        }

        private void BFS_click(object sender, RoutedEventArgs e)
        {
            vm.bfs();
        }

        private void Dijkstra_click(object sender, RoutedEventArgs e)
        {
            vm.dijkstra();
        }

        private void Disable_click(object sender, RoutedEventArgs e)
        {
            vm.disable();
        }
        private void quad_show(object sender, RoutedEventArgs e)
        {
            vm.activateRectangles();
        }

        private void quad_native_activate(object sender, RoutedEventArgs e)
        {
            vm.activateQuadNative();
        }

        private void quad_activate(object sender, RoutedEventArgs e)
        {
            vm.activateQuad();
        }

        private void add_body(object sender, RoutedEventArgs e)
        {
            Random rn = new Random();
            for (int i = 0; i < 1; i++)
            {
                vm.AddSpaceObject(new Astroid(rn.Next(10, 800), rn.Next(0, 600), 7, rn.Next(-5, 5), rn.Next(-5, 5), "red", "explode"));
            }
        }

        private void remove_body(object sender, RoutedEventArgs e)
        {
            var lastAdded = vm.celestialObjects.Last();
            if (lastAdded.GetType().Name == "Astroid")
            {
                vm.Remove(vm.celestialObjects.Last());
            }
        }

        private void back_in_time(object sender, RoutedEventArgs e)
        {
            vm.Restore(vm.CareTaker.Back());
        }

        private void forward_in_time(object sender, RoutedEventArgs e)
        {
            vm.Restore(vm.CareTaker.Forward());
        }
    }
}