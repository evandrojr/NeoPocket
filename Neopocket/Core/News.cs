using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Neopocket.News;
using Neopocket.Utils;
using Neopocket.Forms;

namespace Neopocket.Core
{
    public class News
    {
        //Irei agora concatenar as notícias enquanto o sistema estiver aberto
        public string noticiasAnteriores = "";
        public string noticia = ""; // Apenas uma notícia que esta sendo checada
        private string noticiaASerMostrada = ""; // O que vai para o display
        private int lastNewsCheck = 0;
        private FrmMensagem frmMensagem = null;
        private Thread thread;
        private double minutosEntreChecagens = 10; // 10 minutos

        public News()
        {
            if (D.APP_FTP_USER.ToLower() != "sanog")
                return;
            string ultimaNoticiaMostrada = noticiaASerMostrada;
            lastNewsCheck = Environment.TickCount;
            ThreadStart starter = new ThreadStart(Loop);
            thread = new Thread(starter);
            thread.Start();
        }

        ~News()
        {
            try
            {
                frmMensagem.Close();
            }
            catch { };
            try
            {
                frmMensagem.Dispose();
            }
            catch { };
            try
            {
                thread.Abort();
            }
            catch { }
        }

        private void Loop()
        {
            string noticiaNova = "";
            for (;;)
            {
                //Tenta resgatar as notícas 2 vezes pois pode ser que em uma dê problema de conexão
                try
                {
                    Neopocket.News.News news = new Neopocket.News.News();
                    try
                    {
                        noticiaNova = news.GetNews();
                    }
                    catch
                    {
                        try
                        {
                            Thread.Sleep(5000);
                            noticiaNova = news.GetNews();
                        }
                        catch(Exception ex){
                            string erro = ex.Message;
                        }
                    }
                    //noticiaNova = "olá";
                    if (noticiaNova != noticia && noticiaNova != "")
                    {
                        noticia = noticiaNova;
                        if (frmMensagem != null)
                        {
                            frmMensagem.Close();
                            frmMensagem = null;
                        }
                        if (noticiasAnteriores == "")
                        {
                            noticiaASerMostrada = noticia + Environment.NewLine + Environment.NewLine + noticiasAnteriores;
                            noticiasAnteriores = noticia;
                        }
                        else
                        {
                            noticiaASerMostrada = noticia + Environment.NewLine + Environment.NewLine + noticiasAnteriores;
                            noticiasAnteriores = noticia + Environment.NewLine + Environment.NewLine + noticiasAnteriores;
                        }
                        Neopocket.Lib.Sound snd = new Neopocket.Lib.Sound();
                        snd.PlayNotify();
                        frmMensagem = new FrmMensagem(noticiaASerMostrada);
                        frmMensagem.DeveEstarNoTopo = true;
                        frmMensagem.ShowDialog();
                        frmMensagem.DeveEstarNoTopo = false;
//                        Util.FormExibirDialog(new FrmMensagem(noticiaASerMostrada));
                    }
                }
                catch { }
                Thread.Sleep((int) (minutosEntreChecagens * 60 * 1000));
            }// Fim for
        }//Fim Metodo Loop
      
    }
}
