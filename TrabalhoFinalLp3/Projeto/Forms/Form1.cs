using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace TrabalhoFinalLp3
{
    public partial class Form1 : Form
    {
        Var variables = new Var();
        Sender send = new Sender();
        Batalha bat = new Batalha();
        

        private void btnbattle_Click(object sender, EventArgs e)
        {
            bat.batalhar(this);
        }


        private void Atk1P1_Click(object sender, EventArgs e)
        {
            variables.actiongetter1(1);
            tabPokControl.SelectedTab = tabPok2;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn2P_Click(object sender, EventArgs e)
        {
            efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\btn.MP3";
            efeitosonoropok.Ctlcontrols.play();
            tabPokControl.SelectedTab = select;
        }

        private void Atk1P2_Click(object sender, EventArgs e)
        {
            variables.actiongetter2(1);
            battleTXT.Text = "Os 2 Pokemons se preparam para briga!!";
            tabPokControl.SelectedTab = tabBattle;
        }

        private void btnVolt1_Click(object sender, EventArgs e)
        {
            tabPokControl.SelectedTab = menu;
        }

        private void btnVolt2_Click(object sender, EventArgs e)
        {
            efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\btn.MP3";
            efeitosonoropok.Ctlcontrols.play();
            tabPokControl.SelectedTab = menu;
        }

        private void Atk2P1_Click(object sender, EventArgs e)
        {
            variables.actiongetter1(2);
            tabPokControl.SelectedTab = tabPok2;
        }

        private void Atk3P1_Click(object sender, EventArgs e)
        {
            variables.actiongetter1(3);
            tabPokControl.SelectedTab = tabPok2;
        }

        private void Atk4P1_Click(object sender, EventArgs e)
        {
            variables.actiongetter1(4);
            tabPokControl.SelectedTab = tabPok2;
        }

        private void Atk2P2_Click(object sender, EventArgs e)
        {
            variables.actiongetter2(2);
            battleTXT.Text = "Os 2 Pokemons se preparam para briga!!";
            tabPokControl.SelectedTab = tabBattle;
        }

        private void Atk3P2_Click(object sender, EventArgs e)
        {
            variables.actiongetter2(3);
            battleTXT.Text = "Os 2 Pokemons se preparam para briga!!";
            tabPokControl.SelectedTab = tabBattle;
        }

        private void Atk4P2_Click(object sender, EventArgs e)
        {
            variables.actiongetter2(4);
            battleTXT.Text = "Os 2 Pokemons se preparam para briga!!";
            tabPokControl.SelectedTab = tabBattle;
        }

        private void btnOk2_Click(object sender, EventArgs e)
        {
            send.enviador(this);
        }

        private void btnPokedexP1_Click(object sender, EventArgs e)
        {
            tabPokControl.SelectedTab = PokedexP1;
        }

        private void btnPokedexP2_Click(object sender, EventArgs e)
        {
            tabPokControl.SelectedTab = PokedexP2;
        }

        private void VoltP2_Click(object sender, EventArgs e)
        {
            tabPokControl.SelectedTab = tabPok2;
        }

        private void VoltP1_Click(object sender, EventArgs e)
        {
            tabPokControl.SelectedTab = tabPok1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBaseManager PokeMDF = new DataBaseManager("Pokemon");
            DataTable Nomes = PokeMDF.ConsultarBanco("SELECT Nome FROM Pokemon");
            if(Nomes.Rows.Count<=1)
            {
                MessageBox.Show("O banco de dados não possui nenhum pokemon, ou só possui 1, assim não da pra joojar...");
                this.Close();
            }
            for(int i=0;i<Nomes.Rows.Count;i++)
            {
                PokeBoxP1.Items.Add(Nomes.Rows[i]["Nome"]);
                PokeBoxP2.Items.Add(Nomes.Rows[i]["Nome"]);
            }
        }


        private void PokeBoxP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            desc1P1.Text = "Descrição:";
            desc2P1.Text = "Descrição:";
            desc3P1.Text = "Descrição:";
            desc4P1.Text = "Descrição:";
            send.MudandoPok(PokeBoxP1, Skill1P1, Skill2P1, Skill3P1, Skill4P1, MiniPok1);
        }


        private void PokeBoxP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            desc1P2.Text = "Descrição:";
            desc2P2.Text = "Descrição:";
            desc3P2.Text = "Descrição:";
            desc4P2.Text = "Descrição:";
            send.MudandoPok(PokeBoxP2, Skill1P2, Skill2P2, Skill3P2, Skill4P2, MiniPok2);
        }

        private void Skill1P1_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill1P1, desc1P1);
        }

        private void Skill2P1_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill2P1, desc2P1);
        }

        private void Skill3P1_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill3P1, desc3P1);
        }

        private void Skill4P1_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill4P1, desc4P1);
        }

        private void Skill1P2_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill1P2, desc1P2);
        }

        private void Skill2P2_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill2P2, desc2P2);
        }

        private void Skill3P2_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill3P2, desc3P2);
        }

        private void Skill4P2_SelectedIndexChanged(object sender, EventArgs e)
        {
            send.MudandoSkill(Skill4P2, desc4P2);
        }
    }
}
