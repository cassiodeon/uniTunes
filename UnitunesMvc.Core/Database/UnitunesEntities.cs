using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;

namespace UnitunesMvc.Core.Database.Entities
{

    public class UnitunesEntities : DbContext
    {
        private const string SERVIDOR = "localhost\\sqlexpress";
        private const string DB_USER = "sa";
        private const string DB_PASS = "_43690";
        private const string DB_NAME = "Unitunes"; 

        public const int USUARIO_ADMIN_ID = 1;

        public DbSet<Album> Albums { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Streaming> Streamings { get ; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ArquivoBinario> Binarios { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<UsuarioFavoritos> Favoritos { get; set; }
        public DbSet<Midia> Midias { get; set;}
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItems { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        static UnitunesEntities()
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UnitunesEntities>());
        }

        public UnitunesEntities()
            : base(MontarStringConexao())
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        private static string MontarStringConexao()
        {

            //if (1 == 1)
            //{
            //    return "Data Source=.\\SQLExpress;Integrated Security=true;AttachDbFilename = C:\\Pasta\\Unitunes.mdf; User Instance = true; ";
            //}
                
            var cs = new StringBuilder();

            cs.AppendFormat("Server={0};", SERVIDOR);
            cs.AppendFormat("Database={0};", DB_NAME);
            cs.AppendFormat("User Id={0};", DB_USER);
            cs.AppendFormat("Password={0};", DB_PASS);
            return "Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Workspace\\uniTunes\\UnitunesMvc.Core\\Database\\Database_Unitunes.mdf; Integrated Security = True";
            //return cs.ToString();
        }

        public void feed()
        {
            try
            {
                inserirUsuarios();
                this.SaveChanges();
                inserirCategorias();
                this.SaveChanges();
                inserirFilmes();
                this.SaveChanges();
                inserirMusicas();
                inserirPodcasts();
                inserirLivros();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            } 
            finally
            {
                this.SaveChanges();
            }
        }

        private void inserirMusicas()
        {
            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Musica,
                Nome = "Starway to heaven",
                TipoStreaming = TipoStreaming.Musica,
                Descricao = ".",
                Duracao = 6,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 23
            });

            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Musica,
                Nome = "Venus",
                TipoStreaming = TipoStreaming.Musica,
                Descricao = ".",
                Duracao = 3,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 13
            });

            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Musica ,
                Nome = "Buzios do coração",
                TipoStreaming = TipoStreaming.Musica,
                Descricao = ".",
                Duracao = 4,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 6
            });

        }

        private void inserirLivros()
        {

            this.Livros.Add(new Livro
            {
                Nome = "Harry Potter",
                Descricao = ".",
                NumeroPaginas = 323,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 9
            });

            this.Livros.Add(new Livro
            {
                Nome = "Design Patterns: Elements of Reusable Object-Oriented Software",
                Descricao = "gof",
                NumeroPaginas = 555,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 11
            });

        }

        private void inserirPodcasts()
        {
            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Podcast ,
                Nome = "Nerdcast #123",
                TipoStreaming = TipoStreaming.Podcast,
                AutorId = 1,
                Preco = 1,
                Descricao = ".",
                Duracao = 55,
                CategoriaId = 19
            });

            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Podcast,
                Nome = "Rapaduracast #123",
                TipoStreaming = TipoStreaming.Podcast,
                Descricao = ".",
                AutorId = 1,
                Preco = 1,
                Duracao = 55,
                CategoriaId = 22
            });

        }

        private void inserirFilmes()
        {

            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Video ,
                Nome = "The Shawshank Redemption",
                TipoStreaming = TipoStreaming.Video,
                Descricao = "Two imprisoned men bond over a number of years.",
                Duracao = 120,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 14
            });

            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Video,
                Nome = "The Dark Knight",
                TipoStreaming = TipoStreaming.Video,
                Descricao = "When the menace known as the Joker wreaks havoc.",
                Duracao = 120,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 13
            });

            this.Streamings.Add(new Streaming
            {
                Tipo = TipoMidia.Video,
                Nome = "The Lord of the Rings: The Return of the King",
                TipoStreaming = TipoStreaming.Video,
                Descricao = "Gandalf and Aragorn lead the World of Men against Sauron's army..",
                Duracao = 220,
                AutorId = 1,
                Preco = 1,
                CategoriaId = 12
            });
        }

        private void inserirCategorias()
        {
            this.Categorias.Add(new Categoria { Nome = "Industrial Rock", Tipo = TipoMidia.Musica });   //1         
            this.Categorias.Add(new Categoria { Nome = "MPB", Tipo = TipoMidia.Musica });               //2           
            this.Categorias.Add(new Categoria { Nome = "Gospel", Tipo = TipoMidia.Musica });            //3
            this.Categorias.Add(new Categoria { Nome = "Alternative Rock", Tipo = TipoMidia.Musica });  //4
            this.Categorias.Add(new Categoria { Nome = "Pop", Tipo = TipoMidia.Musica });               //5
            this.Categorias.Add(new Categoria { Nome = "Tecnobrega", Tipo = TipoMidia.Musica });        //6

            this.Categorias.Add(new Categoria { Nome = "Exatas", Tipo = TipoMidia.Livro });             //7
            this.Categorias.Add(new Categoria { Nome = "Humanas", Tipo = TipoMidia.Livro });            //8 
            this.Categorias.Add(new Categoria { Nome = "Fantástica", Tipo = TipoMidia.Livro });         //9
            this.Categorias.Add(new Categoria { Nome = "Direito", Tipo = TipoMidia.Livro });            //10
            this.Categorias.Add(new Categoria { Nome = "Software", Tipo = TipoMidia.Livro });           //11

            this.Categorias.Add(new Categoria { Nome = "Sci-fi", Tipo = TipoMidia.Video });             //12
            this.Categorias.Add(new Categoria { Nome = "Ação", Tipo = TipoMidia.Video });               //13
            this.Categorias.Add(new Categoria { Nome = "Drama", Tipo = TipoMidia.Video });              //14
            this.Categorias.Add(new Categoria { Nome = "Suspense", Tipo = TipoMidia.Video });           //15
            this.Categorias.Add(new Categoria { Nome = "Terror", Tipo = TipoMidia.Video });             //16
            this.Categorias.Add(new Categoria { Nome = "Thriler", Tipo = TipoMidia.Video });            //17

            this.Categorias.Add(new Categoria { Nome = "Arte", Tipo = TipoMidia.Podcast });             //18
            this.Categorias.Add(new Categoria { Nome = "Games", Tipo = TipoMidia.Podcast });            //19
            this.Categorias.Add(new Categoria { Nome = "Ciência", Tipo = TipoMidia.Podcast });          //20
            this.Categorias.Add(new Categoria { Nome = "Tecnologia", Tipo = TipoMidia.Podcast });       //21
            this.Categorias.Add(new Categoria { Nome = "Filmes", Tipo = TipoMidia.Podcast });           //22

            this.Categorias.Add(new Categoria { Nome = "Classic Rock", Tipo = TipoMidia.Musica });      //23
        }

        private void inserirUsuarios()
        {
            this.Usuarios.Add(new Usuario { PrimeiroNome = "Admin",
                UltimoNome = "Admin", Email = "admin@email.com",
                Senha = "qwerasd", Tipo = TipoUsuario.Administrador,
                Conta = new Conta { Saldo = 0}
            });

            this.Usuarios.Add(new Usuario
            {
                PrimeiroNome = "Adademico",
                UltimoNome = "Random",
                Email = "academico@email.com",
                Senha = "qwerasd",
                Tipo = TipoUsuario.Academico ,
                Conta = new Conta { Saldo = 0 }
            });
            this.Usuarios.Add(new Usuario
            {
                PrimeiroNome = "Autor",
                UltimoNome = "Random",
                Email = "autor@email.com",
                Senha = "qwerasd",
                Tipo = TipoUsuario.Administrador,
                Conta = new Conta { Saldo = 0 }
            });
            this.SaveChanges();
        }

        public System.Data.Entity.DbSet<UnitunesMvc.Core.Database.Entities.VendaItem> VendaItems { get; set; }
    }
}


