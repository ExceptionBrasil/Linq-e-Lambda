using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_e_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            /**** LISTAS ****/
            List<GeneroMusical> generoMusicals = PopularGenero();
            List<Musica> musicas = PopularMusica();
            List<Artista> artistas = PopularArtita();


            /**** Consultas ****/

            //01 - Simples
            var query01 = from g in generoMusicals
                          where g.Nome.Contains("Rock")
                          select new { Nome = g.Nome, Id = g.Id };

            Console.WriteLine("------------------------------------------------");
            foreach (var item in query01)
            {
                Console.WriteLine("{0}\t{1}", item.Id, item.Nome);
            }

            //Join de listas não relacionadas
            var query02 = from m in musicas
                          join g in generoMusicals on m.IdGenero equals g.Id
                          select new { m, g };



            Console.WriteLine("------------------------------------------------");
            foreach (var item in query02)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", item.m.Id, item.m.Nome, item.g.Id, item.g.Nome);
            }


            //Lambda
            var query03 = artistas.Where(a => a.Nome.Contains("Deus"));
            query03 = query03.Take(10);


            Console.WriteLine("------------------------------------------------");
            foreach (var item in query03)
            {
                Console.WriteLine("{0}\t{1}", item.Id, item.Nome);
                Console.WriteLine("            Músicas");
                foreach (var musica in item.Musicas)
                {
                    Console.WriteLine("            {0}\t{1}", musica.Id, musica.Nome);
                }
            }


            //Filtro com lista
            var query04 = from a in artistas
                          select a;

            query04 = query04.Where(m => m.Musicas.Any(mu => mu.IdGenero == 1));

            Console.WriteLine("------------------------------------------------");
            foreach (var item in query03)
            {
                Console.WriteLine("{0}\t{1}", item.Id, item.Nome);
                Console.WriteLine("            Músicas");
                foreach (var musica in item.Musicas)
                {
                    Console.WriteLine("            {0}\t{1}", musica.Id, musica.Nome);
                }
            }

            Console.ReadKey();
        }



        #region Populando as Listas
        private static List<GeneroMusical> PopularGenero()
        {
            List<GeneroMusical> generoMusicals = new List<GeneroMusical>() {
                new GeneroMusical() { Id = 1, Nome = "Rock" },
                new GeneroMusical() { Id = 2, Nome = "Samba" },
                new GeneroMusical() { Id = 3, Nome = "Clássica" },
                new GeneroMusical() { Id = 4, Nome = "Heavy Metal" }
                };


            return generoMusicals;
        }

        private static List<Artista> PopularArtita()
        {
            List<Artista> artistas = new List<Artista>();

            artistas.Add(new Artista() { Id = 1, Nome = "Batuto do Samba" });
            artistas.Add(new Artista() { Id = 2, Nome = "Deus do Metal" });
            artistas.Add(new Artista() { Id = 3, Nome = "Só Música Boa" });

            var listaMusicas = PopularMusica();

            artistas[0].Musicas = listaMusicas.Where(m => m.IdGenero == 2).ToList();
            artistas[1].Musicas = listaMusicas.Where(m => m.IdGenero == 4).ToList();
            artistas[2].Musicas = listaMusicas.Where(m => m.IdGenero != 4 && m.IdGenero != 2).ToList();




            return artistas;
        }
        private static List<Musica> PopularMusica()
        {
            List<Musica> generoMusicals = new List<Musica>() {
              new Musica(){Id=1,Nome="Samba do Criolo Doido", IdGenero=2},
              new Musica(){Id=2,Nome="5ª Sinfonia de Mozart", IdGenero=3},
              new Musica(){Id=3,Nome="Stand and fight", IdGenero=4},
              new Musica(){Id=4,Nome="Black Mirror", IdGenero=4},
              new Musica(){Id=4,Nome="Red Door", IdGenero=1 }
                };


            return generoMusicals;
        }
        #endregion
    }
}
