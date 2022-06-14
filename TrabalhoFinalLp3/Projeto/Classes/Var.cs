using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TrabalhoFinalLp3
{
    class Var
    {

        public static int S1P1CAT, S2P1CAT, S3P1CAT, S4P1CAT, S1P2CAT, S2P2CAT, S3P2CAT, S4P2CAT, P1IV, P2IV;
        public static string P1FOTO, P2FOTO, P1SOUND, P2SOUND;
        public static string S1P1FOTO, S1P1NOME, S2P1FOTO, S2P1NOME, S3P1FOTO, S3P1NOME, S4P1FOTO, S4P1NOME;
        public static string S1P2FOTO, S1P2NOME, S2P2FOTO, S2P2NOME, S3P2FOTO, S3P2NOME, S4P2FOTO, S4P2NOME;
        public static string S1P1Efeito, S1P1Efetividade, S1P2Efeito, S1P2Efetividade, S2P1Efeito, S2P1Efetividade, S2P2Efeito, S2P2Efetividade;
        public static string S3P1Efeito, S3P1Efetividade, S3P2Efeito, S3P2Efetividade, S4P1Efeito, S4P1Efetividade, S4P2Efeito, S4P2Efetividade;
        public static int acaop1, acaop2, turno = 1;
        public static int p1slpturn = 0, p2slpturn = 0;
        public static int atk1 = 0, atk2 = 0, def1 = 0, def2 = 0;
        public static int Burnp1 = 0, Burnp2 = 0, ParalyseP1 = 0, ParalyseP2 = 0, SleepP1 = 0, SleepP2 = 0;
        public static decimal P1HPMax, P2HPMax, P1HPAtual, P2HPAtual;

        public void actiongetter1(int vox)
        {
            acaop1 = vox;
        }

        public void actiongetter2(int vex)
        {
            acaop2 = vex;
        }

        protected int Ordem(Form1 forms)
        {
            int prioridadeP1 = 0, prioridadeP2 = 0, VeloP1, VeloP2;
            VeloP1 = Convert.ToInt32(forms.VelPokP1.Text);
            VeloP2 = Convert.ToInt32(forms.VelPokP2.Text);
            Random rand = new Random();
            switch (acaop1)
            {
                case 1:
                    {
                        prioridadeP1 = Convert.ToInt32(forms.PrioridadeS1P1.Text);
                        break;
                    }
                case 2:
                    {
                        prioridadeP1 = Convert.ToInt32(forms.PrioridadeS2P1.Text);
                        break;
                    }
                case 3:
                    {
                        prioridadeP1 = Convert.ToInt32(forms.PrioridadeS3P1.Text);
                        break;
                    }
                case 4:
                    {
                        prioridadeP1 = Convert.ToInt32(forms.PrioridadeS4P1.Text);
                        break;
                    }
            }
            switch (acaop2)
            {
                case 1:
                    {
                        prioridadeP2 = Convert.ToInt32(forms.PrioridadeS1P2.Text);
                        break;
                    }
                case 2:
                    {
                        prioridadeP2 = Convert.ToInt32(forms.PrioridadeS2P2.Text);
                        break;
                    }
                case 3:
                    {
                        prioridadeP2 = Convert.ToInt32(forms.PrioridadeS3P2.Text);
                        break;
                    }
                case 4:
                    {
                        prioridadeP2 = Convert.ToInt32(forms.PrioridadeS4P2.Text);
                        break;
                    }
            }
            if (prioridadeP1 > prioridadeP2)
                return 1;
            else if (prioridadeP2 > prioridadeP1)
                return 2;
            else if (prioridadeP1 == prioridadeP2 && VeloP1 > VeloP2)
                return 1;
            else if (prioridadeP1 == prioridadeP2 && VeloP2 > VeloP1)
                return 2;
            else
                return rand.Next(1, 3);
        }

        protected void ChangeStatsP1(string efeito, string efetividade, Label acerto, Form1 forms)
        {
            Random random = new Random();
            decimal a = 2, b = 2;
            if (efeito == "Ataque" && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text))) 
            {
                if (Convert.ToInt32(efetividade) > 0 && atk1 < 6 && atk1 > -6)
                {
                    if ((atk1 + Convert.ToInt32(efetividade)) <= 6)
                        atk1 += Convert.ToInt32(efetividade);
                    else
                        atk1 = 6;
                    if (atk1 < 0)
                        b = atk1 * (-1) + 2;
                    else if (atk1 > 0)
                        a = atk1 + 2;
                    forms.atkbonusP1.Text = (a / b).ToString();
                    forms.battleTXT.Text = "O ataque de " + forms.NomePokeP1.Text + " aumentou";
                }
                else if (Convert.ToInt32(efetividade) < 0 && atk2 < 6 && atk2 > -6)
                {
                    if ((atk2 + Convert.ToInt32(efetividade)) >= -6)
                        atk2 += Convert.ToInt32(efetividade);
                    else
                        atk2 = -6;
                    if (atk2 < 0)
                        b = atk2 * (-1) + 2;
                    else if (atk2 > 0)
                        a = atk2 + 2;
                    forms.atkbonusP2.Text = (a / b).ToString();
                    forms.battleTXT.Text = "O ataque de " + forms.NomePokeP2.Text + " diminuiu";
                }
                else
                    forms.battleTXT.Text = "Nada aconteceu";
            }

            else if (efeito == "Defesa" && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
            {
                if (Convert.ToInt32(efetividade) > 0 && def1 < 6 && def1 > -6)
                {
                    if ((def1 + Convert.ToInt32(efetividade)) <= 6)
                        def1 += Convert.ToInt32(efetividade);
                    else
                        def1 = 6;
                    if (def1 < 0)
                        b = def1 * (-1) + 2;
                    else if (def1 > 0)
                        a = def1 + 2;
                    forms.defbonusP1.Text = (a / b).ToString();
                    forms.battleTXT.Text = "A defesa de " + forms.NomePokeP1.Text + " aumentou";
                }
                else if (Convert.ToInt32(efetividade) < 0 && def2 < 6 && def2 > -6)
                {
                    if ((def2 + Convert.ToInt32(efetividade)) >= -6)
                        def2 += Convert.ToInt32(efetividade);
                    else
                        def2 = -6;
                    if (def2 < 0)
                        b = def2 * (-1) + 2;
                    else if (def2 > 0)
                        a = def2 + 2;
                    forms.defbonusP2.Text = (a / b).ToString();
                    forms.battleTXT.Text = "A defesa de " + forms.NomePokeP2.Text + " diminuiu";
                }
            }
            else if (efeito == "Cura" && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
            {
                if (P1HPAtual == P1HPMax)
                    forms.battleTXT.Text = "nada aconteceu";
                else if (P1HPAtual + Convert.ToInt32(P1HPMax * (Convert.ToDecimal(efetividade) / 100)) > P1HPMax)
                {
                    P1HPAtual = P1HPMax;
                    AttBarrasP1(100, 100, forms);
                    forms.battleTXT.Text = "A Vida de " + forms.NomePokeP1.Text + " foi restaurada";
                }
                else
                {
                    P1HPAtual += Convert.ToInt32(P1HPMax * (Convert.ToDecimal(efetividade) / 100));
                    AttBarrasP1(P1HPAtual, P1HPMax, forms);
                    forms.battleTXT.Text = "A Vida de " + forms.NomePokeP1.Text + " foi restaurada";
                }
            }

            else if (efeito == "Queima")
            {
                if (Burnp2 == 0 && ParalyseP2 == 0 && SleepP2 == 0 && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    Burnp2 = 1;
                    forms.tab1P2stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\burn.jpg");
                    forms.tab2P1stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\burn.jpg");
                    forms.battleP2st.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\burn.jpg");
                    forms.battleTXT.Text = forms.NomePokeP2.Text + " está queimando";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else if (efeito == "Dorme")
            {
                if (Burnp2 == 0 && ParalyseP2 == 0 && SleepP2 == 0 && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    SleepP2 = 1;
                    p2slpturn = random.Next(1, 4);
                    forms.tab1P2stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\asleep.jpg");
                    forms.tab2P1stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\asleep.jpg");
                    forms.battleP2st.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\asleep.jpg");
                    forms.battleTXT.Text = forms.NomePokeP2.Text + " está dormindo";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else if (efeito == "Paralisa")
            {
                if (Burnp2 == 0 && ParalyseP2 == 0 && SleepP2 == 0 && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    ParalyseP2 = 1;
                    forms.tab1P2stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\paralysis.jpg");
                    forms.tab2P1stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\paralysis.jpg");
                    forms.battleP2st.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\paralysis.jpg");
                    forms.battleTXT.Text = forms.NomePokeP2.Text + " está paralisado";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else if (efeito == "CuraStatus")
            {
                if (Burnp1 == 1 || ParalyseP1 == 1 || SleepP1 == 1 && (random.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    Burnp1 = 0;
                    ParalyseP1 = 0;
                    SleepP1 = 0;
                    forms.tab1P1stIMG.Image = null;
                    forms.tab2P2stIMG.Image = null;
                    forms.battleP1st.Image = null;
                    forms.battleTXT.Text = forms.NomePokeP1.Text + " não possui mais efeitos negativos";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else
                forms.battleTXT.Text = "Mas falhou";
        }
        protected void ChangeStatsP2(string efeito, string efetividade, Label acerto, Form1 forms) 
        {
            Random rand = new Random();
            decimal a = 2, b = 2;
            if (efeito == "Ataque" && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
            {
                if (Convert.ToInt32(efetividade) > 0 && atk2 < 6 && atk2 > -6)
                {
                    if ((atk2 + Convert.ToInt32(efetividade)) <= 6)
                        atk2 += Convert.ToInt32(efetividade);
                    else
                        atk2 = 6;
                    if (atk2 < 0)
                        b = atk2 * (-1) + 2;
                    else if (atk2 > 0)
                        a = atk2 + 2;
                    forms.atkbonusP2.Text = (a / b).ToString();
                    forms.battleTXT.Text = "O ataque de " + forms.NomePokeP2.Text + " aumentou";
                }
                else if (Convert.ToInt32(efetividade) < 0 && atk1 < 6 && atk1 > -6)
                {
                    if ((atk1 + Convert.ToInt32(efetividade)) >= -6)
                        atk1 += Convert.ToInt32(efetividade);
                    else
                        atk1 = -6;
                    if (atk1 < 0)
                        b = atk1 * (-1) + 2;
                    else if (atk1 > 0)
                        a = atk1 + 2;
                    forms.atkbonusP1.Text = (a / b).ToString();
                    forms.battleTXT.Text = "O ataque de " + forms.NomePokeP1.Text + " diminuiu";
                }
                else
                    forms.battleTXT.Text = "Nada aconteceu";
            }
            else if (efeito == "Defesa" && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
            {
                if (Convert.ToInt32(efetividade) > 0 && def2 < 6 && def2 > -6)
                {
                    if ((def2 + Convert.ToInt32(efetividade)) <= 6)
                        def2 += Convert.ToInt32(efetividade);
                    else
                        def2 = 6;
                    if (def2 < 0)
                        b = def2 * (-1) + 2;
                    else if (def2 > 0)
                        a = def2 + 2;
                    forms.defbonusP2.Text = (a / b).ToString();
                    forms.battleTXT.Text = "A defesa de " + forms.NomePokeP2.Text + " aumentou";
                }
                else if (Convert.ToInt32(efetividade) < 0 && def1 < 6 && def1 > -6)
                {
                    if ((def1 + Convert.ToInt32(efetividade)) >= -6)
                        def1 += Convert.ToInt32(efetividade);
                    else
                        def1 = -6;
                    if (def1 < 0)
                        b = def1 * (-1) + 2;
                    else if (def1 > 0)
                        a = def1 + 2;
                    forms.defbonusP1.Text = (a / b).ToString();
                    forms.battleTXT.Text = "A defesa de " + forms.NomePokeP1.Text + " diminuiu";
                }
            }
            else if (efeito == "Cura" && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
            {
                if (P2HPAtual == P2HPMax)
                {
                    forms.battleTXT.Text = "nada aconteceu";
                }
                else if (P2HPAtual + Convert.ToInt32(P2HPMax * (Convert.ToDecimal(efetividade) / 100)) > P2HPMax)
                {
                    P2HPAtual = P2HPMax;
                    AttBarrasP1(100, 100, forms);
                    forms.battleTXT.Text = "A Vida de " + forms.NomePokeP2.Text + " foi restaurada";
                }
                else
                {
                    P2HPAtual += Convert.ToInt32(P2HPMax * (Convert.ToDecimal(efetividade) / 100));
                    AttBarrasP1(P2HPAtual, P2HPMax, forms);
                    forms.battleTXT.Text = "A Vida de " + forms.NomePokeP2.Text + " foi restaurada";
                }
            }
            else if (efeito == "Queima")
            {
                if (Burnp1 == 0 && ParalyseP1 == 0 && SleepP1 == 0 && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    Burnp1 = 1;
                    forms.tab1P1stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\burn.jpg");
                    forms.tab2P2stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\burn.jpg");
                    forms.battleP1st.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\burn.jpg");
                    forms.battleTXT.Text = forms.NomePokeP1.Text + " está queimando";
                }
            }
            else if (efeito == "Dorme")
            {
                if ((Burnp1 == 0 && ParalyseP1 == 0 && SleepP1 == 0) && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    SleepP1 = 1;
                    p1slpturn = rand.Next(1, 4);
                    forms.tab1P1stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\asleep.jpg");
                    forms.tab2P2stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\asleep.jpg");
                    forms.battleP1st.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\asleep.jpg");
                    forms.battleTXT.Text = forms.NomePokeP1.Text + " está dormindo";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else if (efeito == "Paralisa")
            {
                if (Burnp1 == 0 && ParalyseP1 == 0 && SleepP1 == 0 && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    ParalyseP1 = 1;
                    forms.tab1P1stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\paralysis.jpg");
                    forms.tab2P2stIMG.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\paralysis.jpg");
                    forms.battleP1st.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\paralysis.jpg");
                    forms.battleTXT.Text = forms.NomePokeP1.Text + " está paralisado";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else if (efeito == "CuraStatus")
            {
                if (Burnp2 == 1 || ParalyseP2 == 1 && (rand.Next(1, 101) <= Convert.ToInt32(acerto.Text)))
                {
                    Burnp2 = 0;
                    ParalyseP2 = 0;
                    forms.tab1P2stIMG.Image = null;
                    forms.tab2P1stIMG.Image = null;
                    forms.battleP2st.Image = null;
                    forms.battleTXT.Text = forms.NomePokeP2.Text + " não possui mais efeitos negativos";
                }
                else
                    forms.battleTXT.Text = "Mas falhou";
            }
            else
                forms.battleTXT.Text = "Mas falhou";
        }

        protected void AttBarrasP2(decimal hpatual, decimal hpmax, Form1 forms)
        {
            int conta = Convert.ToInt32((hpmax - hpatual) * 100 / hpmax);
            if (hpatual <= 0)
            {
                forms.tab1Hp2.Value = 0;
                forms.tab2Hp1.Value = 0;
                forms.BattleHPP2.Value = 0;
            }
            else
            {
                forms.tab1Hp2.Value = 100 - conta;
                forms.tab2Hp1.Value = 100 - conta;
                forms.BattleHPP2.Value = 100 - conta;
            }
        }

        protected void AttBarrasP1(decimal hpatual, decimal hpmax, Form1 forms)
        {
            int conta = Convert.ToInt32((hpmax - hpatual) * 100 / hpmax);
            int testvida = forms.tab1Hp1.Value;
            if (hpatual <= 0)
            {
                forms.tab1Hp1.Value = 0;
                forms.tab2Hp2.Value = 0;
                forms.BattleHPP1.Value = 0;
            }
            else
            {
                forms.tab1Hp1.Value = 100 - conta;
                forms.tab2Hp2.Value = 100 - conta;
                forms.BattleHPP1.Value = 100 - conta;
            }
        }

        protected bool TestarSkills(ComboBox s1, ComboBox s2, ComboBox s3, ComboBox s4)
        {
            if ((s1.Text != s2.Text && s1.Text != s3.Text && s1.Text != s4.Text) && (s2.Text != s3.Text && s2.Text != s4.Text) && (s3.Text != s4.Text))
                return true; 
            else
                return false;
        }

        protected void CategoriaSkill(int catskill, PictureBox fotoskill)
        {
            switch (catskill)
            {
                case 0:
                    fotoskill.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\Statskill.png");
                    break;
                case 1:
                    fotoskill.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\Fsatk.jpeg");
                    break;
                case 2:
                    fotoskill.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + @"\Spatk.jpg");
                    break;
            }
        }
        protected void AttStats(double IV, NumericUpDown lvl, Label stats) 
        {
            double resul;
            double porcentLVL = 0;
            if (lvl.Value > 1)
            {
                porcentLVL = Convert.ToDouble(stats.Text);
                porcentLVL *= 0.010 * Convert.ToDouble(lvl.Value);
            }
            double porcentIV;
            if (IV == 0)
                porcentIV = 0;
            else if (IV > 0 && IV < 16)
                porcentIV = Convert.ToDouble(stats.Text) * 0.001 * Convert.ToDouble(lvl.Value);
            else if (IV > 15 && IV < 26)
                porcentIV = Convert.ToDouble(stats.Text) * 0.005 * Convert.ToDouble(lvl.Value);
            else if (IV > 25 && IV < 31)
                porcentIV = Convert.ToDouble(stats.Text) * 0.01 * Convert.ToDouble(lvl.Value);
            else
                porcentIV = Convert.ToDouble(stats.Text) * 0.02 * Convert.ToDouble(lvl.Value); 
            resul = Convert.ToDouble(stats.Text) + porcentIV + porcentLVL;
            stats.Text = Convert.ToInt32(resul).ToString();
        }
        protected int AttHP(double IV, NumericUpDown lvl, decimal hp)
        {
            double resul;
            double porcentLVL = 0;
            if (lvl.Value > 1)
            {
                porcentLVL = Convert.ToDouble(hp);
                porcentLVL *= 0.030 * Convert.ToDouble(lvl.Value);
            }
            double porcentIV;
            if (IV == 0)
                porcentIV = 0;
            else if (IV > 0 && IV < 16)
                porcentIV = Convert.ToDouble(hp) * 0.005 * Convert.ToDouble(lvl.Value);
            else if (IV > 15 && IV < 26)
                porcentIV = Convert.ToDouble(hp) * 0.01 * Convert.ToDouble(lvl.Value);
            else if (IV > 25 && IV < 31)
                porcentIV = Convert.ToDouble(hp) * 0.015 * Convert.ToDouble(lvl.Value);
            else
                porcentIV = Convert.ToDouble(hp) * 0.025 * Convert.ToDouble(lvl.Value);
            resul = Convert.ToDouble(hp) + porcentIV + porcentLVL;
            return Convert.ToInt32(resul);
        }

        protected decimal Ataque(NumericUpDown lvl, Label tipo1PokeP1, Label tipo2PokeP1, Label tipoHabilidadeP1, Label tipo1PokeP2, Label tipo2PokeP2, Label atackp1, Label defp2, Label podersk, Label atkbonus, Label defbonus, Form1 forms)
        {
            decimal Stab;
            decimal atk;
            decimal def;
            decimal crit = 1;
            forms.critinv.Text = "0";
            decimal rand;
            if (tipoHabilidadeP1.Text == tipo1PokeP1.Text || tipoHabilidadeP1.Text == tipo2PokeP1.Text)
                Stab = 1.5m;
            else
                Stab = 1;
            Random random = new Random();
            if (random.Next(1, 100) <= 7)
            {
                crit = 2;
                forms.critinv.Text = "1";
            }
            rand = random.Next(85, 101);
            rand /= 100;
            decimal lvl1 = lvl.Value;
            lvl1 = (lvl1 * 2) + 10;
            lvl1 /= 250;
            atk = Convert.ToDecimal(atackp1.Text) * Convert.ToDecimal(atkbonus.Text);
            def = Convert.ToDecimal(defp2.Text) * Convert.ToDecimal(defbonus.Text);
            decimal formatack = (lvl1 * (atk / def) * Convert.ToDecimal(podersk.Text) + 2) * Stab * (Vantagem(tipoHabilidadeP1, tipo1PokeP2, tipo2PokeP2) * crit) * rand;
            return formatack;
        }

        protected decimal Vantagem(Label tipoHabilidadeP1, Label tipo1PokeP2, Label tipo2PokeP2)
        {
            int idp1, id1p2, id2p2 = 0, quant=1;
            DataBaseManager PokeMDF = new DataBaseManager("Pokemon");
            DataTable t1 = PokeMDF.ConsultarBanco($"SELECT ID FROM Tipos WHERE Nome = '{tipoHabilidadeP1.Text}'");
            DataTable t2 = PokeMDF.ConsultarBanco($"SELECT ID FROM Tipos WHERE Nome = '{tipo1PokeP2.Text}'");
            DataTable t3 = PokeMDF.ConsultarBanco($"SELECT ID FROM Tipos WHERE Nome = '{tipo2PokeP2.Text}'");
            idp1 = Convert.ToInt32(t1.Rows[0]["ID"]);
            id1p2 = Convert.ToInt32(t2.Rows[0]["ID"]);
            if(t3.Rows.Count>0)
                id2p2 = Convert.ToInt32(t3.Rows[0]["ID"]);
            DataTable TestImune = PokeMDF.ConsultarBanco($"SELECT * FROM Nulo WHERE Origem = {idp1} and Destino = {id1p2} or Origem = {idp1} and Destino = {id2p2}");
            if (TestImune.Rows.Count >= 1)
                return 0;
            DataTable Desvantagem = PokeMDF.ConsultarBanco($"SELECT * FROM Perde WHERE Origem = {idp1} and Destino = {id1p2} or Origem = {idp1} and Destino = {id2p2}"); 
            if (Desvantagem.Rows.Count == 1)
                quant = -1;
            else if (Desvantagem.Rows.Count > 1)
                return 0.25m;
            DataTable Vantagem = PokeMDF.ConsultarBanco($"SELECT * FROM Vence WHERE Origem = {idp1} and Destino = {id1p2} or Origem = {idp1} and Destino = {id2p2}");
            if (Vantagem.Rows.Count == 1)
                quant += 1;
            else if (Vantagem.Rows.Count > 1)
                return 4;
            if (quant == -1)
                return 0.5m;
            else
                return quant;
        }
    }
}
