using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganizadorV1._0
{
    public partial class Form1 : Form
    {

        double proporcaoPainelEsquerda = 0.7;
        int buttonSizeX = 100;
        int buttonSizeY = 20;
        int textBoxSizeX = 190;
        int textBoxSizeY = 25;
        string localArquivo = "C:\\Users\\usuario\\Desktop\\dadoSalvos.txt";

        public Form1()
        {
            this.Resize += form1_resize;

            InitializeComponent();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
        }

        MainPanel mainPanelLeft = new MainPanel();
        MainPanel mainPanelRight = new MainPanel();
        List<Entradas> listaItens = new List<Entradas>();
        TextBox textBoxTipo = new TextBox();
        TextBox textBoxStatus = new TextBox();
        TextBox textBoxNome = new TextBox();
        TextBox textBoxInscricao = new TextBox();

        private void Form1_Load(object sender, EventArgs e)
        {
            CriarItemRight();
            PopularListaItens();
            OrdenarPorNome(listaItens);
            CriarItemLeftMultiplos(listaItens);
        }

        private void OrdenarPorNome(List<Entradas> lista)
        {
            lista.Sort(delegate (Entradas x, Entradas y)
            {
                if (x.Nome == null && y.Nome == null) return 0;
                else if (x.Nome == null) return -1;
                else if (y.Nome == null) return 1;
                else return x.Nome.CompareTo(y.Nome);
            });
        }

        private void PopularListaItens()
        {
            int valorIndex = 0;
            if (File.Exists(localArquivo))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(localArquivo))
                    {
                        string stringValor;
                        while ((stringValor = reader.ReadLine())!= null)
                        {
                            String[] listaString = stringValor.Split(';') ;
                            Entradas entrada = GeradorDeEntrada(Convert.ToInt32(listaString[0]), listaString[1], listaString[2], listaString[3]);
                            entrada.Index = valorIndex;
                            valorIndex += 1;
                            listaItens.Add(entrada);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CriarItemRight()
        {
            mainPanelRight.SetBounds(50,50,200,300);

            var lbInscricao = new Label();
            lbInscricao.Text = "Incrição";
            mainPanelRight.Controls.Add(lbInscricao);

            textBoxInscricao.MinimumSize = new Size(textBoxSizeX, textBoxSizeY);
            textBoxInscricao.Text = "Inscrição";
            textBoxInscricao.ForeColor = Color.Gray;
            textBoxInscricao.KeyDown += TextBoxInscricao_KeyDown;
            mainPanelRight.Controls.Add(textBoxInscricao);


            var lbNome = new Label();
            lbNome.Text = "Nome";
            mainPanelRight.Controls.Add(lbNome);

            textBoxNome.MinimumSize = new Size(textBoxSizeX, textBoxSizeY);
            textBoxNome.Text = "Nome";
            textBoxNome.ForeColor = Color.Gray;
            textBoxNome.KeyDown += TextBoxNome_KeyDown;
            mainPanelRight.Controls.Add(textBoxNome);

            var lbStatus = new Label();
            lbStatus.Text = "Status";
            mainPanelRight.Controls.Add(lbStatus);

            textBoxStatus.MinimumSize = new Size(textBoxSizeX, textBoxSizeY);
            textBoxStatus.Text = "Status";
            textBoxStatus.ForeColor = Color.Gray;
            textBoxStatus.KeyDown += TextBoxStatus_KeyDown;
            mainPanelRight.Controls.Add(textBoxStatus);

            var lbTipo = new Label();
            lbTipo.Text = "Tipo";
            mainPanelRight.Controls.Add(lbTipo);

            textBoxTipo.MinimumSize = new Size(textBoxSizeX, textBoxSizeY);
            textBoxTipo.Text = "Tipo";
            textBoxTipo.ForeColor = Color.Gray;
            textBoxTipo.KeyDown += TextBoxTipo_KeyDown;
            mainPanelRight.Controls.Add(textBoxTipo);

            var btnSalvar = new Button();
            btnSalvar.Size = new Size(buttonSizeX, buttonSizeY);
            btnSalvar.Text = "Salvar";
            btnSalvar.Click += new EventHandler(botao_salvar);
            mainPanelRight.Controls.Add(btnSalvar);


            flowLayoutPanel1.Controls.Add(mainPanelRight);
        }

        private void CriarItemLeft(Entradas entrada)
        {
                FlowLayoutPanel innerContainer = new FlowLayoutPanel();
                FlowLayoutPanel smallContainerLeft = new FlowLayoutPanel();
                FlowLayoutPanel smallContainerRight = new FlowLayoutPanel();
                innerContainer.AutoSize = false;
                innerContainer.BorderStyle = BorderStyle.None;
                innerContainer.BackColor = Color.FromArgb(20, 10, 100, 100);


                innerContainer.FlowDirection = FlowDirection.LeftToRight;
                smallContainerLeft.AutoSize = true;
                smallContainerLeft.FlowDirection = FlowDirection.TopDown;
                smallContainerRight.AutoSize = true;
                smallContainerRight.FlowDirection = FlowDirection.TopDown;
                smallContainerRight.Dock = DockStyle.Right;


                Label lb = new Label();
                lb.Text = GeradorDeDescricao(entrada);
                lb.AutoSize = true;
                lb.MinimumSize = new Size(300,50);
                smallContainerLeft.Controls.Add(lb);

                var btnComplete = new Button();
                btnComplete.Tag = entrada.Index;
                btnComplete.Text = "COMPLETE" ;
                btnComplete.Click += new EventHandler(btn_complete_event);
                btnComplete.Size = new Size(buttonSizeX, buttonSizeY);
                smallContainerRight.Controls.Add(btnComplete);

                innerContainer.Controls.Add(smallContainerLeft);
                innerContainer.Controls.Add(smallContainerRight);


                mainPanelLeft.Controls.Add(innerContainer);
            
            flowLayoutPanel1.Controls.Add(mainPanelLeft);
        }

        private void TextBoxTipo_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Tipo")
            {
                tb.Text = "";
            }
            tb.ForeColor = Color.Black;
        }

        private void TextBoxStatus_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Status")
            {
                tb.Text = "";
            }
            tb.ForeColor = Color.Black;
        }

        private void TextBoxNome_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Nome")
            {
                tb.Text = "";
            }
            tb.ForeColor = Color.Black;
        }

        private void TextBoxInscricao_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Inscrição")
            {
                tb.Text = "";
            }
            tb.ForeColor = Color.Black;
        }

        private void CriarItemLeftMultiplos(List<Entradas> entrada)
        {
            mainPanelLeft.Controls.Clear();
            foreach (Entradas itemEntrada in entrada)
            {
                CriarItemLeft(itemEntrada);
            }
        }

        private void botao_salvar(object sender, EventArgs e)
        {
            Entradas entrada = null;
            SalvarEmTexto(entrada, true);
        }

        private void SalvarEmTexto(Entradas entrada, bool Append)
        {
            try
            {
                if (entrada == null)
                {
                    entrada = GeradorDeEntrada(Convert.ToInt32(textBoxInscricao.Text), textBoxNome.Text, textBoxStatus.Text, textBoxTipo.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique os valores de entrada" + ex.Message);
                textBoxInscricao.Text = "";
                textBoxNome.Text = "";
                textBoxStatus.Text = "";
                textBoxTipo.Text = "";
            }
            finally
            {
                if (entrada != null)
                {
                    if (Append == true)
                    {
                        listaItens.Add(entrada);
                        CriarItemLeft(entrada);
                        textBoxInscricao.Text = "Inscrição";
                        textBoxNome.Text = "Nome";
                        textBoxStatus.Text = "Status";
                        textBoxTipo.Text = "Tipo";
                    }
                    using (StreamWriter writer = new StreamWriter(@localArquivo, true))
                    {
                        writer.WriteLine("{0};{1};{2};{3}", entrada.Inscricao, entrada.Nome, entrada.Status, entrada.Tipo);
                    }
                }
            }

        }
        
        private Entradas GeradorDeEntrada(int inscricao, string nome, string status, string tipo)
        {
            var retorno = new Entradas(inscricao,nome, status, tipo);
            return retorno;
        }

        private string GeradorDeDescricao(Entradas entrada)
        {
            if (entrada == null)
                return "mensagem padrão, objeto null";
            string descricao;
            descricao = entrada.Inscricao + "\n" + entrada.Nome + "\n" + entrada.Status + "\n" + entrada.Tipo;
            return descricao;
        }

        private void btn_complete_event(object sender, EventArgs e)
        {
            int lic = listaItens.Count;
            int buttonTag = (int)((Button)sender).Tag;
            buttonTag = IndexSearch(buttonTag);
            listaItens.RemoveAt(buttonTag);
            CriarItemLeftMultiplos(listaItens);
            File.Delete(localArquivo);
            for (int i = 0; i < lic - 1; i++)
            {
                SalvarEmTexto(listaItens[i], false);

            }
        }

        private int IndexSearch(int valorIndex)
        {
            for (int i = 0; i < listaItens.Count; i++)
            {
                if (listaItens[i].Index == valorIndex)
                    return i;
            }
            return -1;
        }
               
        private void form1_resize(object sender, EventArgs e)
        {
            mainPanelLeft.SetBounds(100, 100, Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * proporcaoPainelEsquerda), flowLayoutPanel1.Height);
        }
      }
}
