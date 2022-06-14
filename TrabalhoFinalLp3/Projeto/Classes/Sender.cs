using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoFinalLp3
{
    class Sender : Var
    {
        public void enviador(Form1 forms)
        {
            DataBaseManager PokeMDF = new DataBaseManager("Pokemon");
            if (!TestarSkills(forms.Skill1P1, forms.Skill2P1, forms.Skill3P1, forms.Skill4P1) || !TestarSkills(forms.Skill1P2, forms.Skill2P2, forms.Skill3P2, forms.Skill4P2))
                MessageBox.Show("Tem skills iguais em 1 ou nos 2 pokemons, arruma isso ae");
            else if (forms.Skill1P1.Text == null || forms.Skill2P1.Text == null || forms.Skill3P1.Text == null || forms.Skill4P1.Text == null || forms.Skill1P2.Text == null || forms.Skill2P2 == null || forms.Skill3P2.Text == null || forms.Skill4P2.Text == null)
                MessageBox.Show("Tem skills em branco ae");
            else
            {
                Random rand = new Random();
                forms.battlemusic.URL = Directory.GetCurrentDirectory() + $@"\battle{rand.Next(1, 3)}.MP3";
                forms.battlemusic.Ctlcontrols.play();
                forms.battlemusic.settings.setMode("loop", true);
                DataTable pok1 = PokeMDF.ConsultarBanco($"SELECT * FROM Pokemon WHERE Nome='{forms.PokeBoxP1.Text}'");
                forms.Tipo1PokP1.Text = pok1.Rows[0]["Tipo"].ToString();
                if (pok1.Rows[0]["Tipo2"].ToString() != null)
                    forms.Tipo2PokP1.Text = pok1.Rows[0]["Tipo2"].ToString();
                else
                    forms.Tipo2PokP1.Text = "";
                forms.AtkFsP1.Text = pok1.Rows[0]["Dano"].ToString();
                forms.AtkSpP1.Text = pok1.Rows[0]["DanoSp"].ToString();
                P1HPMax = Convert.ToInt32(pok1.Rows[0]["Vida"].ToString());
                forms.DefFsP1.Text = pok1.Rows[0]["Defesa"].ToString();
                forms.DefSpP1.Text = pok1.Rows[0]["DefesaSp"].ToString();
                forms.VelPokP1.Text = pok1.Rows[0]["Velocidade"].ToString();
                forms.NomePokeP1.Text = pok1.Rows[0]["Nome"].ToString();
                P1FOTO = pok1.Rows[0]["Foto"].ToString();
                P1SOUND = pok1.Rows[0]["Audio"].ToString();
                try { forms.PokedexP1img.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P1FOTO); }
                catch { forms.PokedexP1img.Image = null; }
                
                Random IV = new Random();
                P1IV = IV.Next(1, 32);
                AttStats(P1IV, forms.numLVLP1, forms.AtkFsP1);
                AttStats(P1IV, forms.numLVLP1, forms.AtkSpP1);
                AttStats(P1IV, forms.numLVLP1, forms.DefFsP1);
                AttStats(P1IV, forms.numLVLP1, forms.DefSpP1);
                AttStats(P1IV, forms.numLVLP1, forms.VelPokP1);
                P1HPMax = AttHP(P1IV, forms.numLVLP1, P1HPMax);
                P1HPAtual = P1HPMax;
                AttBarrasP1(100, 100, forms);

                DataTable pok2 = PokeMDF.ConsultarBanco($"SELECT * FROM Pokemon WHERE Nome='{forms.PokeBoxP2.Text}'");
                forms.Tipo1PokP2.Text = pok2.Rows[0]["Tipo"].ToString();
                if (pok2.Rows[0]["Tipo2"].ToString() != null)
                    forms.Tipo2PokP2.Text = pok2.Rows[0]["Tipo2"].ToString();
                else
                    forms.Tipo2PokP2.Text = "";
                forms.AtkFsP2.Text = pok2.Rows[0]["Dano"].ToString();
                forms.AtkSpP2.Text = pok2.Rows[0]["DanoSp"].ToString();
                P2HPMax = Convert.ToInt32(pok2.Rows[0]["Vida"].ToString());
                forms.DefFsP2.Text = pok2.Rows[0]["Defesa"].ToString();
                forms.DefSpP2.Text = pok2.Rows[0]["DefesaSp"].ToString();
                forms.VelPokP2.Text = pok2.Rows[0]["Velocidade"].ToString();
                forms.NomePokeP2.Text = pok2.Rows[0]["Nome"].ToString();
                P2FOTO = pok2.Rows[0]["Foto"].ToString();
                P2SOUND = pok2.Rows[0]["Audio"].ToString();
                try { forms.PokedexP2img.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P2FOTO); }
                catch { forms.PokedexP2img.Image = null; }
                
                P2IV = IV.Next(1, 32);
                AttStats(P2IV, forms.numLVLP2, forms.AtkFsP2);
                AttStats(P2IV, forms.numLVLP2, forms.AtkSpP2);
                AttStats(P2IV, forms.numLVLP2, forms.DefFsP2);
                AttStats(P2IV, forms.numLVLP2, forms.DefSpP2);
                AttStats(P2IV, forms.numLVLP2, forms.VelPokP2);
                P2HPMax = AttHP(P2IV, forms.numLVLP2, P2HPMax);
                P2HPAtual = P2HPMax;
                AttBarrasP2(100, 100, forms);

                DataTable hab1p1 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill1P1.Text}'");
                forms.TipoS1P1.Text = hab1p1.Rows[0]["Tipo"].ToString();
                forms.PoderS1P1.Text = hab1p1.Rows[0]["Poder"].ToString();
                forms.AcertoS1P1.Text = hab1p1.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS1P1.Text = hab1p1.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS1P1.Text = hab1p1.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS1P1.Text = hab1p1.Rows[0]["Quantidade"].ToString();
                S1P1NOME = hab1p1.Rows[0]["Nome"].ToString();
                S1P1FOTO = hab1p1.Rows[0]["Foto"].ToString();
                forms.txtHab1P1.Text = "HABILIDADE 1 - " + S1P1NOME;
                S1P1CAT = Convert.ToInt32(hab1p1.Rows[0]["Categoria"]);
                CategoriaSkill(S1P1CAT, forms.skilcatS1P1);
                if (S1P1CAT == 0)
                {
                    S1P1Efeito = hab1p1.Rows[0]["Efeito"].ToString();
                    S1P1Efetividade = hab1p1.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab2p1 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill2P1.Text}'");
                forms.TipoS2P1.Text = hab2p1.Rows[0]["Tipo"].ToString();
                forms.PoderS2P1.Text = hab2p1.Rows[0]["Poder"].ToString();
                forms.AcertoS2P1.Text = hab2p1.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS2P1.Text = hab2p1.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS2P1.Text = hab2p1.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS2P1.Text = hab2p1.Rows[0]["Quantidade"].ToString();
                S2P1NOME = hab2p1.Rows[0]["Nome"].ToString();
                S2P1FOTO = hab2p1.Rows[0]["Foto"].ToString();
                forms.txtHab2P1.Text = "HABILIDADE 2 - " + S2P1NOME;
                S2P1CAT = Convert.ToInt32(hab2p1.Rows[0]["Categoria"]);
                CategoriaSkill(S2P1CAT, forms.skilcatS2P1);
                if (S2P1CAT == 0)
                {
                    S2P1Efeito = hab2p1.Rows[0]["Efeito"].ToString();
                    S2P1Efetividade = hab2p1.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab3p1 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill3P1.Text}'");
                forms.TipoS3P1.Text = hab3p1.Rows[0]["Tipo"].ToString();
                forms.PoderS3P1.Text = hab3p1.Rows[0]["Poder"].ToString();
                forms.AcertoS3P1.Text = hab3p1.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS3P1.Text = hab3p1.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS3P1.Text = hab3p1.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS3P1.Text = hab3p1.Rows[0]["Quantidade"].ToString();
                S3P1NOME = hab3p1.Rows[0]["Nome"].ToString();
                S3P1FOTO = hab3p1.Rows[0]["Foto"].ToString();
                forms.txtHab3P1.Text = "HABILIDADE 3 - " + S3P1NOME;
                S3P1CAT = Convert.ToInt32(hab3p1.Rows[0]["Categoria"]);
                CategoriaSkill(S3P1CAT, forms.skilcatS3P1);
                if (S3P1CAT == 0)
                {
                    S3P1Efeito = hab3p1.Rows[0]["Efeito"].ToString();
                    S3P1Efetividade = hab3p1.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab4p1 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill4P1.Text}'");
                forms.TipoS4P1.Text = hab4p1.Rows[0]["Tipo"].ToString();
                forms.PoderS4P1.Text = hab4p1.Rows[0]["Poder"].ToString();
                forms.AcertoS4P1.Text = hab4p1.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS4P1.Text = hab4p1.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS4P1.Text = hab4p1.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS4P1.Text = hab4p1.Rows[0]["Quantidade"].ToString();
                S4P1NOME = hab4p1.Rows[0]["Nome"].ToString();
                S4P1FOTO = hab4p1.Rows[0]["Foto"].ToString();
                forms.txtHab4P1.Text = "HABILIDADE 4 - " + S4P1NOME;
                S4P1CAT = Convert.ToInt32(hab4p1.Rows[0]["Categoria"]);
                CategoriaSkill(S4P1CAT, forms.skilcatS4P1);
                if (S4P1CAT == 0)
                {
                    S4P1Efeito = hab4p1.Rows[0]["Efeito"].ToString();
                    S4P1Efetividade = hab4p1.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab1p2 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill1P2.Text}'");
                forms.TipoS1P2.Text = hab1p2.Rows[0]["Tipo"].ToString();
                forms.PoderS1P2.Text = hab1p2.Rows[0]["Poder"].ToString();
                forms.AcertoS1P2.Text = hab1p2.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS1P2.Text = hab1p2.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS1P2.Text = hab1p2.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS1P2.Text = hab1p2.Rows[0]["Quantidade"].ToString();
                S1P2NOME = hab1p2.Rows[0]["Nome"].ToString();
                S1P2FOTO = hab1p2.Rows[0]["Foto"].ToString();
                forms.txtHab1P2.Text = "HABILIDADE 1 - " + S1P2NOME;
                S1P2CAT = Convert.ToInt32(hab1p2.Rows[0]["Categoria"]);
                CategoriaSkill(S1P2CAT, forms.skilcatS1P2);
                if (S1P2CAT == 0)
                {
                    S1P2Efeito = hab1p2.Rows[0]["Efeito"].ToString();
                    S1P2Efetividade = hab1p2.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab2p2 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill2P2.Text}'");
                forms.TipoS2P2.Text = hab2p2.Rows[0]["Tipo"].ToString();
                forms.PoderS2P2.Text = hab2p2.Rows[0]["Poder"].ToString();
                forms.AcertoS2P2.Text = hab2p2.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS2P2.Text = hab2p2.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS2P2.Text = hab2p2.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS2P2.Text = hab2p2.Rows[0]["Quantidade"].ToString();
                S2P2NOME = hab2p2.Rows[0]["Nome"].ToString();
                S2P2FOTO = hab2p2.Rows[0]["Foto"].ToString();
                forms.txtHab2P2.Text = "HABILIDADE 2 - " + S2P2NOME;
                S2P2CAT = Convert.ToInt32(hab2p2.Rows[0]["Categoria"]);
                CategoriaSkill(S2P2CAT, forms.skilcatS2P2);
                if (S2P2CAT == 0)
                {
                    S2P2Efeito = hab2p2.Rows[0]["Efeito"].ToString();
                    S2P2Efetividade = hab2p2.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab3p2 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill3P2.Text}'");
                forms.TipoS3P2.Text = hab3p2.Rows[0]["Tipo"].ToString();
                forms.PoderS3P2.Text = hab3p2.Rows[0]["Poder"].ToString();
                forms.AcertoS3P2.Text = hab3p2.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS3P2.Text = hab3p2.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS3P2.Text = hab3p2.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS3P2.Text = hab3p2.Rows[0]["Quantidade"].ToString();
                S3P2NOME = hab3p2.Rows[0]["Nome"].ToString();
                S3P2FOTO = hab3p2.Rows[0]["Foto"].ToString();
                forms.txtHab3P2.Text = "HABILIDADE 3 - " + S3P2NOME;
                S3P2CAT = Convert.ToInt32(hab3p2.Rows[0]["Categoria"]);
                CategoriaSkill(S3P2CAT, forms.skilcatS3P2);
                if (S3P2CAT == 0)
                {
                    S3P2Efeito = hab3p2.Rows[0]["Efeito"].ToString();
                    S3P2Efetividade = hab3p2.Rows[0]["EfetividadeEfeito"].ToString();
                }

                DataTable hab4p2 = PokeMDF.ConsultarBanco($"SELECT * FROM Habilidades WHERE Nome='{forms.Skill4P2.Text}'");
                forms.TipoS4P2.Text = hab4p2.Rows[0]["Tipo"].ToString();
                forms.PoderS4P2.Text = hab4p2.Rows[0]["Poder"].ToString();
                forms.AcertoS4P2.Text = hab4p2.Rows[0]["Acerto"].ToString();
                forms.PrioridadeS4P2.Text = hab4p2.Rows[0]["Prioridade"].ToString();
                forms.QuantMaxS4P2.Text = hab4p2.Rows[0]["Quantidade"].ToString();
                forms.QuantAtualS4P2.Text = hab4p2.Rows[0]["Quantidade"].ToString();
                S4P2NOME = hab4p2.Rows[0]["Nome"].ToString();
                S4P2FOTO = hab4p2.Rows[0]["Foto"].ToString();
                forms.txtHab4P2.Text = "HABILIDADE 4 - " + S4P2NOME;
                S4P2CAT = Convert.ToInt32(hab4p2.Rows[0]["Categoria"]);
                CategoriaSkill(S4P2CAT, forms.skilcatS4P2);
                if (S4P2CAT == 0)
                {
                    S4P2Efeito = hab4p2.Rows[0]["Efeito"].ToString();
                    S4P2Efetividade = hab4p2.Rows[0]["EfetividadeEfeito"].ToString();
                }

                forms.Atk1P1.Text = S1P1NOME;
                forms.Atk2P1.Text = S2P1NOME;
                forms.Atk3P1.Text = S3P1NOME;
                forms.Atk4P1.Text = S4P1NOME;
                try { forms.ImgPok1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P1FOTO); }
                catch { forms.ImgPok1.Image = null; }
                try { forms.ImgPokInim1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P2FOTO); }
                catch { forms.ImgPokInim1.Image = null; }
                forms.Atk1P2.Text = S1P2NOME;
                forms.Atk2P2.Text = S2P2NOME;
                forms.Atk3P2.Text = S3P2NOME;
                forms.Atk4P2.Text = S4P2NOME;
                forms.atkbonusP1.Text = "1";
                forms.defbonusP1.Text = "1";
                forms.atkbonusP2.Text = "1";
                forms.defbonusP2.Text = "1";
                forms.Atk1P1.Enabled = true;
                forms.Atk2P1.Enabled = true;
                forms.Atk3P1.Enabled = true;
                forms.Atk4P1.Enabled = true;
                forms.Atk1P2.Enabled = true;
                forms.Atk2P2.Enabled = true;
                forms.Atk3P2.Enabled = true;
                forms.Atk4P2.Enabled = true;
                p1slpturn = 0;
                p2slpturn = 0;
                SleepP1 = 0;
                SleepP2 = 0;
                Burnp1 = 0;
                Burnp2 = 0;
                ParalyseP1 = 0;
                ParalyseP2 = 0;
                try { forms.ImgPok2.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P2FOTO); }
                catch { forms.ImgPok2.Image = null; }
                try { forms.ImgPokInim2.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P1FOTO); }
                catch { forms.ImgPokInim2.Image = null; }
                
                forms.tab1P1stIMG.Image = null;
                forms.tab2P1stIMG.Image = null;
                forms.battleP1st.Image = null;
                forms.tab1P2stIMG.Image = null;
                forms.tab2P2stIMG.Image = null;
                forms.battleP2st.Image = null;
                forms.tabPokControl.SelectedTab = forms.tabPok1;
                try { forms.BattleImgP1.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P1FOTO); }
                catch { forms.BattleImgP1.Image = null; }
                try { forms.BattleImgP2.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + P2FOTO); }
                catch { forms.BattleImgP2.Image = null; }
            }
        }
        public void MudandoPok(ComboBox player, ComboBox s1, ComboBox s2, ComboBox s3, ComboBox s4, PictureBox imagee)
        {
            DataBaseManager PokeMDF = new DataBaseManager("Pokemon");
            s1.Items.Clear();
            s2.Items.Clear();
            s3.Items.Clear();
            s4.Items.Clear();
            DataTable Tipos = PokeMDF.ConsultarBanco($"SELECT Tipo,Tipo2 FROM Pokemon WHERE Nome='{player.Text}'");
            string tip = Tipos.Rows[0][0].ToString();
            string tip2 = Tipos.Rows[0][1].ToString();
            DataTable Fotos = PokeMDF.ConsultarBanco($"SELECT Foto FROM Pokemon WHERE Nome='{player.Text}'");
            string fot = Fotos.Rows[0][0].ToString();
            try{ imagee.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + fot); }
            catch { imagee.Image = null; }
            DataTable ID = PokeMDF.ConsultarBanco($"SELECT ID FROM Pokemon WHERE Nome='{player.Text}'");
            string id = ID.Rows[0][0].ToString();
            DataTable Skills = PokeMDF.ConsultarBanco($"SELECT Nome FROM Habilidades WHERE PoksID like '%{id}%' or PoksID like '%{id}' or PoksID like '{id}%' or Tipo='{tip}' or Tipo='{tip2}'");
            if(Skills.Rows.Count==0)
                MessageBox.Show("esse pokemon não possui skills válidas, escolha outro");
            else
            {
                for (int i = 0; i < Skills.Rows.Count; i++)
                {
                    s1.Items.Add(Skills.Rows[i]["Nome"].ToString());
                    s2.Items.Add(Skills.Rows[i]["Nome"].ToString());
                    s3.Items.Add(Skills.Rows[i]["Nome"].ToString());
                    s4.Items.Add(Skills.Rows[i]["Nome"].ToString());
                }
            }
        }
        public void MudandoSkill(ComboBox skill, Label desc)
        {
            DataBaseManager PokeMDF = new DataBaseManager("Pokemon");
            DataTable descricao = PokeMDF.ConsultarBanco($"SELECT Descricao FROM Habilidades WHERE Nome='{skill.Text}'");
            desc.Text = "Descrição: " + descricao.Rows[0][0].ToString();
        }
    }
}
