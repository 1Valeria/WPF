using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Globalization;
using System.Windows.Media.Animation;

namespace lab02v3._0
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer music = new MediaPlayer();
        private MediaPlayer click = new MediaPlayer();

        Language lng = new Language("English");
        Country cn1 = new Country("United States of America");
        Country cn2 = new Country("United Kingdom");
        Series s1 = new Series("Vampire Diaries");
        Series s2 = new Series("Castle");
        Series s3 = new Series("Harry Potter");
        Series s4 = new Series("Avatar: The Legend of Korra");
        
        char ch = 'n';

        public MainWindow()
        {
            InitializeComponent();

            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;

            //заполняем меню смены языка
            menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }

            var rw = new ResXResourceWriter("Demo.resx");

            music.Volume = 1;
            music.Open(new Uri("Source/music3.mp3", UriKind.Relative));
            music.Play();

            System.Windows.Controls.Image img1 = new System.Windows.Controls.Image();

            DoubleAnimation ta = new DoubleAnimation();
            ta.From = 0;
            ta.To = 100;
            ta.Duration = new Duration(TimeSpan.FromSeconds(1));
           


            s1.Add_Couple("Elena Gilbert", "Damon Salvatore");
            s1.Add_Couple("Caroline Forbes", "Stefan Salvatore");
            s1.Add_Couple("Jo Robles", "Alaric Zaltsman");
            s1.Add_Couple("Bonnie Bennet", "Jeremy Gilbert");

            s2.Add_Couple("Katherine Beckett", "Richard Castle");
            s2.Add_Couple("Lanie Parish", "Javier Esposito");
            s2.Add_Couple("Jenny O'Malley", "Kevin Ryan");

            s3.Add_Couple("Ginny Weasley", "Harry Potter");
            s3.Add_Couple("Hermione Granger", "Ron Weasley");
            s3.Add_Couple("Fleur Delacour", "Bill Weasley");

            s4.Add_Couple("Asami Sato", "Avatar Korra");
            s4.Add_Couple("Jinora", "Kai");
            s4.Add_Couple("Opal", "Bolin");

            cn1.Add_Series(s1);
            cn1.Add_Series(s2);
            cn1.Add_Series(s4);
            cn2.Add_Series(s3);

            lng.Add_Country(cn1);
            lng.Add_Country(cn2);
            textBox.Clear();

            var binding = new Binding();
            binding.ElementName = "slider";
            binding.Path = new PropertyPath("Value");
            // устанавливаем привязку
            text.SetBinding(TextBlock.FontSizeProperty, binding);

            // удалить привязку
            //BindingOperations.ClearBinding(text, TextBlock.FontSizeProperty);

            listBox.ItemsSource = lng;
            ShowerForTxT();
        }

        public void fclick()
        {
            click.Volume = 0.5;
            click.Open(new Uri("Source/click.wav", UriKind.Relative));
            click.Play();
        }

        public void ShowerForTxT()
        {
            textBox.Clear();

            foreach (Country cn in lng)
            {
                textBox.Text += cn.ToString2();
                textBox.Text += "\n";

                foreach (Series s in cn)
                {
                    textBox.Text += "\n";
                    textBox.Text += s.ToString2();

                    foreach (Couple c in s)
                    {
                        textBox.Text += "\n";
                        textBox.Text += c.ToString2();
                    }

                    textBox.Text += "\n";
                }

                textBox.Text += "\n";
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fclick();

            var selectedItems = e.AddedItems;

            if (selectedItems.Count > 0)
            {
                var selectedItem = selectedItems[0];

                foreach (Country cn in lng)
                {
                    if (cn.ToString() == selectedItem.ToString())
                    {
                        listBox1.Items.Clear();
                        listBox2.Items.Clear();

                        foreach (Series s in cn)
                        {
                            listBox1.Items.Add(s.ToString());
                        }
                    }
                }
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fclick();

            var selectedItems = e.AddedItems;

            if (selectedItems.Count > 0)
            {
                var selectedItem = selectedItems[0];

                foreach (Country cn in lng)
                {
                    foreach (Series s in cn)
                    {
                        if (s.ToString() == selectedItem.ToString())
                        {
                            listBox2.Items.Clear();
                            foreach (Couple c in s)
                            {
                                listBox2.Items.Add(c.ToString());
                            }
                        }
                    }
                }
            }

        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fclick();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            string st = "";

            if (ch == 'n')
            {
                st = Interaction.InputBox("Enter the name of new country", "New country");
            }
            else if (ch == 's')
            {
                st = Interaction.InputBox("Introduzca el nombre del nuevo país ", "Nuevo país");
            }
            else if (ch == 'r')
            {
                st = Interaction.InputBox("Введите название новой страны", "Новая страна");
            }
            if (st != "")
            {
                Country nwcntr = new Country(st);

                lng.Add_Country(nwcntr);

                ShowerForTxT();

                listBox.ItemsSource = "";
                listBox.ItemsSource = lng;

                listBox.UpdateLayout();

                if (ch == 'n')
                {
                    MessageBox.Show("Success!", "New country added");
                }
                else if (ch == 's')
                {
                    MessageBox.Show("Éxito! ", "Nuevo país añadió");
                }
                else if (ch == 'r')
                {
                    MessageBox.Show("Успех!", "Новая страна добавлена");
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            string st = "", st1 = "";

            if (ch == 'n')
            {
                st = Interaction.InputBox("Enter the name of country you want to add new TV series", "New TV Serie");
                st1 = Interaction.InputBox("Enter the name of new TV series", "New TV Series");
            }
            else if (ch == 's')
            {
                st = Interaction.InputBox("Introduzca el nombre del país en el que desea agregar la nueva serie de TV ", " Nueva Serie de TV");
                st1 = Interaction.InputBox("Escriba el nombre de la nueva serie de televisión ", " Nueva Serie de TV");
            }
            else if (ch == 'r')
            {
                st = Interaction.InputBox("Введите название страны куда вы хотите добавить сериал", "Новый сериал");
                st1 = Interaction.InputBox("Введите название сериала", "Новый сериал");
            }

            if (st != "" && st1 != "")
            {
                Series nwsrs = new Series(st1);

                foreach (Country cn in lng)
                {
                    if (cn.ToString() == st.ToString())
                    {
                        cn.Add_Series(nwsrs);
                        ShowerForTxT();
                        if (ch == 'n')
                        {
                            MessageBox.Show("Success!", "New TV added");
                        }
                        else if (ch == 's')
                        {
                            MessageBox.Show("Éxito! ", "Nueva TV añadió");
                        }
                        else if (ch == 'r')
                        {
                            MessageBox.Show("Успех!", "Новый сериал добавлен");
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            string st="", st1="", st2="";

            if (ch == 'n')
            {
                st = Interaction.InputBox("Enter the name of TV serie you want to add new couple", "New couple");
                st1 = Interaction.InputBox("Enter the name and surname of people 1", "New couple");
                st2 = Interaction.InputBox("Enter the name and surname of people 2", "New couple");
            }
            else if (ch == 's')
            {
                st = Interaction.InputBox("Escriba el nombre de la serie de televisión que desea agregar nueva pareja ", " Nueva pareja");
                st1 = Interaction.InputBox("Introduzca el nombre y apellidos de personas 1", " Nueva pareja");
                st2 = Interaction.InputBox("Introduzca el nombre y apellidos de personas 2", " Nueva pareja");
            }
            else if (ch == 'r')
            {
                st = Interaction.InputBox("Введите название сериала в который вы хотите добавить пару", "Новая пара");
                st1 = Interaction.InputBox("Введите имя и фамилию первого персонажа", "Новая пара");
                st2 = Interaction.InputBox("Введите имя и фамилию второго персонажа", "Новая пара");
            }
            if (st != "" && st1 != "" && st2 != "")
                foreach (Country cn in lng)
                {
                    foreach (Series s in cn)
                    {
                        if (s.ToString() == st.ToString())
                        {
                            s.Add_Couple(st1, st2);
                            ShowerForTxT();
                            if (ch == 'n')
                            {
                                MessageBox.Show("Success!", "New couple added");
                            }
                            else if (ch == 's')
                            {
                                MessageBox.Show("Éxito! ", " Nueva pareja añadió");
                            }
                            else if (ch == 'r')
                            {
                                MessageBox.Show("Успех!", "Новая пара добавлена");
                            }
                        }
                    }
                }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            bool f = false;

            foreach (Country cn in lng)
            {
                foreach (Series s in cn)
                {
                    foreach (Couple c in s)
                    {
                        if (c.ToString() == textBox3.Text)
                        {
                            if (ch == 'n')
                            {
                                textBlock.Text = "Couple: " + c.ToString() + "\nTVSerie: " + s.ToString() + "\nCountry: " + cn.ToString();
                            }
                            else if (ch == 's')
                            {
                                textBlock.Text = "Par: " + c.ToString() + "\nSerieTV: " + s.ToString() + "\nPaís: " + cn.ToString();
                            }
                            else if (ch == 'r')
                            {
                                textBlock.Text = "Пара: " + c.ToString() + "\nTV Сериал: " + s.ToString() + "\nСтрана: " + cn.ToString();
                            }

                            textBox3.Clear();
                            f = true;
                        }
                        else if (f == false)
                        {
                            textBlock.Text = "Result: Not found";
                        }

                    }
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            string st = "";

            if (ch == 'n')
            {
                st = Interaction.InputBox("Enter the name of country you want to remove", "Remove country");
            }
            else if (ch == 's')
            {
                st = Interaction.InputBox("Introduzca el nombre del país que desea eliminar ", " Eliminar país");
            }
            else if (ch == 'r')
            {
                st = Interaction.InputBox("Введите название страны которую вы хотите удалить", "Удалить страну");
            }
            lng.Remove_Country(st.ToString());

            ShowerForTxT();

            listBox.ItemsSource = "";
            listBox.ItemsSource = lng;

            ShowerForTxT();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            click.Volume = 1;
            click.Open(new Uri("Source/click.wav", UriKind.Relative));
            click.Play();

            string st = "";

            if (ch == 'n')
            {
                st = Interaction.InputBox("Enter the name of country you want to remove", "Remove TV serie");
            }
            else if (ch == 's')
            {
                st = Interaction.InputBox("Introduzca el nombre del país que desea eliminar", "Eliminar serie de TV");
            }
            else if (ch == 'r')
            {
                st = Interaction.InputBox("Введите название сериала которого вы хотите удалить", "Удалить сериал");
            }

            for (int i = 0; i < lng.Length; i++)
                lng[i].Remove_Series(st);

            ShowerForTxT();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            fclick();
            string st2 = "";
            if (ch == 'n')
            {
                st2 = Interaction.InputBox("Enter the name of couple you want to remove", "Remove couple");
            }
            else if (ch == 's')
            {
                st2 = Interaction.InputBox("Escriba el nombre de la pareja que desea eliminar", "Eliminar pareja");
            }
            else if (ch == 'r')
            {
                st2 = Interaction.InputBox("Введите имена пары, которые вы хотите удалить", "Удалить пару");
            }
            for (int i = 0; i < lng.Length; i++)
                for (int j = 0; j < lng[i].Length; j++)
                    lng[i][j].Remove_Couple(st2);

            ShowerForTxT();
        }
//open
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            try
            {
                var file = new FileInfo("Source/Save.bin");
                FileStream fs = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                Myserializer f = new Myserializer();
                lng = (Language)f.Deserialize(fs);
                fs.Close();
                listBox.UpdateLayout();
                listBox.ItemsSource = lng;
                ShowerForTxT();
            }

            catch (FileNotFoundException a)
            {
                if (ch == 'n')
                {
                    MessageBox.Show("File not found", "Error", MessageBoxButton.OK);
                }
                else if (ch == 's')
                {
                    MessageBox.Show("Archivo no encontrado ", " Error ", MessageBoxButton.OK);
                }
                else if (ch == 'r')
                {
                    MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK);
                }
            }

        }
//save
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            fclick();
            
            Myserializer f = new Myserializer();
            var file = new FileInfo("Source/Save.bin");
            FileStream fs = file.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            f.Serialize(fs, lng);
            File.WriteAllText("Source/Save.txt", textBox.Text);
            fs.Close();

        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            if (music.Volume == 1)
            {
                button9.Background = new ImageBrush(new BitmapImage(new Uri(@"d:\2 курс 453501\C#\labs\lab02v3.0\lab02v3.0\bin\Debug\Source\soundoff.png")));
                music.Volume = 0;
            }
            else if (music.Volume == 0)
            {
                button9.Background = new ImageBrush(new BitmapImage(new Uri(@"d:\2 курс 453501\C#\labs\lab02v3.0\lab02v3.0\bin\Debug\Source\soundon.png")));
                music.Volume = 1;
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            fclick();
        }

        private void button10_Click_1(object sender, RoutedEventArgs e)
        {
            fclick();

            textBox5.Clear();

            List<string> pp = new List<string>();

            foreach (Country cn in lng)
            {
                foreach (Series s in cn)
                {
                    foreach (Couple c in s)
                    {
                        pp.Add(c.name.ToString());
                        pp.Add(c.name2.ToString());
                    }
                }
            }

            IEnumerable<string> items = pp.Where(p => p.StartsWith(textBox1.Text));

            foreach (string s in items)
                textBox5.Text += s + "\n";

            textBox1.Clear();
        }

        private void button11_Click_1(object sender, RoutedEventArgs e)
        {
            fclick();

            textBox2.Clear();

            List<string> pp = new List<string>();

            foreach (Country cn in lng)
            {
                foreach (Series s in cn)
                {
                    foreach (Couple c in s)
                    {
                        pp.Add(c.ToString());
                    }
                }
            }

            IEnumerable<string> auto = pp.OrderBy(s => s.Length);

            foreach (string str in auto)
                textBox2.Text += str + "\n";
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            fclick();

            textBox4.Clear();

            List<string> pp = new List<string>();

            foreach (Country cn in lng)
            {
                foreach (Series s in cn)
                {
                    foreach (Couple c in s)
                    {
                        pp.Add(c.ToString());
                    }
                }
            }

            IEnumerable<string> first = pp.Take(5);
            IEnumerable<string> second = pp.Skip(4);

            // Поскольку пропущены 4 элемента, пятый элемент 
            // должен присутствовать в обеих последовательностях
            IEnumerable<string> concat = first.Concat<string>(second);
            IEnumerable<string> union = first.Union<string>(second);
            
            if (ch == 'n')
            {
                textBox4.Text +=
                    "All couples: " + pp.Count() + " elements\n" +
                    "first: " + first.Count() + " elements\n" +
                    "second: " + second.Count() + " elements\n" +
                    "concat: " + concat.Count() + " elements\n" +
                    "union: " + union.Count() + " elements";
            }
            else if (ch == 's')
            {
                textBox4.Text +=
                    "Todas las parejas: " + pp.Count() + " elementos\n" +
                    "primero: " + first.Count() + " elementos\n" +
                    "segundo: " + second.Count() + " elementos\n" +
                    "concat: " + concat.Count() + " elementos\n" +
                    "unión: " + union.Count() + " elementos";
            }
            else if (ch == 'r')
            {
                textBox4.Text +=
                    "Все пары: " + pp.Count() + " элементов\n" +
                    "первый: " + first.Count() + " элементов\n" +
                    "второй: " + second.Count() + " элементов\n" +
                    "concat: " + concat.Count() + " элементов\n" +
                    "объединение: " + union.Count() + " элементов";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ch == 'n')
            {
                MessageBox.Show("Hi! This is my work for past 4 moths. Enjoy.", "TVSeriesLife");
            }
            else if (ch == 's')
            {
                MessageBox.Show("¡Hola! Este es mi trabajo por los últimos 4 meses. Disfrutar.", "LaVidaDeLaSerieDeTV");
            }
            else if (ch == 'r')
            {
                MessageBox.Show("Приветствую! Это моя работа за последние 4 месяца. Развлекайтесь.", "TV жизнь");
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ch = 'n';

            textBlock4.Text = "Countries";
            textBlock5.Text = "TV Series";
            textBlock6.Text = "Couples";
            button.Content = "Add Country";
            button1.Content = "Add TV Serie";
            button2.Content = "Add Couple";
            button4.Content = "Remove Country";
            button5.Content = "Remove TV Serie";
            button6.Content = "Remove Couple";
            textBlock7.Text = "Enter the names of peoples you want to know about (for example: Elena Gilbert and Damon Salvatore)";
            button3.Content = "Find";
            textBlock.Text = "Result";
            textBlock1.Text = "Enter the first letters (or name)";
            button10.Content = "Select from collection";
            textBox5.Text = "Result";
            button11.Content = "OrderBy";
            button12.Content = "Union";
            
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            ch = 's';
            
            textBlock4.Text = "Los paises";
            textBlock5.Text = "Series de Televisión";
            textBlock6.Text = "Los Parejas";
            button.Content = "Añadir País";
            button1.Content = "Añadir Serie TV";
            button2.Content = "Añadir Pareja";
            button4.Content = "Retire País";
            button5.Content = "Retire Serie TV";
            button6.Content = "Retire Pareja";
            textBlock7.Text = "Introduzca los nombres de los pueblos que desea saber sobre (por ejemplo: Elena Gilbert y Damon Salvatore)";
            button3.Content = "Encontrar";
            textBlock.Text = "Resultado";
            textBlock1.Text = "Introduzca las primeras letras (o nombre)";
            button10.Content = "Seleccione de la colección";
            textBox5.Text = "Resultado";
            button11.Content = "OrdenarPor";
            button12.Content = "Unión";
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            ch = 'r';
            
            textBlock4.Text = "Страны";
            textBlock5.Text = "Сериалы";
            textBlock6.Text = "Пары";
            button.Content = "Добавить страну";
            button1.Content = "Добавить сериал";
            button2.Content = "Добавить пару";
            button4.Content = "Удалить страну";
            button5.Content = "Удалить сериал";
            button6.Content = "Удалить пару";
            textBlock7.Text = "Введите имена о которых вы хотите узнать (например: Elena Gilbert и Damon Salvatore)";
            button3.Content = "Найти";
            textBlock.Text = "Результат";
            textBlock1.Text = "Введите пкрвые буквы (или имя)";
            button10.Content = "Выборка из коллекции";
            textBox5.Text = "Результат";
            button11.Content = "Упорядочивание";
            button12.Content = "Объединение";
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {

            if (ch == 'n')
            {
                MessageBox.Show("About the owner:\nHi, My Name is Valeria. And I'm a student of BSUIR", "About");
            }
            else if (ch == 's')
            {
                MessageBox.Show("Sobre la propietario:\nHola, mi nombre es Valería. Y yo soy la estudiante BSUIR", "Sobre mí");
            }
            else if (ch == 'r')
            {
                MessageBox.Show("О владельце:\nПривет, меня зовут Валерия. И я студент БГУИР", "Сведения");
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
            //отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (Language != null)
                {
                    App.Language = lang;
                }
            }

        }

        private void fallingstar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void image_GotMouseCapture(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Image img = (System.Windows.Controls.Image)sender;
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            img.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, animation);
        }
    }

}