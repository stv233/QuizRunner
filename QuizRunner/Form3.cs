﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizRunner
{
    public partial class IfrCreator : Form
    {
        // Указывает, разрешено ли форме закрыться.
        public bool CanClose;

        //Указывает, были ли изменены данные, после открытия или создания.
        public bool Changed;

        public struct UVariable
        {
            public string Name;
            public double Value;
            public TextBox NameInput;
            public NumericUpDown ValueInput;
            public Label Remove;
            public object AddButton;
        };

        public UVariable[] UserVariable = new UVariable[0];

        public struct SLine
        {
            public TextBox Prefix;
            public TextBox Calc;
            public TextBox Postfix;
            public Label Remove;
        }

        public SLine[] StatisticsLines = new SLine[0];

        public IfrCreator()
        {
            InitializeComponent();
        }

        private readonly SaveFileDialog IsfdSaveDialog = new SaveFileDialog
        {
            Title = "Сохранить",
            FileName = "Test.qrtf",
            Filter = "QuizRunner Test File (*.qrtf)|*.qrtf"
        };

        private readonly OpenFileDialog IofdOpenDialog = new OpenFileDialog
        {
            Title = "Открыть",
            FileName = "Test.qrtf",
            Filter = "QuizRunner Test File (*.qrtf)|*.qrtf"
        };

        private void IfrCreator_Load(object sender, EventArgs e)
        {
            var IttCreatorToolTip = new ToolTip();

            var IpnMenu = new Panel
            {
                BackColor = Color.FromArgb(18, 136, 235),
                Width = 60,
                Height = this.ClientSize.Height,
                Parent = this
            };

            var IpbSave = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.SavePic,
                Width = 50,
                Height = 50,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = IpnMenu,
                Left = 5,
                Top = 5
            };
            IpbSave.MouseEnter += MenuButtons_MouseEnter;
            IpbSave.MouseLeave += MenuButtons_MouseLeave;
            IpbSave.Click += IpbSave_Click;
            IttCreatorToolTip.SetToolTip(IpbSave, "Сохранить...");

            var IpbOpen = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.OpenPic,
                Width = 50,
                Height = 50,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = IpnMenu,
                Left = 5,
                Top = IpbSave.Top + IpbSave.Height + 15
            };
            IpbOpen.MouseEnter += MenuButtons_MouseEnter;
            IpbOpen.MouseLeave += MenuButtons_MouseLeave;
            IpbOpen.Click += IpbOpen_Click;
            IttCreatorToolTip.SetToolTip(IpbOpen, "Открыть...");

            var IlbExit = new Label
            {
                AutoSize = false,
                Width = IpnMenu.Width,
                Height = IpnMenu.Width,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "❌",
                Font = new Font("Verdana", 25, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = IpnMenu,
                Top = IpnMenu.Height - IpnMenu.Width
            };
            IlbExit.MouseEnter += IlbExit_MouseEnter;
            IlbExit.MouseLeave += IlbExit_MouseLeave;
            IlbExit.Click += IlbExit_Click;
            IttCreatorToolTip.SetToolTip(IlbExit, "Закрыть " + AppDomain.CurrentDomain.FriendlyName.Substring(0,
                    AppDomain.CurrentDomain.FriendlyName.Length - 4));

            var IpbBack = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.BackPic,
                Width = 50,
                Height = 50,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = IpnMenu,
                Left = 5,
            };
            IpbBack.Top = IlbExit.Top - IpbBack.Height - 15;
            IpbBack.MouseEnter += MenuButtons_MouseEnter;
            IpbBack.MouseLeave += MenuButtons_MouseLeave;
            IpbBack.Click += IpbBack_Click;
            IttCreatorToolTip.SetToolTip(IpbBack, "Вернуться в меню");

            var IpnUserVariable = new Panel
            {
                AutoScroll = true,
                Width = this.ClientSize.Width / 9 * 2,
                Height = this.ClientSize.Height,
                BackColor = Color.FromArgb(18, 136, 235),
                Left = this.ClientSize.Width - this.ClientSize.Width / 9 * 2,
                Parent = this
            };

            var IlbUserVariableHeader = new Label
            {
                AutoSize = true,
                Text = "Переменные",
                Font = new Font("Verdana", 8, FontStyle.Bold),
                ForeColor = Color.White,
                Top = 5,
                Parent = IpnUserVariable
            };
            IlbUserVariableHeader.Left = IpnUserVariable.Width / 2 - IlbUserVariableHeader.Width / 2;

            var IbtAddVariable = new Button
            {
                Text = "Добавить",
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(18, 136, 235),
                Cursor = System.Windows.Forms.Cursors.Hand,
                AutoSize = true,
                Parent = IpnUserVariable
            };
            IbtAddVariable.Left = IpnUserVariable.Width / 2 - IbtAddVariable.Width / 2;
            IbtAddVariable.Top = IlbUserVariableHeader.Top + IlbUserVariableHeader.Height + 42;
            IbtAddVariable.Click += IbtAddVariable_Click;

            var ItcQuizEditor = new TabControl
            {
                Left = IpnMenu.Width + 2,
                Width = this.ClientSize.Width - IpnMenu.Width - IpnUserVariable.Width - 2,
                Height = this.ClientSize.Height - 1,
                Parent = this
            };
            ItcQuizEditor.SendToBack();

            var ItpHome = new TabPage
            {
                Text = "Главная",
                Parent = ItcQuizEditor
            };

            var IlbTestName = new Label
            {
                AutoSize = true,
                Text = "Название теста",
                Font = new Font("Verdana", 25, FontStyle.Bold),
                ForeColor = Color.FromArgb(18, 136, 235),
                Top = 50,
                Parent = ItpHome
            };
            IlbTestName.Left = ItpHome.ClientSize.Width / 2 - IlbTestName.Width / 2;

            var ItbTestName = new TextBox
            {
                Width = ItpHome.ClientSize.Width - 40,
                Height = 25,
                Font = new Font("Verdana", 20, FontStyle.Bold),
                Top = IlbTestName.Top + IlbTestName.Height + 20,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                Parent = ItpHome
            };
            ItbTestName.Left = ItpHome.ClientSize.Width / 2 - ItbTestName.Width / 2;
            ItbTestName.TextChanged += UnsavedText_TextChanged;

            var IlbTestDescription = new Label
            {
                AutoSize = true,
                Text = "Описание теста",
                Font = new Font("Verdana", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(18, 136, 235),
                Top = ItbTestName.Top + ItbTestName.Height + 20,
                Parent = ItpHome
            };
            IlbTestDescription.Left = ItpHome.ClientSize.Width / 2 - IlbTestDescription.Width / 2;

            var IrtbTestDescription = new RichTextBox
            {
                Left = ItbTestName.Left,
                Top = IlbTestDescription.Top + IlbTestDescription.Height + 20,
                Width = ItbTestName.Width,
                Font = new Font("Verdana", 15, FontStyle.Bold),
                BorderStyle = BorderStyle.Fixed3D,
                Parent = ItpHome
            };
            IrtbTestDescription.Height = ItpHome.ClientSize.Height - IrtbTestDescription.Top - 20;
            IrtbTestDescription.TextChanged += UnsavedText_TextChanged;

            var IlbAddTabPage = new Label
            {
                AutoSize = true,
                Text = "+",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new Font("Verdana", 15, FontStyle.Bold),
                ForeColor = Color.Green,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = ItpHome
            };
            IlbAddTabPage.MouseEnter += IlbAddTabPage_MouseEnter;
            IlbAddTabPage.MouseLeave += IlbAddTabPage_MouseLeave;

            var ItpStatistics = new TabPage
            {
                Text = "Статистика",
                Parent = ItcQuizEditor
            };

            var IlbHint = new Label
            {
                AutoSize = false,
                Top = 20,
                Width = ItpStatistics.ClientSize.Width - 40,
                Height = 80,
                Text = "Создайте строку для отображения статистики.\n" +
                    "Используйте: [имя переменной], что бы использовать переменную в расчётах.",
                Font = new Font("Verdana", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(18, 136, 235),
                Parent = ItpStatistics
            };
            IlbHint.Left = ItpStatistics.ClientSize.Width / 2 - IlbHint.Width / 2;

            var IgbStatisticsLines = new GroupBox
            {
                Text = "Cтроки статистики",
                ForeColor = Color.FromArgb(18, 136, 235),
                Font = new Font("Verdana", 10, FontStyle.Bold),
                Width = ItpStatistics.ClientSize.Width - 40,
                Top = IlbHint.Top + IlbHint.Height + 30,
                Parent = ItpStatistics
            };
            IgbStatisticsLines.Left = ItpStatistics.ClientSize.Width / 2
                - IgbStatisticsLines.Width / 2;
            IgbStatisticsLines.Height = ItpStatistics.ClientSize.Height
                - IgbStatisticsLines.Top - 10;

            var IpnStatisticsLinesScroller = new Panel
            {
                AutoScroll = true,
                BorderStyle = BorderStyle.None,
                Left = 15,
                Top = 15,
                Width = IgbStatisticsLines.ClientSize.Width-30,
                Height = IgbStatisticsLines.ClientSize.Height-30,
                Parent = IgbStatisticsLines
            };

            var IlbAddStatisticsLine = new Label
            {
                AutoSize = true,
                Text = "+",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new Font("Verdana", 15, FontStyle.Bold),
                ForeColor = Color.Green,
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = ItpStatistics,
                Left=IgbStatisticsLines.Left,
                Tag= IpnStatisticsLinesScroller
            };
            IlbAddStatisticsLine.Top = IgbStatisticsLines.Top - IlbAddStatisticsLine.Height;
            IlbAddStatisticsLine.MouseEnter += IlbAddTabPage_MouseEnter;
            IlbAddStatisticsLine.MouseLeave += IlbAddTabPage_MouseLeave;
            IlbAddStatisticsLine.Click += IlbAddStatisticsLine_Click;

            var IlbStatisticPrefix = new Label
            {
                AutoSize = false,
                Font = new Font("Verdana", 8, FontStyle.Bold),
                Text = "Префикс",
                TextAlign=System.Drawing.ContentAlignment.MiddleCenter,
                Height = 15,
                Width = IpnStatisticsLinesScroller.ClientSize.Width / 10 * 2,
                Left = IpnStatisticsLinesScroller.ClientSize.Width / 20 * 1,
                Top = 20,
                Parent = IpnStatisticsLinesScroller
            };

            var IlbStatisticСalculations = new Label
            {
                AutoSize = false,
                Font = new Font("Verdana", 8, FontStyle.Bold),
                Text = "Расчёты",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 15,
                Width = IpnStatisticsLinesScroller.ClientSize.Width / 10 * 2,
                Left = IpnStatisticsLinesScroller.ClientSize.Width / 20 * 6,
                Top = 20,
                Parent = IpnStatisticsLinesScroller
            };

            var IlbStatisticPostfix = new Label
            {
                AutoSize = false,
                Font = new Font("Verdana", 8, FontStyle.Bold),
                Text = "Постфикс",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Height = 15,
                Width = IpnStatisticsLinesScroller.ClientSize.Width / 10 * 2,
                Left = IpnStatisticsLinesScroller.ClientSize.Width / 20 * 11,
                Top = 20,
                Parent = IpnStatisticsLinesScroller
            };

        }

        private void MenuButtons_MouseEnter(object sender, EventArgs e)
        {
            var Button = (PictureBox)sender;
            Button.Width += 4;
            Button.Height += 4;
            Button.Left -= 2;
            Button.Top -= 2;
        }

        private void MenuButtons_MouseLeave(object sender, EventArgs e)
        {
            var Button = (PictureBox)sender;
            Button.Width -= 4;
            Button.Height -= 4;
            Button.Left += 2;
            Button.Top += 2;
        }

        private void IlbExit_MouseEnter(object sender, EventArgs e)
        {
            var IlbExit = (Label)sender;
            IlbExit.Font = new Font("Verdana", 30, FontStyle.Bold);
            IlbExit.BackColor = Color.Red;
        }

        private void IlbExit_MouseLeave(object sender, EventArgs e)
        {
            var IlbExit = (Label)sender;
            IlbExit.Font = new Font("Verdana", 25, FontStyle.Bold);
            IlbExit.BackColor = Color.Transparent;
        }

        private void IlbExit_Click(object sender, EventArgs e)
        {
            CanClose = true;
            Application.Exit();
        }

        private void IpbBack_Click(object sender, EventArgs e)
        {
            CanClose = true;
            var IStartPage = new IfrStartPage();
            IStartPage.Show();
            this.Close();
        }

        private void IpbSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void IpbOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void IbtAddVariable_Click(object sender, EventArgs e)
        {
            var IbtAddVariable = (Button)sender;
            AddVariable(IbtAddVariable.Parent, sender);
        }

        private void UnsavedText_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void IlbAddTabPage_MouseEnter(object sender,EventArgs e)
        {
            var IlbAddTabPage = (Label)sender;
            IlbAddTabPage.ForeColor = Color.White;
            IlbAddTabPage.BackColor = Color.Green;
        }

        private void IlbAddTabPage_MouseLeave(object sender, EventArgs e)
        {
            var IlbAddTabPage = (Label)sender;
            IlbAddTabPage.ForeColor = Color.Green;
            IlbAddTabPage.BackColor = Color.Transparent;
        }

        private void IlbAddStatisticsLine_Click(object sender,EventArgs e)
        {
            var IlbAddStatisticsLine = (Label)sender;
            AddStatisticLine(IlbAddStatisticsLine.Tag);
        }

        /// <summary>
        /// Сохраняет тест в файл.
        /// </summary>
        private void Save()
        {
            if (IsfdSaveDialog.ShowDialog()==DialogResult.OK)
            {
                //Тут должна быть функция сохранения.
            }
        }

        /// <summary>
        /// Открыввает тест из файла.
        /// </summary>
        private void Open()
        {
            if (IofdOpenDialog.ShowDialog()==DialogResult.OK)
            {
                //Тут должна быть функция открытия.
            }
        }

        /// <summary>
        /// Создаёт пользовательскую переменную.
        /// </summary>
        /// <param name="parent">Обьект на котором будет создана переменная.</param>
        /// <param name="sender">Обьект посылающий запрос.</param>
        private void AddVariable(object parent, object sender)
        {
            var IttCreatorToolTip = new ToolTip();

            Array.Resize<UVariable>(ref UserVariable, UserVariable.Length + 1);
            Panel ParentPanel = (Panel)parent;
            int Now = UserVariable.Length - 1;
            var Name = new TextBox
            {
                Text = "Имя " + (Now).ToString(),
                Width = ParentPanel.Width / 10 * 3,
                Left = ParentPanel.Width / 10 * 1,
                Parent = ParentPanel
            };
            Name.Tag = Name.Text;
            if (UserVariable.Length==1)
            {
                Name.Top = 20;
            }
            else
            {
                Name.Top = UserVariable[Now - 1].NameInput.Top 
                    + UserVariable[Now - 1].NameInput.Height + 20;
            }
            Name.TextChanged += NameInput_TextChanged;
            IttCreatorToolTip.SetToolTip(Name, "Имя");
            UserVariable[Now].NameInput = Name;
            UserVariable[Now].Name = Name.Text;

            var Value = new NumericUpDown
            {
                Width = ParentPanel.Width / 10 * 2,
                Left = ParentPanel.Width / 10 * 5,
                Top = Name.Top,
                ThousandsSeparator = true,
                Minimum = Convert.ToDecimal(Decimal.MinValue),
                DecimalPlaces = 1,
                Maximum = Convert.ToDecimal(Decimal.MaxValue),
                Tag = Now,
                Parent = ParentPanel
            };
            Value.ValueChanged += ValueInput_ValieChanged;
            IttCreatorToolTip.SetToolTip(Value, "Значение");
            UserVariable[Now].ValueInput = Value;
            UserVariable[Now].Value = Convert.ToDouble(Value.Value);

            var Remove = new Label
            {
                AutoSize = false,
                Width = ParentPanel.Width / 10 * 1,
                Height = Name.Height,
                Left = ParentPanel.Width / 10 * 8,
                Top = Name.Top,
                ForeColor = Color.Red,
                Text = "❌",
                Font = new Font("Verdana", 12, FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = ParentPanel
            };
            Remove.Tag = Now;
            Remove.Click += RemoveVar_Click;
            IttCreatorToolTip.SetToolTip(Remove, "Удалить переменную");
            UserVariable[Now].Remove = Remove;

            var AddButton = (Button)sender;
            AddButton.Top = Name.Top + Name.Height + 20;
            UserVariable[Now].AddButton = AddButton;

            // Функция проверки имени переменной.
            // Функция добавления переменной.
        }

        /// <summary>
        /// Удаляет пользовательскую переменную по индексу в массиве.
        /// </summary>
        /// <param name="Index">Индекс.</param>
        private void RemoveVariable(int Index)
        {
            // Функция удаления переменной.

            UserVariable[Index].NameInput.Dispose();
            UserVariable[Index].ValueInput.Dispose();
            UserVariable[Index].Remove.Dispose();

            if (Index != UserVariable.Length - 1)
            {
                for (var i = Index; i < UserVariable.Length - 1; i++)
                {
                    UserVariable[i] = UserVariable[i + 1];
                    UserVariable[i].Remove.Tag = i;
                    UserVariable[i].ValueInput.Tag = i;
                    if (i != 0)
                    {
                        UserVariable[i].NameInput.Top = UserVariable[i - 1].NameInput.Top +
                            UserVariable[i - 1].NameInput.Height + 20;
                        UserVariable[i].ValueInput.Top = UserVariable[i - 1].NameInput.Top +
                            UserVariable[i - 1].NameInput.Height + 20;
                        UserVariable[i].Remove.Top = UserVariable[i - 1].NameInput.Top +
                            UserVariable[i - 1].NameInput.Height + 20;
                    }
                    else
                    {
                        UserVariable[i].NameInput.Top = 20;
                        UserVariable[i].ValueInput.Top = 20;
                        UserVariable[i].Remove.Top = 20;
                    }
                }


                Array.Resize<UVariable>(ref UserVariable, UserVariable.Length - 1);
                var AddButton = (Button)UserVariable[UserVariable.Length - 1].AddButton;
                AddButton.Top = UserVariable[UserVariable.Length - 1].NameInput.Top +
                    UserVariable[UserVariable.Length - 1].NameInput.Height + 20;
            }
            else
            {
                if (UserVariable.Length == 1)
                {
                    Array.Resize<UVariable>(ref UserVariable, UserVariable.Length - 1);
                }
                else
                {
                    Array.Resize<UVariable>(ref UserVariable, UserVariable.Length - 1);
                    var AddButton = (Button)UserVariable[UserVariable.Length - 1].AddButton;
                    AddButton.Top = UserVariable[UserVariable.Length - 1].NameInput.Top +
                        UserVariable[UserVariable.Length - 1].NameInput.Height + 20;
                }
            }
        }

        private void RemoveVar_Click(object sender, EventArgs e)
        {
            Changed = true;
            var Remove = (Label)sender;
            RemoveVariable((int)Remove.Tag);
        }

        private void NameInput_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
            var Name = (TextBox)sender;
            if (Name.Text != Name.Tag.ToString())
            {
                // Функция получения значения по имени переменной (Name.Tag.ToString())
                // Функция удаления переменной.
                // Функция создания переменной.
            }
        }

        private void ValueInput_ValieChanged(object sender, EventArgs e)
        {
            Changed = true;
            // var Value = (NumericUpDown)sender; Что бы не мешался, пока нет функции.
            // Функция изменения значения переменной по имени (UserVariable[Convert.ToInt32(Value.Tag].Name)).
        }

        /// <summary>
        /// Cоздаёт строку статистики на указанной панели.
        /// </summary>
        /// <param name="sender">Панель</param>
        private void AddStatisticLine(object sender)
        {
            Array.Resize<SLine>(ref StatisticsLines, StatisticsLines.Length + 1);
            int Now = StatisticsLines.Length - 1;
            var PParen = (Panel)sender;

            var Prefix = new TextBox
            {
                Width = PParen.Width / 10 * 2,
                Left = PParen.Width / 20 * 1,
                Parent = PParen
            };
            if (StatisticsLines.Length==1)
            {
                Prefix.Top = 40;
            }
            else
            {
                Prefix.Top = StatisticsLines[Now - 1].Prefix.Top + 30;
            }
            Prefix.TextChanged += UnsavedText_TextChanged;
            StatisticsLines[Now].Prefix = Prefix;

            var Calc = new TextBox
            {
                Width = PParen.Width / 10 * 2,
                Left = PParen.Width / 20 * 6,
                Top = Prefix.Top,
                Parent = PParen
            };
            Calc.TextChanged += UnsavedText_TextChanged;
            StatisticsLines[Now].Calc = Calc;

            var Postfix = new TextBox
            {
                Width = PParen.Width / 10 * 2,
                Left = PParen.Width / 20 * 11,
                Top = Prefix.Top,
                Parent = PParen
            };
            Postfix.TextChanged += UnsavedText_TextChanged;
            StatisticsLines[Now].Postfix = Postfix;

            var RemoveLine = new Label
            {
                AutoSize = false,
                Width = PParen.Width / 20 * 3,
                Height = Prefix.Height,
                Left = PParen.Width / 10 * 8,
                Top = Prefix.Top,
                ForeColor = Color.Red,
                Text = "❌",
                Font = new Font("Verdana", 12, FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand,
                Parent = PParen
            };
            RemoveLine.Click += RemoveLine_Click;
            RemoveLine.Tag = Now;
            StatisticsLines[Now].Remove = RemoveLine;
        }

        /// <summary>
        /// Удаляет строку статистики по указанному индексу в массиве.
        /// </summary>
        /// <param name="Index">Индекс.</param>
        private void RemoveStatisticLine(int Index)
        {
            StatisticsLines[Index].Prefix.Dispose();
            StatisticsLines[Index].Calc.Dispose();
            StatisticsLines[Index].Postfix.Dispose();
            StatisticsLines[Index].Remove.Dispose();

            for (var i = Index; i < StatisticsLines.Length - 1; i++)
            {
                StatisticsLines[i] = StatisticsLines[i + 1];
                StatisticsLines[i].Remove.Tag = i;
                
                if (i!=0)
                {
                    StatisticsLines[i].Prefix.Top = StatisticsLines[i - 1].Prefix.Top + 30;
                    StatisticsLines[i].Calc.Top = StatisticsLines[i].Prefix.Top;
                    StatisticsLines[i].Postfix.Top = StatisticsLines[i].Prefix.Top;
                    StatisticsLines[i].Remove.Top = StatisticsLines[i].Prefix.Top;
                }
                else
                {
                    StatisticsLines[i].Prefix.Top = 40;
                    StatisticsLines[i].Calc.Top = StatisticsLines[i].Prefix.Top;
                    StatisticsLines[i].Postfix.Top = StatisticsLines[i].Prefix.Top;
                    StatisticsLines[i].Remove.Top = StatisticsLines[i].Prefix.Top;
                }
            }

            Array.Resize<SLine>(ref StatisticsLines, StatisticsLines.Length - 1);
        }

        void RemoveLine_Click(object sender,EventArgs e)
        {
            var RemoveLine = (Label)sender;
            RemoveStatisticLine(Convert.ToInt32(RemoveLine.Tag));
            Changed = true;
        }
    }
}
