using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TrabalhoFinalLp3
{
    class Batalha : Var
    {
        public async void batalhar(Form1 forms)
        {
            Random random = new Random();
            int rand = random.Next(1, 4);
            int randomacao = random.Next(1, 101);
            if ((Ordem(forms) == 1 && turno == 1) || (Ordem(forms) == 2 && turno == 2))
            {
                switch (acaop1)
                {
                    case 1:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP1 == 1 && p1slpturn == 0)
                            {
                                SleepP1 = 0;
                                forms.battleTXT.Text = forms.NomePokeP1.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P1stIMG.Image = null;
                                forms.tab2P2stIMG.Image = null;
                                forms.battleP1st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP1.Text + " usou " + S1P1NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S1P1FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste="", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                            int c = 1;
                            for(int i = aaa.Length-1; c < 5; i--)
                                 {
                                    teste += aaa[i];
                                    c++;
                                 }
                            if(teste == "3PM.")
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                }
                            await Task.Delay(2000);
                            if ((ParalyseP1 == 0 || (ParalyseP1 == 1 && rand > 1)) && (SleepP1 == 0))
                            {
                                if (Vantagem(forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS1P1.Text) && S1P1CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que a Ação não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 1 && (S1P1CAT == 1 || S1P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS1P1.Text))
                                    forms.battleTXT.Text = forms.NomePokeP2.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2) < 1 && Vantagem(forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 0 && (S1P1CAT == 1 || S1P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS1P1.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 1 && (S1P1CAT == 1 || S1P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS1P1.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S1P1CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS1P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkFsP1, forms.DefFsP2, forms.PoderS1P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S1P1CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS1P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS1P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkSpP1, forms.DefSpP2, forms.PoderS1P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S1P1CAT == 0)
                                {
                                    ChangeStatsP1(S1P1Efeito, S1P1Efetividade, forms.AcertoS1P1, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP2(P2HPAtual, P2HPMax, forms);
                                await Task.Delay(1000);
                                if (P2HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP2.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP1 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP1 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p1slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS1P1.Text) && S1P1CAT > 0 && SleepP1 != 1 && (ParalyseP1 == 0 || (ParalyseP1 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS1P1.Text = (Convert.ToInt32(forms.QuantAtualS1P1.Text) - 1).ToString();
                            if (forms.QuantAtualS1P1.Text == "0")
                                forms.Atk1P1.Enabled = false;
                            break;
                        }
                    case 2:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP1 == 1 && p1slpturn == 0)
                            {
                                SleepP1 = 0;
                                forms.battleTXT.Text = forms.NomePokeP1.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P1stIMG.Image = null;
                                forms.tab2P2stIMG.Image = null;
                                forms.battleP1st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP1.Text + " usou " + S2P1NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S2P1FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP1 == 0 || (ParalyseP1 == 1 && rand > 1)) && (SleepP1 == 0))
                            {
                                if (Vantagem(forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS2P1.Text) && S2P1CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 1 && (S2P1CAT == 1 || S2P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS2P1.Text))
                                    forms.battleTXT.Text = forms.NomePokeP2.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2) < 1 && Vantagem(forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 0 && (S2P1CAT == 1 || S2P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS2P1.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 1 && (S2P1CAT == 1 || S2P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS2P1.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S2P1CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS2P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkFsP1, forms.DefFsP2, forms.PoderS2P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S2P1CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS2P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS2P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkSpP1, forms.DefSpP2, forms.PoderS2P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S2P1CAT == 0)
                                {
                                    ChangeStatsP1(S2P1Efeito, S2P1Efetividade, forms.AcertoS2P1, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP2(P2HPAtual, P2HPMax, forms);
                                await Task.Delay(1000);
                                if (P2HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP2.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP1 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP1 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p1slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS2P1.Text) && S2P1CAT > 0 && SleepP1 != 1 && (ParalyseP1 == 0 || (ParalyseP1 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS2P1.Text = (Convert.ToInt32(forms.QuantAtualS2P1.Text) - 1).ToString();
                            if (forms.QuantAtualS2P1.Text == "0")
                                forms.Atk2P1.Enabled = false;
                            break;
                        }
                    case 3:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP1 == 1 && p1slpturn == 0)
                            {
                                SleepP1 = 0;
                                forms.battleTXT.Text = forms.NomePokeP1.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P1stIMG.Image = null;
                                forms.tab2P2stIMG.Image = null;
                                forms.battleP1st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP1.Text + " usou " + S3P1NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S3P1FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP1 == 0 || (ParalyseP1 == 1 && rand > 1)) && (SleepP1 == 0))
                            {
                                if (Vantagem(forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS3P1.Text) && S3P1CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 1 && (S3P1CAT == 1 || S3P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS3P1.Text))
                                    forms.battleTXT.Text = forms.NomePokeP2.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2) < 1 && Vantagem(forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 0 && (S3P1CAT == 1 || S3P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS3P1.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 1 && (S3P1CAT == 1 || S3P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS3P1.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S3P1CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS3P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkFsP1, forms.DefFsP2, forms.PoderS3P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S3P1CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS3P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS3P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkSpP1, forms.DefSpP2, forms.PoderS3P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S3P1CAT == 0)
                                {
                                    ChangeStatsP1(S3P1Efeito, S3P1Efetividade, forms.AcertoS3P1, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP2(P2HPAtual, P2HPMax, forms);
                                await Task.Delay(1000);
                                if (P2HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP2.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP1 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP1 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p1slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS3P1.Text) && S3P1CAT > 0 && SleepP1 != 1 && (ParalyseP1 == 0 || (ParalyseP1 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS3P1.Text = (Convert.ToInt32(forms.QuantAtualS3P1.Text) - 1).ToString();
                            if (forms.QuantAtualS3P1.Text == "0")
                                forms.Atk3P1.Enabled = false;
                            break;
                        }
                    case 4:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP1 == 1 && p1slpturn == 0)
                            {
                                SleepP1 = 0;
                                forms.battleTXT.Text = forms.NomePokeP1.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P1stIMG.Image = null;
                                forms.tab2P2stIMG.Image = null;
                                forms.battleP1st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP1.Text + " usou " + S4P1NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S4P1FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P1SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP1 == 0 || (ParalyseP1 == 1 && rand > 1)) && (SleepP1 == 0))
                            {
                                if (Vantagem(forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS4P1.Text) && S4P1CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2) == 1 && (S4P1CAT == 1 || S4P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS4P1.Text))
                                    forms.battleTXT.Text = forms.NomePokeP2.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2) < 1 && Vantagem(forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 0 && (S4P1CAT == 1 || S4P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS4P1.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2) > 1 && (S4P1CAT == 1 || S4P1CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS4P1.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S4P1CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS4P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkFsP1, forms.DefFsP2, forms.PoderS4P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S4P1CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS4P1.Text))
                                    P2HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP1, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.TipoS4P1, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.AtkSpP1, forms.DefSpP2, forms.PoderS4P1, forms.atkbonusP1, forms.defbonusP2, forms));
                                else if (S4P1CAT == 0)
                                {
                                    ChangeStatsP1(S4P1Efeito, S4P1Efetividade, forms.AcertoS4P1, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP2(P2HPAtual, P2HPMax, forms);
                                await Task.Delay(1000);
                                if (P2HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP2.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP1 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP1 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP1.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p1slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS4P1.Text) && S4P1CAT > 0 && SleepP1 != 1 && (ParalyseP1 == 0 || (ParalyseP1 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS4P1.Text = (Convert.ToInt32(forms.QuantAtualS4P1.Text) - 1).ToString();
                            if (forms.QuantAtualS4P1.Text == "0")
                                forms.Atk4P1.Enabled = false;
                            break;
                        }
                }

            }

            else if ((Ordem(forms) == 1 && turno == 2) || (Ordem(forms) == 2 && turno == 1))
            {
                switch (acaop2)
                {
                    case 1:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP2 == 1 && p2slpturn == 0)
                            {
                                SleepP2 = 0;
                                forms.battleTXT.Text = forms.NomePokeP2.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P2stIMG.Image = null;
                                forms.tab2P1stIMG.Image = null;
                                forms.battleP2st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP2.Text + " usou " + S1P2NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S1P2FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP2 == 0 || (ParalyseP2 == 1 && rand > 1)) && (SleepP2 == 0))
                            {
                                if (Vantagem(forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS1P2.Text) && S1P2CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 1 && (S1P2CAT == 1 || S1P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS1P2.Text))
                                    forms.battleTXT.Text = forms.NomePokeP1.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1) < 1 && Vantagem(forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 0 && (S1P2CAT == 1 || S1P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS1P2.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 1 && (S1P2CAT == 1 || S1P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS1P2.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S1P2CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS1P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkFsP2, forms.DefFsP1, forms.PoderS1P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S1P2CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS1P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS1P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkSpP2, forms.DefSpP1, forms.PoderS1P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S1P2CAT == 0)
                                {
                                    ChangeStatsP2(S1P2Efeito, S1P2Efetividade, forms.AcertoS1P2, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP1(P1HPAtual, P1HPMax, forms);
                                await Task.Delay(2000);
                                if (P1HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP1.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP2 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP2 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p2slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS1P2.Text) && S1P2CAT > 0 && SleepP2 != 1 && (ParalyseP2 == 0 || (ParalyseP2 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS1P2.Text = (Convert.ToInt32(forms.QuantAtualS1P2.Text) - 1).ToString();
                            if (forms.QuantAtualS1P2.Text == "0")
                                forms.Atk1P2.Enabled = false;
                            break;
                        }
                    case 2:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP2 == 1 && p2slpturn == 0)
                            {
                                SleepP2 = 0;
                                forms.battleTXT.Text = forms.NomePokeP2.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P2stIMG.Image = null;
                                forms.tab2P1stIMG.Image = null;
                                forms.battleP2st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP2.Text + " usou " + S2P2NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S2P2FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP2 == 0 || (ParalyseP2 == 1 && rand > 1)) && (SleepP2 == 0))
                            {
                                if (Vantagem(forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text) && S2P2CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 1 && (S2P2CAT == 1 || S2P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text))
                                    forms.battleTXT.Text = forms.NomePokeP1.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1) < 1 && Vantagem(forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 0 && (S2P2CAT == 1 || S2P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 1 && (S2P2CAT == 1 || S2P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S2P2CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkFsP2, forms.DefFsP1, forms.PoderS2P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S2P2CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS2P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkSpP2, forms.DefSpP1, forms.PoderS2P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S2P2CAT == 0 && randomacao <= Convert.ToInt32(forms.AcertoS2P2.Text))
                                {
                                    ChangeStatsP2(S2P2Efeito, S2P2Efetividade, forms.AcertoS2P2, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP1(P1HPAtual, P1HPMax, forms);
                                await Task.Delay(2000);
                                if (P1HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP1.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP2 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP2 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p2slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS2P2.Text) && S2P2CAT > 0 && SleepP2 != 1 && (ParalyseP2 == 0 || (ParalyseP2 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS2P2.Text = (Convert.ToInt32(forms.QuantAtualS2P2.Text) - 1).ToString();
                            if (forms.QuantAtualS2P2.Text == "0")
                                forms.Atk2P2.Enabled = false;
                            break;
                        }
                    case 3:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP2 == 1 && p2slpturn == 0)
                            {
                                SleepP2 = 0;
                                forms.battleTXT.Text = forms.NomePokeP2.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P2stIMG.Image = null;
                                forms.tab2P1stIMG.Image = null;
                                forms.battleP2st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP2.Text + " usou " + S3P2NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S3P2FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP2 == 0 || (ParalyseP2 == 1 && rand > 1)) && (SleepP2 == 0))
                            {
                                if (Vantagem(forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS3P2.Text) && S3P2CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 1 && (S3P2CAT == 1 || S3P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS3P2.Text))
                                    forms.battleTXT.Text = forms.NomePokeP1.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1) < 1 && Vantagem(forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 0 && (S3P2CAT == 1 || S3P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS3P2.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 1 && (S3P2CAT == 1 || S3P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS3P2.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S3P2CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS3P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkFsP2, forms.DefFsP1, forms.PoderS3P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S3P2CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS3P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS3P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkSpP2, forms.DefSpP1, forms.PoderS3P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S3P2CAT == 0)
                                {
                                    ChangeStatsP2(S3P2Efeito, S3P2Efetividade, forms.AcertoS3P2, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP1(P1HPAtual, P1HPMax, forms);
                                await Task.Delay(2000);
                                if (P1HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP1.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP2 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP2 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p2slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS3P2.Text) && S3P2CAT > 0 && SleepP2 != 1 && (ParalyseP2 == 0 || (ParalyseP2 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS3P2.Text = (Convert.ToInt32(forms.QuantAtualS3P2.Text) - 1).ToString();
                            if (forms.QuantAtualS3P2.Text == "0")
                                forms.Atk3P2.Enabled = false;
                            break;
                        }
                    case 4:
                        {
                            forms.btnbattle.Enabled = false;
                            if (SleepP2 == 1 && p2slpturn == 0)
                            {
                                SleepP2 = 0;
                                forms.battleTXT.Text = forms.NomePokeP2.Text + " acordou";
                                await Task.Delay(1000);
                                forms.tab1P2stIMG.Image = null;
                                forms.tab2P1stIMG.Image = null;
                                forms.battleP2st.Image = null;
                            }
                            forms.battleTXT.Text = forms.NomePokeP2.Text + " usou " + S4P2NOME;
                            try { forms.BattleSkillImg.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\Imagens" + S4P2FOTO); }
                            catch { forms.BattleSkillImg.Image = null; }
                            string teste = "", aaa = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                            int c = 1;
                            for (int i = aaa.Length - 1; c < 5; i--)
                            {
                                teste += aaa[i];
                                c++;
                            }
                            if (teste == "3PM.")
                            {
                                forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\poksounds" + P2SOUND;
                                forms.efeitosonoropok.Ctlcontrols.play();
                            }
                            await Task.Delay(2000);
                            if ((ParalyseP2 == 0 || (ParalyseP2 == 1 && rand > 1)) && (SleepP2 == 0))
                            {
                                if (Vantagem(forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 0 && randomacao <= Convert.ToInt32(forms.AcertoS4P2.Text) && S4P2CAT > 0)
                                {
                                    forms.battleTXT.Text = "Parece que o Ataque não fez nenhum efeito.";
                                    await Task.Delay(2000);
                                    break;
                                }
                                else if (Vantagem(forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1) == 1 && (S4P2CAT == 1 || S4P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS4P2.Text))
                                    forms.battleTXT.Text = forms.NomePokeP1.Text + " Tomou dano";
                                else if (Vantagem(forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1) < 1 && Vantagem(forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 0 && (S4P2CAT == 1 || S4P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS4P2.Text))
                                    forms.battleTXT.Text = "Não foi muito efetivo";
                                else if (Vantagem(forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1) > 1 && (S4P2CAT == 1 || S4P2CAT == 2) && randomacao <= Convert.ToInt32(forms.AcertoS4P2.Text))
                                {
                                    forms.efeitosonoropok.URL = Directory.GetCurrentDirectory() + @"\crit.MP3";
                                    forms.efeitosonoropok.Ctlcontrols.play();
                                    forms.battleTXT.Text = "Foi super efetivo!!";
                                }
                                if (S4P2CAT == 1 && randomacao <= Convert.ToInt32(forms.AcertoS4P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkFsP2, forms.DefFsP1, forms.PoderS4P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S4P2CAT == 2 && randomacao <= Convert.ToInt32(forms.AcertoS4P2.Text))
                                    P1HPAtual -= Convert.ToInt32(Ataque(forms.numLVLP2, forms.Tipo1PokP2, forms.Tipo2PokP2, forms.TipoS4P2, forms.Tipo1PokP1, forms.Tipo2PokP1, forms.AtkSpP2, forms.DefSpP1, forms.PoderS4P2, forms.atkbonusP2, forms.defbonusP1, forms));
                                else if (S4P2CAT == 0)
                                {
                                    ChangeStatsP2(S4P2Efeito, S4P2Efetividade, forms.AcertoS4P2, forms);
                                }
                                if (forms.critinv.Text == "1")
                                {
                                    await Task.Delay(1000);
                                    forms.battleTXT.Text = "Foi um ataque crítico!";
                                }
                                AttBarrasP1(P1HPAtual, P1HPMax, forms);
                                await Task.Delay(2000);
                                if (P1HPAtual <= 0)
                                {
                                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP1.Text + " desmaiou. Fim da batalha!";
                                    await Task.Delay(3000);
                                    forms.tabPokControl.SelectedTab = forms.menu;
                                    forms.battlemusic.Ctlcontrols.stop();
                                }
                            }
                            else if (ParalyseP2 == 1 && rand == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta paralisado";
                                await Task.Delay(2000);
                            }
                            else if (SleepP2 == 1)
                            {
                                forms.battleTXT.Text = "Porém " + forms.NomePokeP2.Text + " esta dormindo";
                                await Task.Delay(2000);
                                p2slpturn -= 1;
                            }
                            if (randomacao > Convert.ToInt32(forms.AcertoS4P2.Text) && S4P2CAT > 0 && SleepP2 != 1 && (ParalyseP2 == 0 || (ParalyseP2 == 1 && rand != 1)))
                            {
                                forms.battleTXT.Text = "Mas errou";
                                await Task.Delay(2000);
                            }
                            forms.QuantAtualS4P2.Text = (Convert.ToInt32(forms.QuantAtualS4P2.Text) - 1).ToString();
                            if (forms.QuantAtualS4P2.Text == "0")
                                forms.Atk4P2.Enabled = false;
                            break;
                        }
                }
            }
            forms.BattleSkillImg.Image = null;
            if (Burnp1 == 1 && turno == 2)
            {
                decimal conta = 0.06m;
                forms.battleTXT.Text = forms.NomePokeP1.Text + " continua queimando";
                P1HPAtual -= Convert.ToInt32(conta * P2HPMax);
                AttBarrasP1(P1HPAtual, P1HPMax, forms);
                await Task.Delay(2000);
                if (P1HPAtual <= 0)
                {
                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP1.Text + " desmaiou. Fim da batalha!";
                    await Task.Delay(3000);
                    forms.tabPokControl.SelectedTab = forms.menu;
                    forms.battlemusic.Ctlcontrols.stop();
                }
            }
            if (Burnp2 == 1 && turno == 2)
            {
                decimal conta = 0.06m;
                forms.battleTXT.Text = forms.NomePokeP2.Text + " continua queimando";
                P2HPAtual -= Convert.ToInt32(conta * P2HPMax);
                AttBarrasP2(P2HPAtual, P2HPMax, forms);
                await Task.Delay(2000);
                if (P2HPAtual <= 0)
                {
                    forms.battleTXT.Text = "O Pokemon " + forms.NomePokeP2.Text + " desmaiou. Fim da batalha!";
                    await Task.Delay(3000);
                    forms.tabPokControl.SelectedTab = forms.menu;
                    forms.battlemusic.Ctlcontrols.stop();
                }
            }
            if (turno == 1 && P1HPAtual > 0 && P2HPAtual > 0)
                turno = 2;
            else if (turno == 2 && P1HPAtual > 0 && P2HPAtual > 0)
            {
                forms.tabPokControl.SelectedTab = forms.tabPok1;
                turno = 1;
            }
            else
            {
                turno = 1;
                AttBarrasP1(100, 100, forms);
                AttBarrasP2(100, 100, forms);
            }
            forms.critinv.Text = "0";
            forms.btnbattle.Enabled = true;
        }
    }
}
